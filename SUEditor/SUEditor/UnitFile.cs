using System;
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
        // The name of the file this class will manipulate.

        // Fields
        private bool isLocal;           // Is UnitTypes.dat local
        private string fileLocation;    // The location of the UnitTypes.dat
        private string fileName;        // The name of the file to be edited
        private int unitCount;          // Number of units in the file

        // Constructors
        /* Functionality to be added later
        public UnitFile(string loc)
        {
            fileLocation = loc;
            isLocal = false;
        }
        */

        public UnitFile()
        {
            fileLocation = "";
            fileName = "";
            isLocal = false;
            unitCount = -1;
        }

        public void setFile(string fn)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.OpenRead(fn), Encoding.UTF8))
                {
                    // Confirm the 2 int header, first is 8...
                    if (reader.ReadInt32() == 8)
                    {
                        // ... second is the number of entries
                        unitCount = reader.ReadInt32();
                    }
                }
            }
            catch
            {
            }
        }

        public string[] getUnitNames()
        {
            string[] names = new string[unitCount];
            int curPos = 0x91;

            using (BinaryReader reader = new BinaryReader(File.OpenRead(fileName), Encoding.UTF8))
            {
                // First, offset the header
                reader.BaseStream.Seek(0x8, SeekOrigin.Begin);
                for (int i = 0; i < unitCount; i++)
                {
                    // Sanity check!
                    if ((reader.BaseStream.Position + 0x91) > reader.BaseStream.Length)
                        return null;
                    reader.BaseStream.Seek(0x91, SeekOrigin.Current);
                    names[i] = reader.ReadString();
                    // Reset the position!
                    reader.BaseStream.Seek(-names[i].Length, SeekOrigin.Current);
                }
            }

            return names;
        }

        public int showUnitCount()
        {
            return unitCount;
        }
    }
}
