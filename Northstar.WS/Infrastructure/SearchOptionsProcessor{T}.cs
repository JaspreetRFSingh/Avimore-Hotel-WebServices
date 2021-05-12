
using Northstar.WS.Infrastructure.CustomAttributes;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Northstar.WS.Infrastructure
{
    public class SearchOptionsProcessor<T>
    {
        private string[] _searchQuery;
        public SearchOptionsProcessor(string[] searchQuery)
        {
            _searchQuery = searchQuery;
        }

        public IEnumerable<SearchTerm> GetAllTerms()
        {
            if (_searchQuery == null) yield break;

            foreach (var expression in _searchQuery)
            {
                if (string.IsNullOrEmpty(expression)) continue;
                var tokens = expression.Split(' ');

                if (tokens.Length < 3)
                {
                    yield return new SearchTerm
                    {
                        IsValidSyntax = false,
                        Name = expression
                    };
                    continue;
                }

                yield return new SearchTerm
                {
                    IsValidSyntax = true,
                    Name = tokens[0],
                    Operator = tokens[1],
                    Value = string.Join(" ", tokens.Skip(2))
                };
            }
        }

        public IEnumerable<SearchTerm> GetValidTerms()
        {
            var searchTerms = GetAllTerms()
                .Where(x=>x.IsValidSyntax)
                .ToArray();

            if (!searchTerms.Any()) yield break;

            var declaredTerms = GetTermsFromModel();

            foreach (var term in searchTerms)
            {
                var declaredTerm = declaredTerms.SingleOrDefault(x => x.Name.Equals(term.Name, System.StringComparison.OrdinalIgnoreCase));
                if (declaredTerm == null) continue;
                yield return new SearchTerm
                {
                    IsValidSyntax = term.IsValidSyntax,
                    Name = declaredTerm.Name,
                    Operator = term.Operator,
                    Value = term.Value
                };
            }

        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            var terms = GetValidTerms().ToArray();
            if (!terms.Any()) return query;
            var modifiedQuery = query;
            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper.GetPropertyInfo<T>(term.Name);
                var obj = ExpressionHelper.Parameter<T>();

                var left = ExpressionHelper.GetPropertyExpression(obj, propertyInfo);
                var right = Expression.Constant(term.Value);

                var comparisonExpression = Expression.Equal(left, right);

                var lambdaExpression = ExpressionHelper.GetLambda<T, bool>(obj, comparisonExpression);

                modifiedQuery = ExpressionHelper.CallWhere(modifiedQuery, lambdaExpression);

            }

            return modifiedQuery;
        }

        private static IEnumerable<SearchTerm> GetTermsFromModel()
        {
            return typeof(T).GetTypeInfo().DeclaredProperties
                .Where(p => p.GetCustomAttributes<SearchableAttribute>().Any())
                .Select(p => new SearchTerm { Name = p.Name });
        }

    }
}
