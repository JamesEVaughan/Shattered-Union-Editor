using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor
{
    using Types;
    /// <summary>
    /// UnitList creates a list of all the units in UnitTypes.dat. Each node, UnitNode,
    /// contains only the DisplayName and index of each unit found.
    /// </summary>
    class UnitList
    {
        // Fields 
        private int length;     // Number of UnitNodes in list
        private UnitNode first; // First UnitNode in list
        private UnitNode last;  // Last UnitNode in list

        // Properties
        public int Length => length; // Number of UnitNodes in list

        // Constructors
        // Empty list
        public UnitList()
        {
            length = 0;
            first = new UnitNode();
            last = null;
        }

        // Starts with a node
        public UnitList(Name dn, uint i)
        {
            length = 1;
            first = new UnitNode(dn, i);
            last = first;

        }

        // Adds a new node
        public void addNode(Name dn, uint i)
        {
            length++;
            last.addNode(new UnitNode(dn, i));
            last = last.next;
        }

        private class UnitNode
        {
            // Fields
            public UnitNode next;

            // Properties
            public Name DisplayName { get; set; }
            public uint Index { get; set; }


            //Constructors
            public UnitNode()
            {
                DisplayName = new Name();
                Index = 0;
                next = new UnitNode();
            }

            public UnitNode(Name dn, uint i)
            {
                DisplayName = new Name(dn);
                Index = i;
                next = new UnitNode();
            }

            public void addNode(UnitNode newNode)
            {
                next = newNode;
            }
        }
    }
}
