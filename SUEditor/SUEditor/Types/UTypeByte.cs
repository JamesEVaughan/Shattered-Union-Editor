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
    class SUEByte : IEquatable<SUEByte>, IComparable<SUEByte>
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

        /// Implicit type converstions
        public static implicit operator byte(SUEByte sueb)
        {
            return sueb.Value;
        }

        public static implicit operator SUEByte(byte b)
        {
            return new SUEByte(b);
        }

        /// Implementation of IEquatable
        public bool Equals(SUEByte other)
        {
            if (other == null)
            {
                return false;
            }

            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            // If obj is a SUEByte call our equals...
            if (obj as SUEByte != null)
            {
                return Equals((SUEByte)obj);
            }
            // ...Otherwise, kick it up the chain
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// Implementation of IComparable
        public int CompareTo(SUEByte other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return Value.CompareTo(other);
            }
        }

        
    }
}
