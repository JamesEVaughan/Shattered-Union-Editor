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
        public List<string> CurUnits { get; private set; }

        /// <summary>
        /// The currently selected unit
        /// </summary>
        public string SelectedUnit { get; private set; }

        // Constructors

        public ValidateDisplayName()
        {
            // It's the user's responsibility to set CurUnits
            CurUnits = null;
            SelectedUnit = null;
        }

        // Simple copy constructor
        public ValidateDisplayName(string[] names)
        {
            CurUnits = new List<string>(names.Length);
            SelectedUnit = null;

            foreach (string unit in names)
            {
                CurUnits.Add(String.Copy(unit));
            }
        }

        /// <summary>
        /// Sets the list of names, CurUnits, given a list of UnitNames
        /// </summary>
        /// <param name="names">A collection of UnitNames in an IEnumerable collection</param>
        public void SetUnitList(IEnumerable<UnitName> names)
        {
            CurUnits = new List<string>(100);
            foreach (UnitName uName in names)
            {
                CurUnits.Add(uName.ViewName);
            }
        }

        // Implementation of ValidationRule
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // First, value should be a string and have contain something
            string temp = value as string;
            if (temp == null || temp == "")
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
            foreach (string name in CurUnits)
            {
                if (temp.Equals(name) && !temp.Equals(SelectedUnit))
                {
                    return new ValidationResult(false, "Unit already exists with this name.");
                }
            }

            // If it passes all that, it's good to go
            return new ValidationResult(true, null);
        }

        // Event delegates
        public void OnSelectionChange(object sender, SelectionChangedEventArgs args)
        {
            ComboBox tempCB = sender as ComboBox;
            
            if (tempCB == null)
            {
                // It wasn't a combobox, ignore
                return;
            }

            // Now, make sure it's a valid selection for us
            if (tempCB.SelectedItem is UnitName)
            {
                // And set it
                SelectedUnit = (tempCB.SelectedItem as UnitName).ViewName;
            }
        }

    }
}
