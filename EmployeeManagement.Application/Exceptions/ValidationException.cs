using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace EmployeeManagement.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> ValidationErrors { get; set; }

        public ValidationException(ValidationResult result)
        {
            ValidationErrors = new List<string>();
            foreach (var error in result.Errors)
            {
                ValidationErrors.Add(error.ErrorMessage);
            }
        }
    }
}
