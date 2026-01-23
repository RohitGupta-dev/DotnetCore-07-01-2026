using System.ComponentModel.DataAnnotations;

namespace LearningDotnet.Validator
{
    public class DateCheckValidaterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;
            if(date > DateTime.Now)
            {
                return new ValidationResult("The date must not greater then today ");
            }
            return ValidationResult.Success;
        }
    }
}
