


using System.Globalization;
using System.Windows.Controls;

namespace Epicor.App.Validations
{
    using System.Globalization;
    using System.Windows.Controls;

    namespace Epicor.App.Validations
    {
        public class DateValidationRuleNotEmpty : ValidationRule
        {
            public override ValidationResult Validate(object value, CultureInfo cultureInfo)
            {
                //if (value is DateTime selectedDate)
                //{
                //    if (selectedDate <= DateTime.Today)
                //    {
                //        // You can include this check for future date validation as well
                //        return new ValidationResult(ValidationResult.CreateFailedResult(
                //            "Please select a date in the future."));
                //    }
                //    else if (selectedDate == DateTime.MinValue) // Check for empty date
                //    {
                //       // return new ValidationResult(ValidationResult.CreateFailedResult(
                //           // "Please select a date."));
                //    }
                //}

                return ValidationResult.ValidResult;
            }
        }
    }
}
