

using System.Globalization;
using System.Windows.Controls;

namespace Epicor.App.Validations
{
    public class DateValidationRules : ValidationRule
    {
        public String Message { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                if (value is DateTime)
                    return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, Message);
        }
    }
}
