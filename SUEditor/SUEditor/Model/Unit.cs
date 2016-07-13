using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUEditor.Types;

namespace SUEditor.Model
{

    /// <summary>
    /// Unit represents an individual unit from Shattered Union.
    /// </summary>
    public class Unit
    {
        // Properties
        /// <summary> The unit's name displayed in game </summary>
        public SUEString DisplayName { get; set; }
            /// <summary> The model the unit uses in game </summary>
        public SUEString ModelName { get; set; }
        /// <summary> The class used by the game engine </summary>
        public SUEString ClassName { get; set; }
        /// <summary> A flag with a value of 1 or 3 Unknown importance </summary>
        public SUEByte Flag1Or3 { get; set; }
        /// <summary> Boolean. If the unit can be bought </summary>
        public SUEByte CanBuyFlag { get; set; }
        /// <summary> The amount the unit is worth, in $1,000's </summary>
        public SUEInt Cost { get; set; }
        /// <summary> How far the unit can travel with refuelling </summary>
        public SUEInt GasTank { get; set; }
        /// <summary> How many tiles unit can travel in a single turn </summary>
        public SUEInt Speed { get; set; }
        /// <summary> How many tiles away the unit can attack </summary>
        public SUEShort AttackRange { get; set; }
        /// <summary> Boolean. If the unit is an indirect fire unit </summary>
        public SUEByte IsIndirect { get; set; }
        /// <summary> Boolean. If the unit is a Single Use unit</summary>
        public SUEByte IsSingleUse { get; set; }
        /// <summary> Boolean. If the unit is deleted after the battle </summary>
        public SUEByte IsNotKept { get; set; }
        /// <summary> Constant. Value set to "00" </summary>
        public SUEByte FlagZero { get; set; }
        /// <summary> How far the unit can see </summary>
        public SUEShort Vision { get; set; }
        /// <summary> Attack strength against flying targets </summary>
        public SUEShort AirAttack { get; set; }
        /// <summary> Attack strength against ground vehicle targets </summary>
        public SUEShort ArmorAttack { get; set; }
        /// <summary> Attack strength against infantry targets </summary>
        public SUEShort InfAttack { get; set; }
        /// <summary> Defense strength against attacks </summary>
        public SUEShort Defense { get; set; }
        /// <summary> The amount of damage the unit does to the terrain </summary>
        public SUEShort CollateralDamage { get; set; }
        /// <summary> Number of hit points the unit possesses  </summary>
        public SUEShort HitPoints { get; set; }
        /// <summary> Constant. Value set to "00 00" </summary>
        public SUEShort Flag2Zero { get; set; }
        /// <summary> Constant. Value set to "80 3F" </summary>
        public SUEShort Flag803F { get; set; }
        /// <summary> How the unit moves, used in movement calculations </summary>
        public UnitMovementClass MoveCat { get; set; }
        /// <summary> What kind of unit is this? </summary>
        public UnitArmorClass UnitCat { get; set; }
        /// <summary> An unknown flag. DON'T TOUCH! </summary>
        public SUEShort UnkFlag1 { get; set; }
        /// <summary> What faction this unit is used by </summary>
        public UnitFaction Faction { get; set; }
        /// <summary> An unknown flag. DON'T TOUCH! </summary>
        public SUEByte UnkFlag2 { get; set; }
        /// <summary> An unknown flag. DON'T TOUCH! </summary>
        public SUEByte UnkFlag3 { get; set; }
        /// <summary> How many units the New England Alliance starts with in campaign mode </summary>
        public SUEByte StartsInNEA { get; set; }
        /// <summary> How many units the Confederacy starts with in campaign mode </summary>
        public SUEByte StartsInCon { get; set; }
        /// <summary> How many units the Great Plains Federation starts with in campaign mode </summary>
        public SUEByte StartsInGPF { get; set; }
        /// <summary> How many units the Republic of Texas starts with in campaign mode </summary>
        public SUEByte StartsInRoT { get; set; }
        /// <summary> How many units California starts with in campaign mode </summary>
        public SUEByte StartsInCal { get; set; }
        /// <summary> How many units Pacifica starts with in campaign mode </summary>
        public SUEByte StartsInPac { get; set; }
        /// <summary> How many units the EU starts with in campaign mode </summary>
        public SUEByte StartsInEU { get; set; }
        /// <summary> How many units Russia starts with in campaign mode </summary>
        public SUEByte StartsInRus { get; set; }

        // Constructors
        public Unit(SUEString dn) :
            this()
        {
            DisplayName = new SUEString(dn);
        }

        public Unit()
        {
            DisplayName = new SUEString();
            ModelName = new SUEString();
            ClassName = new SUEString();
            Flag1Or3 = 1;   // Default to 1, because most units are 1
            CanBuyFlag = 0;
            Cost = 0;
            GasTank = 0;
            Speed = 0;
            AttackRange = 0;
            IsIndirect = 0;
            IsSingleUse = 0;
            IsNotKept = 0;
            FlagZero = 0;
            Vision = 0;
            AirAttack = 0;
            ArmorAttack = 0;
            InfAttack = 0;
            Defense = 0;
            CollateralDamage = 0;
            HitPoints = 0;
            Flag2Zero = 0;
            Flag803F = 0;
            MoveCat = UnitMovementClass.Footl;  // Default to first UnitMovementClass value
            UnitCat = UnitArmorClass.Air; // Default to the first UnitArmorClass value
            UnkFlag1 = 0;
            Faction = UnitFaction.NEA;  // Default to first UnitFaction value
            UnkFlag2 = 0;
            UnkFlag3 = 0;
        }
    }
}
