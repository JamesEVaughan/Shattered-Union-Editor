﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        private bool isLocal;           // Is UnitTypes.dat local
        private string fileLocation;    // The location of the UnitTypes.dat
        private string fileName;        // The name of the file to be edited
        private UnitList unitDir;       // A list of all the units in the file

        // Properties
        public int UnitCount { get; set; } // Number of units in the file
        public UnitList UnitDir => unitDir;
        

        // Constructors
        public UnitFile(string fn) : 
            this(fn, "")
        {
            isLocal = true;
        }

        public UnitFile(string fn, string loc)
        {
            fileName = fn;
            fileLocation = loc;
            isLocal = false;
            UnitCount = 0;
            unitDir = new UnitList();
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

            using (BinaryReader reader = new BinaryReader(new FileStream(Path.Combine(fileLocation, fileName), FileMode.Open, FileAccess.Read)))
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
                    Types.Name tempName;
                    byte[] tempBytes = new byte[29];
                    long tempUnitInd;
                    for (int i = 0; i < UnitCount; i++)
                    {
                        // Pull the index from the position of the reader
                        tempUnitInd = reader.BaseStream.Position;
                        // Read the name from the file
                        tempBytes = reader.ReadBytes(Types.Name.Size());
                        // Convert the byte array to a Name
                        tempName = new Types.Name(System.Text.Encoding.ASCII.GetString(tempBytes));
                        unitDir.addNode(tempName, tempUnitInd);
                        reader.BaseStream.Seek(116, SeekOrigin.Current);
                    }
                }

                catch
                {

                }
            }
        }
    }
}
