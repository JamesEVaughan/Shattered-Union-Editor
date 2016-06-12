using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor
{
    /// <summary>
    /// This exception is thrown if a given file doesn't exist or is not properly formatted.
    /// </summary>
    class SUE_InvalidFileException : Exception
    {
        public SUE_InvalidFileException() :
            base()
        {

        }

        public SUE_InvalidFileException(string message) :
            base(message)
        {

        }
    }
}
