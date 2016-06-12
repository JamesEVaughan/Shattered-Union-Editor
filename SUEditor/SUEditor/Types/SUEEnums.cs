using System;
using System.Text;

namespace SUEditor.Types
{
    /// <summary>
    /// Flag used in file to determine which faction(s) have access to a given unit
    /// </summary>
    enum Faction : byte
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
    /// Offset to find a specific attribute of an individual unit in file
    /// </summary>
    enum AttributeOffeset : long
    {
        DispName = 0x00L,   // The name displayed in game
        ModelName = 0x1EL,  // The name of the model used
        ClassName = 0x3CL,  // The name of the class used internally
        FuelAmt = 0x60L,    // The maximum amount of fuel a unit can store
        MoveRange = 0x64L,  // The number of tiles a unit can move in a single turn
        SightRange = 0x6EL, // The number of tiles a unit can see
        AttackAir = 0x70L,  // The unit's attack strength against "Air" units
        AttackVeh = 0x72L,  // The unit's attack strength against "Vehicle" units
        AttackInf = 0x74L,  // The unit's attack strength against "Infantry units
        Defense = 0x76L,    // The unit's defense against attacks
        Health = 0x7AL,     // The number of hit points a unit possesses
        UnitClass = 0x82L,  // The class a unit belongs to, see UnitArmorClass enumerator
    }

    /// <summary>
    /// Defines the available unit classes available. Used to determine the which attack
    /// value is used during combat
    /// </summary>
    enum UnitArmorClass : short
    {
        UnitAir = 0x00, // An air unit, such as a plane or helicopter
        UnitInf = 0x01, // An infantry unit, such as a squad of soldiers or towed gun
        UnitVeh = 0x03, // A vehicular unit, such as a car or heavy tank
    }
}