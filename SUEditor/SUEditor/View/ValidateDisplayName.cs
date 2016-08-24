using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.ObjectModel;

using SUEditor.ViewModel;
using System.Globalization;

namespace SUEditor.View
{
    public class ValidateDisplayName : ValidationRule
    {
        // Properties
        /// <summary>
        /// A pointer to the current list of units
        /// </summary>
        public ObservableCollection<UnitName> CurUnits { get; set; }

        // Constructors

        public ValidateDisplayName()
        {

        }

        /// <summary>
        /// Sets up our ValidationRule for DisplayNames
        /// </summary>
        /// <param name="theList">The active list of units</param>
        public ValidateDisplayName(ObservableCollection<UnitName> theList)
        {
            // It's just a pointer, so simple assigned is fine
            CurUnits = theList;
        }

        // Implementation of ValidationRule
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // First, value should be a string
            string temp = value as string;
            if (temp == null)
            {
                // Prolly a stupid check, but it needs to be done.
                return new ValidationResult(false, "Value is not a string.");
            }

            // String can only contain ASCII characters and non
            // Note: This is an assumption, because all strings in the data files are coded as 8-bit
            // chars
            foreach (char tempC in temp)
            {
                if (Char.IsLetterOrDigit(tempC))
                {
                    if ((int)tempC > 127)
                    {
                        return new ValidationResult(false, "Value contains non-ASCII characters.");
                    }
                    continue;
                }
                // This final check is for all special cases such as the hyphen and space
                if (tempC == '-' || tempC == ' ')
                {
                    continue;
                }

                // If it fails those checks, it's invalid
                return new ValidationResult(false, "Value contains illegal characters.");
            }

            // Lastly, check to make sure it's unique
            foreach (UnitName uName in CurUnits)
            {
                if (temp.Equals(uName.ViewName))
                {
                    return new ValidationResult(false, "Unit already exists with this name.");
                }
            }

            // If it passes all that, it's good to go
            return new ValidationResult(true, null);
        }

    }
}
