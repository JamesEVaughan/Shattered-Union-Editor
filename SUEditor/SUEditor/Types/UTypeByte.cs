using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor.Types
{
    class Byte
    {
        /// <summary>
        /// Byte represents a 1-byte, numeric data type from the UnitTypes file.
        /// </summary>
        
        public byte Value { get; set; }

        public Byte(byte b)
        {
            Value = b;
        }

        public Byte(Byte b)
        {
            Value = b.Value;
        }
        static public int Size()
        {
            // Returns number of bytes in Byte
            return 1;
        }
    }
}
