using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using work_platform_backend.Models;

namespace work_platform_backend.Validations
{
    public class EmailAttributeUniqueValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (ApplicationContext)validationContext.GetService(typeof(ApplicationContext));
            var entity = context.User.SingleOrDefault(e => e.Email == value.ToString());

            if(entity != null )
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }            
            else
            {
                return ValidationResult.Success;
            }
        }

        private string GetErrorMessage(string email)
        {
            return $"Email {email} is Already in use";
        }
    }
}