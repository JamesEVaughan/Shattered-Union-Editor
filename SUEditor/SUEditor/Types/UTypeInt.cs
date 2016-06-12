using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor.Types
{
    /// <summary>
    /// SUEInt represents a 4-byte, numeric data type from UnitTypes.dat
    /// </summary>
    class SUEInt
    {

        public int Value { get; set; }

        public SUEInt(int i)
        {
            Value = i;
        }

        public SUEInt(SUEInt i)
        {
            Value = i.Value;
        }

        public SUEInt()
        {
            Value = 0;
        }

        static public int Size()
        {
            // Returns the number of bytes in SUEInt
            return 4;
        }

        // Implicit conversions
        static public implicit operator int(SUEInt suei)
        {
            return suei.Value;
        }

        static public implicit operator SUEInt(int i)
        {
            return new SUEInt(i);
        }
    }
}
