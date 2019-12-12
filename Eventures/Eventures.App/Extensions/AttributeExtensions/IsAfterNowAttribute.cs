using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eventures.App.Extensions
{
    public class IsAfterNowAttribute : ValidationAttribute
    {
        private readonly DateTime currentDateTime;

        public IsAfterNowAttribute()
        {
            this.currentDateTime = DateTime.UtcNow;
        }
        protected override ValidationResult IsValid(object dateTime, ValidationContext validationContext)
        {
            if ((DateTime)dateTime <= currentDateTime)
            {
                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
