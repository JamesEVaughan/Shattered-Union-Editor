using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor.Types
{
    /// <summary>
    /// SUEByte represents a 1-byte, numeric data type from the UnitTypes file.
    /// </summary>
    class SUEByte
    {
        
        public byte Value { get; set; }

        public SUEByte(byte b)
        {
            Value = b;
        }

        public SUEByte(SUEByte b)
        {
            Value = b.Value;
        }

        public SUEByte()
        {
            Value = 0;
        }

        static public int Size()
        {
            // Returns number of bytes in SUEByte
            return 1;
        }

        // Implicit converstions
        public static implicit operator byte(SUEByte sueb)
        {
            return sueb.Value;
        }

        public static implicit operator SUEByte(byte b)
        {
            return new SUEByte(b);
        }
    }
}
