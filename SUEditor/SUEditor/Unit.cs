using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor
{
    using Types;

    /// <summary>
    /// Unit represents an individual unit from Shattered Union.
    /// </summary>
    class Unit
    {
        // Properties
        public SUEString DisplayName;   // SUEString displayed in game
        public SUEShort AirAttack;      // Attack strength against flying targets
        public SUEShort ArmorAttack;    // Attack strength against ground vehicle targets
        public SUEShort InfAttack;      // Attack strength against infantry targets
        public SUEShort HitPoints;      // Number of hit points the unit possesses
        public SUEShort Defense;        // Defense strength against attacks
        public SUEShort Vision;         // How far the unit can see
        public SUEInt GasTank;          // How far the unit can travel with refuelling
        public SUEInt Speed;            // How many tiles unit can travel in a single turn
        public UnitArmorClass UnitCat;  // What kind of unit is this?
        
        // Constructors
        public Unit(SUEString dn) :
            this()
        {
            DisplayName = new SUEString(dn);
        }

        public Unit()
        {
            DisplayName = new SUEString();
            AirAttack = 0;
            ArmorAttack = 0;
            InfAttack = 0;
            HitPoints = 0;
            Defense = 0;
            Vision = 0;
            GasTank = 0;
            Speed = 0;
            UnitCat = UnitArmorClass.UnitAir;
        }
    }
}
