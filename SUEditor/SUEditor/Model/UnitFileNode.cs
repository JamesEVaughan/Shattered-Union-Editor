using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUEditor.Types;

namespace SUEditor.Model
{
    /// This file used to contain a singly-linked list built around UnitFileNode. That class
    /// was abandoned in favor of using a List with UnitFileNode, because I was dumb and
    /// forgot about the bountiful library of .NET. :<

    /// <summary>
    /// UnitFileNode is a node class for a List. It stores an instance of a Unit and the start of  
    /// its corresponding data structure in UnitFile.dat. Implements IEquatable and IComparable.
    /// </summary>
    public class UnitFileNode : IEquatable<UnitFileNode>, IComparable<UnitFileNode>
    {
        // Properties
        /// <summary>
        /// The actaul representation of this unit
        /// </summary>
        public Unit TheUnit { get; private set; }
        /// <summary>
        /// Byte-address of the unit in "UnitFile.dat"
        /// </summary>
        public long Index { get; set; }
        /// <summary>
        /// The DisplayName of the unit we're pointing at
        /// </summary>
        public SUEString Name => TheUnit.DisplayName;


        //Constructors
        public UnitFileNode()
        {
            Index = 0;
            TheUnit = null;
        }
        
        public UnitFileNode(Unit un, long i)
        {
            TheUnit = un;
            Index = i;
        }
        
        /// Implementation of IEquatable
        public bool Equals(UnitFileNode other)
        {
            if (other == null)
            {
                return false;
            }

            // Both Name and Index must equal for two nodes to equal
            return (Index == other.Index) && (Name.Equals(other.Name));
        }

        public override bool Equals(object other)
        {
            if (other is UnitFileNode)
            {
                return Equals((UnitFileNode)other);
            }

            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            return unchecked(Name.GetHashCode() + 7 * (int)Index);
        }

        // Implementation of IComparable
        public int CompareTo(UnitFileNode other)
        {
            if (other == null)
            {
                return 1;
            }

            // Compare Indexes so that ordering is done by its Index rather than Name
            return Index.CompareTo(other.Index);
        }
    }
}
