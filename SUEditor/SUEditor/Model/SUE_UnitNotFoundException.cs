using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor.Model
{
    /// <summary>
    /// An exception. Thrown if a given unit is not found in a list of Units
    /// </summary>
    public class SUE_UnitNotFoundException : Exception
    {

        public SUE_UnitNotFoundException() :
            base()
        {

        }

        public SUE_UnitNotFoundException(string message) :
            base(message)
        {

        }
    }
}
