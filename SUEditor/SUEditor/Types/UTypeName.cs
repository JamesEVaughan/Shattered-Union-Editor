using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor.Types
{
    class Name
    {
        /*
         * Name represents a 39 byte, character-array data type from UnitTypes.dat
         */

        private char[] data;

        public string Value
        {
            get
            {
                return new string(data);
            }
            set
            {
                for (int i = 0; i < Name.Size(); i++)
                {
                    if (i < value.Length)
                    {
                        data[i] = value[i];
                    }
                    else
                    {
                        data[i] = '\0';
                    }
                }
            }
        }

        public Name(char[] ca)
        {
            data = new char[39];
            int limit;
            if (ca.Length >= 39)
            {
                limit = 39;
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

        public Name(string s) :
            this(s.ToCharArray())
        {
        }

        public Name()
        {
            data = new char[39];
        }

        public Name(Name n)
        {
            data = new char[39];
            for(int i = 0; i < 39; i++)
            {
                data[i] = n.data[i];
            }
        }

        static public int Size()
        {
            // Returns the number of bytes in Short
            return 39;
        }
    }
}
