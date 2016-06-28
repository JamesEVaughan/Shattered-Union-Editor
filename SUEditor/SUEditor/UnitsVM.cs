using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor
{
    /// <summary>
    /// This is the ViewModel class for the UnitEditor
    /// </summary>
    class UnitsVM
    {
        // Properties
        /// <summary>
        /// A listing of the units in the file
        /// </summary>
        public List<string> UnitNames;
        public Unit CurUnit;
    }
}
