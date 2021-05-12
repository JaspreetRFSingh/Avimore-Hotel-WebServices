
using Northstar.WS.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Northstar.WS.Models
{
    public class SortOptions<T> : IValidatableObject
    {
        /// <summary>
        /// The queryname which ASP.NET Core searches for, in the URI (Case Insensitive)
        /// </summary>
        public string[] OrderBy { get; set; }

        /// <summary>
        /// Valdates the sorting by parameters
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SortOptionsProcessor<T>(OrderBy);
            var validTerms = processor.GetValidTerms().Select(x => x.Name);
            var invalidTerms = processor.GetAllTerms().Select(x => x.Name)
                .Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var term in invalidTerms)
            {
                yield return new ValidationResult(
                    $"Invalid sort term: '{term}'",
                    new [] {nameof(OrderBy)}
                    );
            }
        }

        /// <summary>
        /// Applies sort options to a database query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<T> Apply(IQueryable<T> query)
        {
            var processor = new SortOptionsProcessor<T>(OrderBy);
            return processor.Apply(query);
        }

    }
}
