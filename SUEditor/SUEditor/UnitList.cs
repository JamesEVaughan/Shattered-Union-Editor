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
            first = null;
            last = null;
        }

        // Starts with a node
        public UnitList(SUEString dn, long i)
        {
            length = 1;
            first = new UnitNode(dn, i);
            last = first;

        }

        /// <summary>
        /// Adds a new node to the list
        /// </summary>
        /// <param name="dn">Name of the unit to be added</param>
        /// <param name="i">Index in the file</param>
        public void addNode(SUEString dn, long i)
        {
            // IF we're an empty list
            if (length == 0)
            {
                first = new UnitNode(dn, i);
            }
            // If we're a 1-sized list
            else if (length == 1)
            {
                first.addNode(new UnitNode(dn, i));
                last = first.Next;
            }
            else
            {
                last.addNode(new UnitNode(dn, i));
                last = last.Next;
            }

            length++;
        }

        /// <summary>
        /// Builds a printable table of the UnitList
        /// </summary>
        /// <returns>A table to be printed to the screen</returns>
        public string buildTable()
        {
            string table = "";
            UnitNode temp = first;
            for (int i = 0; i < length; i++)
            {
                table += temp.Index + "\t" + temp.DisplayName.Value + "\n";
                temp = temp.Next;
            }

            return table;
        }

        public UnitNode getNodeAt(int index)
        {
            UnitNode temp = first;
            for(int i = 0; i < index; i++)
            {
                temp = temp.Next;
            }

            return temp;
        }

        public class UnitNode
        {
            // Fields
            private UnitNode next;

            // Properties
            public SUEString DisplayName { get; set; }
            public long Index { get; set; }
            public UnitNode Next => next;


            //Constructors
            public UnitNode()
            {
                DisplayName = new SUEString();
                Index = 0;
                next = null;
            }

            public UnitNode(SUEString dn, long i)
            {
                DisplayName = new SUEString(dn);
                Index = i;
                next = null;
            }

            public void addNode(UnitNode newNode)
            {
                next = newNode;
            }
        }
    }
}
