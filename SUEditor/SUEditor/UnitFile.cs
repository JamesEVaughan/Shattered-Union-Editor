using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SUEditor.Types;

namespace SUEditor
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
        public int UnitCount { get; set; } // Number of units in the file
        public List<UnitFileNode> UnitDir; // A list of all the units in the file


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
            UnitDir = new List<UnitFileNode>(100);
        }

        public UnitFile() :
            this("", "")
        {

        }

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
                    Types.SUEString tempName;
                    long tempUnitInd;
                    for (int i = 0; i < UnitCount; i++)
                    {
                        // Pull the index from the position of the reader
                        tempUnitInd = reader.BaseStream.Position;
                        // Read the name from the file and convert to a SUEString
                        tempName = new Types.SUEString(reader.ReadBytes(Types.SUEString.Size()));
                        UnitDir.Add(new UnitFileNode(tempName, tempUnitInd));
                        reader.BaseStream.Seek(116, SeekOrigin.Current);
                    }
                }

                catch
                {

                }
            }
        }
        
        /// <summary>
        /// Loads a unit at the specified index in UnitDir
        /// </summary>
        /// <param name="index">Index of unit in UnitDir</param>
        /// <param name="un">The Unit object for the unit to be loaded into</param>
        public void loadUnit(int index, Unit un)
        {
            // First, make sure the arguments are valid
            if (index >= UnitDir.Count)
            {
                // That's not a valid index
                return;
            }

            // Make sure un is initialized
            if (un == null)
            {
                un = new Unit();
            }

            long fileIndex = UnitDir[index].Index;   // Find where we need to be in the file

            using (BinaryReader reader = openReader())
            {
                try
                {
                    // And now the fun begins, reading in each value from the file
                    // NOTE: Values will be read in order to limit unneccessary jumping in the file
                    // Consult SUEEnums.AttributeOffset for ordered list

                    // First, move cursor to the start of the unit structure
                    reader.BaseStream.Seek(fileIndex, SeekOrigin.Begin);

                    un.DisplayName = new SUEString(reader.ReadBytes(SUEString.Size()));
                    
                    // Jump
                    reader.BaseStream.Seek(fileIndex + (long)AttributeOffeset.FuelAmt, SeekOrigin.Begin);

                    un.GasTank = reader.ReadInt32();
                    un.Speed = reader.ReadInt32();

                    // Jump
                    reader.BaseStream.Seek(fileIndex + (long)AttributeOffeset.SightRange, SeekOrigin.Begin);

                    un.Vision = reader.ReadInt16();
                    un.AirAttack = reader.ReadInt16();
                    un.ArmorAttack = reader.ReadInt16();
                    un.InfAttack = reader.ReadInt16();
                    un.Defense = reader.ReadInt16();

                    // Jump
                    reader.BaseStream.Seek(fileIndex + (long)AttributeOffeset.Health, SeekOrigin.Begin);

                    un.HitPoints = reader.ReadInt16();

                    // Jump
                    reader.BaseStream.Seek(fileIndex + (long)AttributeOffeset.UnitClass, SeekOrigin.Begin);

                    un.UnitCat = (UnitArmorClass)reader.ReadInt16();
                }
                catch 
                {
                    throw;
                }
            }
        }

        // Helpers
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
