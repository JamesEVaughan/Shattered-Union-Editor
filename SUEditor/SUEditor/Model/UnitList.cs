using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor.Model
{
    /// <summary>
    /// UnitList is a wrapper class for a generic List. It provides some helper functions for access.
    /// </summary>
    class UnitList
    {
        // Properties
        /// <summary> This List is the bulk of the class. </summary>
        public List<Unit> TheUnits { get; private set; }

        // Constructors
        public UnitList(int size)
        {
            TheUnits = new List<Unit>(size);
        }

        public UnitList() :
            this(100)
        {

        }
    }
}
