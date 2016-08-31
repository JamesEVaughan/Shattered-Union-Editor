using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Globalization;

using SUEditor.Types;

namespace SUEditor.ViewModel
{
    /// <summary>
    /// This enumeration is for the factions a unit can be a member of. Uses a value converter to
    /// display readable names.
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionConverter))]
    public enum VMFact
    {
        [Description("New England Alliance")]
        NEA,
        [Description("The Confederacy")]
        Con,
        [Description("Great Plains Federation")]
        GPF,
        [Description("Republic of Texas")]
        RoT,
        [Description("California Commonwealth")]
        Cal,
        [Description("Pacifica")]
        Pac,
        [Description("European Union")]
        EU,
        [Description("Russia")]
        Rus,
        [Description("All US Factions")]
        USA
    }

    public class EnumDescriptionConverter : EnumConverter
    {
        public EnumDescriptionConverter(Type t) :
            base(t)
        {

        }

        // Overriden methods

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is VMFact)
            {
                // Ok, this is a VMFact
                VMFact tempFact = (VMFact)value;

                // If we're looking for a string value
                if (destinationType == typeof(string))
                {
                    switch (tempFact)
                    {
                        case VMFact.NEA:
                            return "New England Alliance";
                        case VMFact.Con:
                            return "The Confederacy";
                        case VMFact.GPF:
                            return "Great Plains Federation";
                        case VMFact.RoT:
                            return "Republic of Texas";
                        case VMFact.Cal:
                            return "California Commonwealth";
                        case VMFact.Pac:
                            return "Pacifica";
                        case VMFact.EU:
                            return "European Union";
                        case VMFact.Rus:
                            return "Russia";
                        case VMFact.USA:
                            return "All US Factions";
                    }
                }

                // If we're looking for a UnitFaction
                if (destinationType == typeof(UnitFaction))
                {
                    if (tempFact != VMFact.USA)
                    {
                        return (UnitFaction)((int)(tempFact));
                    }

                    // That doesn't work for VMFact.USA because the values don't line up
                    return UnitFaction.USA;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            // We only care about converting from UnitFaction
            if (value is UnitFaction)
            {
                UnitFaction tempFact = (UnitFaction)value;

                if (tempFact != UnitFaction.USA)
                {
                    return (VMFact)((int)tempFact);
                }

                return VMFact.USA;
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
