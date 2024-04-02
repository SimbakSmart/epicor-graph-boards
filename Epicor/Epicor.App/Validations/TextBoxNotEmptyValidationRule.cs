
using System.Windows.Controls;

namespace Epicor.App.Validations
{
    public class TextBoxNotEmptyValidationRule : ValidationRule
    {
        public String Message { get; set; }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string str = value as string;
            if (str != null)
            {
                if (str.Length > 0)
                    return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, Message);
        }

       
    }
}
