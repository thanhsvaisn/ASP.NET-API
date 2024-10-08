using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2305M_LuuQuangThanh.Attributes
{
    public class AgeValidation : ValidationAttribute
    {
        private readonly int _minAge;

        public AgeValidation(int minAge)
        {
            _minAge = minAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dob = (DateTime)value;
            if (DateTime.Now.Year - dob.Year < _minAge)
            {
                return new ValidationResult($"Employee must be over {_minAge} years old.");
            }
            return ValidationResult.Success;
        }
    }
}
