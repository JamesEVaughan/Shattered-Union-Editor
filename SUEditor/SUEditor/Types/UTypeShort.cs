using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor.Types
{
    /// <summary>
    /// SUEShort represents a 2-byte, numeric data type from UnitTypes.dat
    ///</summary>
    class SUEShort
    {

        public short Value { get; set; }

        public SUEShort(short s)
        {
            Value = s;
        }

        public SUEShort(SUEShort s)
        {
            Value = s.Value;
        }

        public SUEShort()
        {
            Value = 0;
        }

        static public int Size ()
        {
            // Returns the number of bytes in SUEShort
            return 2;
        }

        // Implicit conversions
        static public implicit operator short(SUEShort sues)
        {
            return sues.Value;
        }

        static public implicit operator SUEShort(short s)
        {
            return new SUEShort(s);
        }
    }
}
