using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

namespace SUEditor.View
{
    /// <summary>
    /// ValidateNumerics is a subclass of ValidationRules. Its main use is to validate that numeric 
    /// input in a variety of ways. To utilitize a given check, assign a value to the desired
    /// property. Only works on positive values.
    /// </summary>
    public class ValidateNumerics : ValidationRule
    {
        // Properties
        /// <summary>
        /// Min is the minimum allowed value. If set to a positive value, this will validate that a
        /// given input is greater than or equal to Min. Defaults to negative which disables this
        /// check.
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Max is the maximum allowed value. If set to a positive value, this will validate that a
        /// given input is less than or equal to Max. Defaults to negative which disables this check.
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Multi is the required divisor of a value. If set to a positive value, this will validate
        /// that a given input is a multiple of Multi. Defaults to negative which disables this check.
        /// </summary>
        public int Multi { get; set; }

        // Constructors
        public ValidateNumerics()
        {
            Min = -1;
            Max = -1;
            Multi = -1;
        }

        // Implementation of ValidationRules
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int theNumber;

            // First, let's make sure this is a number
            if (!(Int32.TryParse((value as string), NumberStyles.Currency, System.Globalization.NumberFormatInfo.CurrentInfo, out theNumber)))
            {
                // It's not a number
                return new ValidationResult(false, "The given value is not an integer.");
            }

            // Now, we run each test based on our properties
            // NOTE: Short circuit all tests with a negative check to skip them quickly

            if (Min >= 0 && theNumber < Min)
            {
                // It's under the minimum
                return new ValidationResult(false, String.Format("The given value is below the minimum of {0}", Min));
            }

            if (Max >= 0 && theNumber > Max)
            {
                // It's over the maximum
                return new ValidationResult(false, String.Format("The given value is above the maximum of {0}", Max));
            }

            if (Multi >= 0 && (theNumber % Multi) != 0)
            {
                // It's not a multiple of Multi
                return new ValidationResult(false, String.Format("The given value is not a multiple of {0}", Multi));
            }

            // If it passes the checks, it's good to go
            return new ValidationResult(true, null);
        }
    }
}
