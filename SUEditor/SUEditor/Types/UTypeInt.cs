using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor.Types
{
    class Int
    {
        /*
         * Int represents a 4-byte, numeric data type from UnitTypes.dat
         */

        public int Value { get; set; }

        public Int(int i)
        {
            Value = i;
        }

        public Int(Int i)
        {
            Value = i.Value;
        }

        static public int Size()
        {
            // Returns the number of bytes in Short
            return 4;
        }
    }
}
