using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor.Types
{
    /// <summary>
    /// SUEString is 29-byte array that is stores an ASCII-encoded string from the file.
    /// </summary>
    class SUEString
    {

        private char[] data;

        public string Value
        {
            get
            {
                return new string(data);
            }
            set
            {
                for (int i = 0; i < SUEString.Size(); i++)
                {
                    if (i < value.Length)
                    {
                        data[i] = value[i];
                    }
                    else
                    {
                        // Pad array with zeroes if the string is smaller than 29
                        data[i] = '\0';
                    }
                }
            }
        }

        public SUEString(char[] ca)
        {
            data = new char[29];
            int limit;
            if (ca.Length >= 29)
            {
                limit = 29;
            }
            else
            {
                limit = ca.Length;
            }
            for (int i = 0; i < limit; i++)
            {
                data[i] = ca[i];
            }
       
        }

        public SUEString(string s) :
            this(s.ToCharArray())
        {
        }

        public SUEString()
        {
            data = new char[29];
        }

        public SUEString(SUEString n)
        {
            data = new char[29];
            for(int i = 0; i < 29; i++)
            {
                data[i] = n.data[i];
            }
        }

        static public int Size()
        {
            // Returns the number of bytes in SUEString
            return 29;
        }
    }
}
