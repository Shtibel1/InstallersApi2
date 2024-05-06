using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validations
{
    public class ConditionalRequiredAttribute : ValidationAttribute
    {
        private readonly string _property;
        private readonly string _desiredValue;

        public ConditionalRequiredAttribute(string property, string desiredValue)
        {
            _property = property;
            _desiredValue = desiredValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_property);
            if (property == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            var propertyValue = property.GetValue(validationContext.ObjectInstance)?.ToString();
            if (propertyValue == _desiredValue && value == null)
                return new ValidationResult($"{validationContext.DisplayName} is required when {_property} is {_desiredValue}.");

            return ValidationResult.Success;
        }
    }
}
