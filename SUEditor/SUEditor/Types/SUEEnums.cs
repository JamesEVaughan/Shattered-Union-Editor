using System;
using System.Text;

namespace SUEditor.Types
{

    /// <summary>
    /// Offset to find a specific attribute of an individual unit in file
    /// </summary>
    enum AttributeOffeset : long
    {
        DispName = 0x00L,   /// The name displayed in game
        ModelName = 0x1EL,  // The name of the model used
        ClassName = 0x3CL,  // The name of the class used internally
        Flag1or3 = 0x5AL,    // This flag has an unknown use, see UnitTypesFormat.ods
        CanBuy = 0x5BL,      // If the unit can be bought
        Cost = 0x5CL,        // The cost of the unit, in $1,000's
        FuelAmt = 0x60L,    // The maximum amount of fuel a unit can store
        MoveRange = 0x64L,  // The number of tiles a unit can move in a single turn
        AttackRange = 0x68L, // The number of tiles away the unit can attack
        IndirectFire = 0x6AL, // If the unit is an inderect fire unit, meaning it is unable to fire on adjacent tiles
        SingleUse = 0x6BL,  // If the unit is classified as "Single Use"
        DeleteAfterUse = 0x6CL, // If the unit is deleted at the end of the battle
        ConstFlagZero = 0x6DL,  // Space left blank, proably for padding
        SightRange = 0x6EL, // The number of tiles a unit can see
        AttackAir = 0x70L,  // The unit's attack strength against "Air" units
        AttackVeh = 0x72L,  // The unit's attack strength against "Vehicle" units
        AttackInf = 0x74L,  // The unit's attack strength against "Infantry units
        Defense = 0x76L,    // The unit's defense against attacks
        CollDam = 0x78L,    // The unit's damage to the terrain
        Health = 0x7AL,     // The number of hit points a unit possesses
        Flag2Zero = 0x7CL,  // Space left blank
        Flag803F = 0x7EL,   // Set to "80 3F" for all units
        MovementType = 0x80L, // The type of movement used, see UnitMovementClass enumerator
        UnitClass = 0x82L,  // The class a unit belongs to, see UnitArmorClass enumerator
        UnkFlag1 = 0x84L, // This flag's purpose is unknown, likely used to help AI command units
        Faction = 0x86L,    // The unit's faction alignment, see UnitFaction enumerator 
        UnkFlag2 = 0x87L,   // This flag's purpose is unknown, known values range from 00 to 0E
        UnkFlag3 = 0x88L,   // This flag's purpose is unknown, know values range from 00 through 09
        StartsWithNEA = 0x89L, // The number of this unit that the New England Alliance starts with in campaign mode
        StartsWithCon = 0x8AL, // The number of this unit that the Confederacy starts with in campaign mode
        StartsWithGPF = 0x8BL, // The number of this unit that the Great Plains Federation starts with in campaign mode
        StartsWithRoT = 0x8CL, // The number of this unit that Texas starts with in campaign mode
        StartsWithCal = 0x8DL, // The number of this unit that California starts with in campaign mode
        StartsWithPac = 0x8EL, // The number of this unit that Pacifica starts with in campaign mode
        StartsWithEU = 0x8FL, // The number of this unit that the EU starts with in campaign mode
        StartsWithRus = 0x90L, // The number of this unit that Russia starts with in campaign mode
    }

    /// <summary>
    /// Defines the  unit classes available. Used to determine the which attack value
    /// is used during combat
    /// </summary>
    public enum UnitArmorClass : short
    {
        Air = 0x00, // An air unit, such as a plane or helicopter
        Vehicle = 0x01, // A vehicular unit, such as a car or heavy tank
        Infantry = 0x02, // An infantry unit, such as a squad of soldiers or towed gun
    }

    /// <summary>
    /// Flag used in file to determine which faction(s) have access to a given unit
    /// </summary>
    public enum UnitFaction : byte
    {
        NEA = 0x00, // New England Alliance
        Con = 0x01, // The Confederacy
        GPF = 0x02, // The Great Plains Federation
        RoT = 0x03, // Republic of Texas
        Cal = 0x04, // The California Commonwealth
        Pac = 0x05, // Pacifica
        EU = 0x06,  // European Union
        Rus = 0x07, // Russia
        USA = 0xFE, // All US Factions
    }

    /// <summary>
    /// Defines the movement type used by the unit for movement calculations. Names are taken
    /// from the MainINIFile. Unusual spelling has no known reasoning.
    /// </summary>
    public enum UnitMovementClass : short
    {
        Footl = 0x00,   // This unit walks everywhere, e.g. an infantry squad
        Wheed = 0x01,   // This unit uses a number of wheels, e.g. a car
        Tread = 0x02,   // This unit uses treads, e.g. a tank
        Towee = 0x03,   // This unit is towed by another vehicle, e.g. a trailer
        Planr = 0x04,   // This unit flys using fixed-wings, e.g. a plane
        Copt = 0x05,    // This unit flys using rotary-wings, e.g. a helicopter
        Dummy = 0x06,   // This unit is stationary, e.g. a building
    }
}