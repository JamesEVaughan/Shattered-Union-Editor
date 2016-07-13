using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SUEditor.Types;

namespace SUEditor.Model
{
    /// <summary>
    /// A node for a List containing the starting armies for each faction
    /// </summary>
    public class UnitFactionNode : IEquatable<UnitFactionNode>
    {
        // Properties
        /// <summary>
        /// The name of the unit, should point to the underlaying Unit.Display
        /// </summary>
        public SUEString Name { get; private set; }
        /// <summary>
        /// A 8-member array storing the number of this unit a faction starts with, points to
        /// the appropriate Unit.StartsIn###.
        /// <para> 
        /// Addressed with SUEEnums.UnitFaction
        /// </para>
        /// </summary>
        public SUEByte[] FactionCounts { get; private set; }

        // Constructors
        /// <summary>
        /// This is the only provided constructor, because we are pointing back to the main
        /// UnitList to make updating easier.
        /// </summary>
        /// <param name="un">The Unit this node should reference, must be initialized</param>
        public UnitFactionNode(Unit un)
        {
            FactionCounts = new SUEByte[8];

            // Link Name to the DisplayName of un
            Name = un.DisplayName;

            // Link the appropriate FactionCounts[#] to un.StartsIn###
            FactionCounts[(int)UnitFaction.NEA] = un.StartsInNEA;
            FactionCounts[(int)UnitFaction.Con] = un.StartsInCon;
            FactionCounts[(int)UnitFaction.GPF] = un.StartsInGPF;
            FactionCounts[(int)UnitFaction.RoT] = un.StartsInRoT;
            FactionCounts[(int)UnitFaction.Cal] = un.StartsInCal;
            FactionCounts[(int)UnitFaction.Pac] = un.StartsInPac;
            FactionCounts[(int)UnitFaction.EU] = un.StartsInEU;
            FactionCounts[(int)UnitFaction.Rus] = un.StartsInRus;
        }

        // IEquatable implementation
        public bool Equals(UnitFactionNode other)
        {
            // Trival equality
            if (other == null)
            {
                return false;
            }
            
            for (int i = 0; i < 8; i++)
            {
                if (FactionCounts[i] != other.FactionCounts[i])
                {
                    return false;
                }
            }

            return Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            // Use type specific if obj is a UnitFactionNode
            if (obj is UnitFactionNode)
            {
                return Equals(obj as UnitFactionNode);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            int tempy = 0;
            foreach (SUEByte fact in FactionCounts)
            {
                tempy += fact;
            }
            return unchecked(Name.GetHashCode() + 7 * tempy);
        }
    }
}
