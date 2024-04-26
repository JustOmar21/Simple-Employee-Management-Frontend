using System.ComponentModel.DataAnnotations;

namespace Frontend.CustomValidation
{
    public class DateIsFutureVA : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateOnly)
            {
                DateOnly date = (DateOnly)value;
                DateTime dateNow = DateTime.Now;
                if (new DateOnly(dateNow.Year,dateNow.Month,dateNow.Day).CompareTo((DateOnly)value) >= 0)
                {
                    return new ValidationResult($"{date.ToLongDateString()} is not a future Date");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult($"The value sent is not of DateType 'DateOnly'");
            }
        }
    }
}
