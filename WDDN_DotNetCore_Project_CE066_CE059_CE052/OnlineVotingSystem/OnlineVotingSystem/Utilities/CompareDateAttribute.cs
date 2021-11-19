using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Utilities
{
    public class CompareDateAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public CompareDateAttribute()
        {
        }
        public CompareDateAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var resultDateTime = Convert.ToDateTime(value);
            if(_comparisonProperty != null)
            {
                var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
                if (property != null)
                {
                    var comparisonValue = Convert.ToDateTime(property.GetValue(validationContext.ObjectInstance));
                    if (resultDateTime < comparisonValue)
                        return new ValidationResult($"Date should not be less then {comparisonValue}");
                }
            }
                
            if(_comparisonProperty == null)
            {
                var enteredDate = Convert.ToDateTime(value);
                var currentDate = DateTime.Now;
                if (enteredDate < currentDate)
                {
                    return new ValidationResult($"Date should not be less then {currentDate}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
