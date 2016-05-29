using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor
{
    using Types;

    class Unit
    {
        /*
         * Unit represents an individual unit from Shattered Union.
         */

        // Data members
        private Name displayName;   // Name displayed in game
        private Short airAttack;    // Attack strength against flying targets
        private Short armorAttack;  // Attack strength against ground vehicle targets
        private Short infAttack;    // Attack strength against infantry targets

        // Properties
        public string DisplayName
        {
            get
            {
                return displayName.Value;
            }

            set
            {
                displayName.Value = value;
            }
        }

        public short AirAttack
        {
            get
            {
                return airAttack.Value;
            }
            set
            {
                airAttack.Value= value;
            }
        }

        public short ArmorAttack
        {
            get
            {
                return armorAttack.Value;
            }
            set
            {
                armorAttack.Value = value;
            }
        }

        public short InfAttack
        {
            get
            {
                return infAttack.Value;
            }
            set
            {
                infAttack.Value = value;
            }
        }

        // Constructors
        public Unit(Name dn, Short air, Short arm, Short inf)
        {
            displayName = new Name(dn);
            airAttack = new Short(air);
            armorAttack = new Short(arm);
            infAttack = new Short(inf);
        }

        public Unit()
        {
            displayName = new Name();
            airAttack = new Short(0);
            armorAttack = new Short(0);
            infAttack = new Short(0);
        }
    }
}
