namespace BugTracker.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class RequiredForCompany : ValidationAttribute
    {
        private readonly bool IsCan;

        public RequiredForCompany(bool isCan)
        {
            this.IsCan = isCan;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance;
            if (model == null || this.IsCan == true)
            {
                return ValidationResult.Success;
            }

            if (value == null)
            {
                var propertyInfo = validationContext.ObjectType.GetProperty(validationContext.MemberName);
                return new ValidationResult($"{propertyInfo.Name} is required for the current value ");
            }

            return ValidationResult.Success;
        }
    }
}
