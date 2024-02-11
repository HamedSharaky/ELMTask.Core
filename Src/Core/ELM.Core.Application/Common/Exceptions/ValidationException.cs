using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace ELM.Core.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Failures { get; }

        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            var validationFailures = failures.ToList();

            var propertyNames = validationFailures.Select(e => e.PropertyName).Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = validationFailures.Where(e => e.PropertyName == propertyName).Select(e => e.ErrorMessage).ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }
    }
}