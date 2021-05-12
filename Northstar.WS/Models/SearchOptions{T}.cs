using Northstar.WS.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Northstar.WS.Models
{
    public class SearchOptions<T> : IValidatableObject
    {
        /// <summary>
        /// The parameter name which ASP.NET Core searches for, in the URI (Case insensitive)
        /// </summary>
        public string[] Search { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SearchOptionsProcessor<T>(Search);

            var validTerms = processor.GetValidTerms().Select(p => p.Name);
            var invalidTerms = processor.GetAllTerms().Select(p => p.Name)
                .Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult(
                    $"Invalid search term: '{term}'",
                    new[] { nameof(Search) }
                    );
            }
        }

        public IQueryable<T> Apply(IQueryable<T>query)
        {
            var processor = new SearchOptionsProcessor<T>(Search);
            return processor.Apply(query);
        }

    }
}
