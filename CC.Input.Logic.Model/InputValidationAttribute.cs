using System.ComponentModel.DataAnnotations;

namespace CC.Input.Logic.Model
{
    internal class InputValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var input = validationContext.ObjectInstance as Input;
            if (input != null)
            {
                if (input.DateOfInstallation > DateOnly.FromDateTime(DateTime.Now))
                {
                    return new ValidationResult("'Date of Installation' must be a date in the past.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
