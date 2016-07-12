using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SUEditor.Types;

namespace SUEditor.Model
{
    /// <summary>
    /// UnitFile respresents the UnitTypes.dat file. It is capable of reading and writing
    /// to the file.
    /// </summary>
    class UnitFile
    {
        // Constants
        private const int MAGIC_NUMBER = 0x08;  // The magic number for UnitFile.dat

        // Fields
        private string fileLocation;        // The location of the UnitTypes.dat
        private string fileName;            // The name of the file to be edited

        // Properties
        /// <summary>
        /// Number of units in the file
        /// </summary>
        public int UnitCount { get; private set; }
        /// <summary>
        ///  A list of all the units in the file
        /// </summary>
        public UnitList UnitDir { get; private set; }


        // Constructors
        public UnitFile(string fn) : 
            this(fn, "")
        {

        }

        public UnitFile(string fn, string loc)
        {
            fileName = fn;
            fileLocation = loc;
            UnitCount = 0;
            // Unmodified UnitFile.dat contains 86 entries, 100 should limit unnecessary resizing
            UnitDir = new UnitList(100);
        }

        public UnitFile() :
            this("", "")
        {

        }

        // Methods

        /// <summary>
        /// This method handles the final intialization of UnitFile by confirming that
        /// the file pointed to by fileName & fileLocation is correctly formatted. Throws
        /// SUE_InvalidFileException if file check fails.
        /// </summary>
        public void init()
        {
            /// This method handles the final intialization of UnitFile by confirming that
            /// the file pointed to by fileName & fileLocation is correctly formatted. Throws
            /// SUE_InvalidFileException if file check fails.

            // First, simple sanity checks
            if (fileName == "")
            {
                // We've gotta have a file to open...
                throw new SUE_InvalidFileException("No file name and/or path given.");
            }

            if (UnitCount > 0)
            {
                // We've already done this, dude.
                return;
            }

            using (BinaryReader reader = openReader())
            {
                try
                {
                    int temp = reader.ReadInt32();
                    if (temp != MAGIC_NUMBER)
                    {
                        // This is the wrong file
                        throw new SUE_InvalidFileException("Invalid UnitFile.dat");
                    }
                    UnitCount = reader.ReadInt32();

                    // Now that we know how many units are here, we should check the size
                    temp = (UnitCount * 145) + 8;   // File has an 8-byte header and 145 bytes per unit
                    if (reader.BaseStream.Length != temp)
                    {
                        // The file is not the right size
                        throw new SUE_InvalidFileException("Invalid UnitFile.dat");
                    }

                    // Finally, build the UnitDir
                    long tempUnitInd;
                    tempUnitInd = reader.BaseStream.Position;
                    for (int i = 0; i < UnitCount; i++)
                    {
                        // Pull the index from the position of the reader
                        UnitDir.TheUnits.Add(new UnitFileNode(loadUnit((long)tempUnitInd, reader), tempUnitInd));

                        // Simple sanity check
                        tempUnitInd += 145;
                    }
                }

                catch
                {

                }
            }
        }

        // Helpers
        /// <summary>
        /// Loads a unit at the specified index in UnitDir
        /// </summary>
        /// <param name="index">Index of unit in the file</param>
        /// <param name="br">An open and active BinaryReader</param>
        /// <returns>The Unit object for the unit to be loaded into</returns>
        private Unit loadUnit(long index, BinaryReader br)
        {
            Unit un = new Unit();

            // First, make sure the arguments are valid
            if (index >= br.BaseStream.Length)
            {
                // That's not a valid index
                return null;
            }
            
            // And now the fun begins, reading in each value from the file
            // Consult SUEEnums.AttributeOffset for ordered list

            // First, move cursor to the start of the unit structure
            br.BaseStream.Seek(index, SeekOrigin.Begin);

            un.DisplayName.ByteArraySetter(br.ReadBytes(SUEString.Size));
            un.ModelName.ByteArraySetter(br.ReadBytes(SUEString.Size));
            un.ClassName.ByteArraySetter(br.ReadBytes(SUEString.Size));
            un.Flag1Or3 = br.ReadByte();
            un.CanBuyFlag = br.ReadByte();
            un.Cost = br.ReadInt32();
            un.GasTank = br.ReadInt32();
            un.Speed = br.ReadInt32();
            un.AttackRange = br.ReadInt16();
            un.IsIndirect = br.ReadByte();
            un.IsSingleUse = br.ReadByte();
            un.IsNotKept = br.ReadByte();
            un.FlagZero = br.ReadByte();
            un.Vision = br.ReadInt16();
            un.AirAttack = br.ReadInt16();
            un.ArmorAttack = br.ReadInt16();
            un.InfAttack = br.ReadInt16();
            un.Defense = br.ReadInt16();
            un.CollateralDamage = br.ReadInt16();
            un.HitPoints = br.ReadInt16();
            un.Flag2Zero = br.ReadInt16();
            un.Flag803F = br.ReadInt16();
            un.MoveCat = (UnitMovementClass)br.ReadInt16();
            un.UnitCat = (UnitArmorClass)br.ReadInt16();
            un.UnkFlag1 = br.ReadInt16();
            un.Faction = (UnitFaction)br.ReadByte();
            un.UnkFlag2 = br.ReadByte();
            un.UnkFlag3 = br.ReadByte();
            un.StartsInNEA = br.ReadByte();
            un.StartsInCon = br.ReadByte();
            un.StartsInGPF = br.ReadByte();
            un.StartsInRoT = br.ReadByte();
            un.StartsInCal = br.ReadByte();
            un.StartsInPac = br.ReadByte();
            un.StartsInEU = br.ReadByte();
            un.StartsInRus = br.ReadByte();

            return un;
        }

        /// <summary>
        /// Opens the file for reading using BinaryReader
        /// </summary>
        /// <returns>A BinaryReader object set to Read</returns>
        private BinaryReader openReader()
        {
            return new BinaryReader(new FileStream(Path.Combine(fileLocation, fileName), FileMode.Open, FileAccess.Read));
        }

        /// <summary>
        /// Opens the file for writing using BinaryWriter
        /// </summary>
        /// <returns>A BinaryWriter object set to Write</returns>
        private BinaryWriter openWriter()
        {
            return new BinaryWriter(new FileStream(Path.Combine(fileLocation, fileName), FileMode.Open, FileAccess.Write));
        }
    }
}
