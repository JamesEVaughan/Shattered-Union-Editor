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
    public class UnitList
    {
        // Properties
        /// <summary> 
        /// This List is the bulk of the class. 
        /// </summary>
        public List<UnitFileNode> TheUnits { get; private set; }
        /// <summary>
        /// Quick and dirty exposure of the TheUnits Count property.
        /// </summary>
        public int Count => TheUnits.Count;

        // Constructors
        public UnitList(int size)
        {
            TheUnits = new List<UnitFileNode>(size);
        }

        public UnitList() :
            this(100)
        {

        }

        // Methods
        /// <summary>
        /// Simple indexer for accessing a Unit in this list
        /// </summary>
        /// <param name="index">An index to TheUnits</param>
        /// <returns>The Unit at index</returns>
        public Unit unitAt(int index)
        {
            return TheUnits[index].TheUnit;
        }

        /// <summary>
        /// Generates a new List for the starting armies of each faction. Should be updated 
        /// after a new unit has been created or a unit has been deleted.
        /// </summary>
        /// <returns>A List of the starting armies of each faction.</returns>
        public List<UnitFactionNode> GetStartingUnits()
        {
            List<UnitFactionNode> uFactList = new List<UnitFactionNode>(Count);
            Unit temp;

            for (int i = 0; i < Count; i++)
            {
                // Grab the unit from the list
                temp = unitAt(i);

                // Build the node from that unit
                uFactList.Add(new UnitFactionNode(temp));
            }

            return uFactList;
        }

        /// <summary>
        /// Clones the specified unit, adding it to TheUnits
        /// </summary>
        /// <param name="un">Instance of Unit to be cloned</param>
        /// <param name="newName">The DispName the newly cloned instance will have</param>
        /// <returns>The index of the unit in the file</returns>
        public long CloneUnit(Unit un, string newName)
        {
            Unit tempUnit = new Unit(un);   // Copy un to tempUnit
            long newInd = TheUnits[TheUnits.Count - 1].Index + 145; // Get the next available index
            tempUnit.DisplayName = new Types.SUEString(newName);

            TheUnits.Add(new UnitFileNode(tempUnit, newInd));

            // Finaly, return the index of the new unit
            return newInd;
        }
    }
}
