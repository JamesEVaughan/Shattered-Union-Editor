using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUEditor.Types;
using SUEditor.Model;

namespace SUEditor.ViewModel
{
    /// <summary>
    /// This is the ViewModel class for the Unit Editor tab
    /// </summary>
    class UnitEditorVM
    {
        // Properties
        /// <summary>
        /// A listing of the units in the file
        /// </summary>
        public List<string> UnitNames;
        public Unit CurUnit;
    }
}
