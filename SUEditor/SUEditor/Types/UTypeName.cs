using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor.Types
{
    /// <summary>
    /// SUEString is 30-byte array that is stores an ASCII-encoded string from the file.
    /// </summary>
    class SUEString : IEquatable<SUEString>
    {

        private byte[] data;

        public string Value
        {
            get
            {
                return Encoding.UTF8.GetString(data);
            }
            set
            {
                for (int i = 0; i < SUEString.Size(); i++)
                {
                    if (i < value.Length)
                    {
                        data[i] = (byte)value[i];
                    }
                    else
                    {
                        // Pad array with zeroes if the string is smaller than 29
                        data[i] = 0;
                    }
                }
            }
        }

        public SUEString(char[] ca)
        {
            data = new byte[30];
            int limit;
            if (ca.Length >= 30)
            {
                limit = 30;
            }
            else
            {
                limit = ca.Length;
            }
            for (int i = 0; i < limit; i++)
            {
                data[i] = (byte)ca[i];
            }
       
        }

        public SUEString(byte[] ba)
        {
            data = new byte[Size()];
            int limit = Size();

            if (ba.Length < Size())
            {
                limit = ba.Length;
            }

            for (int i = 0; i < limit; i++)
            {
                data[i] = ba[i];
            }
        }

        public SUEString(string s) :
            this(s.ToCharArray())
        {
        }

        public SUEString()
        {
            data = new byte[30];
        }

        public SUEString(SUEString n)
        {
            data = new byte[30];
            for(int i = 0; i < 30; i++)
            {
                data[i] = n.data[i];
            }
        }

        static public int Size()
        {
            // Returns the number of bytes in SUEString
            return 29;
        }

        /// <summary>
        /// A separate setter for cases of having a byte array
        /// </summary>
        /// <param name="ba"> The byte to set to Value </param>
        public void ByteArraySetter(byte[] ba)
        {
            int tempSize = (ba.Length >= Size()) ? Size() : ba.Length;
            for (int i = 0; i < Size(); i++)
            {
                if (i < tempSize)
                {
                    data[i] = ba[i];
                }
                else
                {
                    data[i] = 0;
                }
            }
        }

        public bool Equals(SUEString other)
        {
            if (other == null)
            {
                return false;
            }

            /// Obviously true
            else if (other == this)
            {
                return true;
            }

            for (int i = 0; i < 29; i++)
            {
                if (data[i] != other.data[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is SUEString)
            {
                return Equals((SUEString)obj);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
