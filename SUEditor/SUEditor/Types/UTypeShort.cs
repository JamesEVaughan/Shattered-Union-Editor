using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor.Types
{
    class Short
    {
        /// <summary>
        /// Short represents a 2-byte, numeric data type from UnitTypes.dat
        ///</summary>

        public short Value { get; set; }

        public Short(short s)
        {
            Value = s;
        }

        public Short (Short s)
        {
            Value = s.Value;
        }

        static public int Size ()
        {
            // Returns the number of bytes in Short
            return 2;
        }
    }
}
