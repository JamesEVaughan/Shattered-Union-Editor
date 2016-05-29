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
        private const string fileName = "UnitTypes.dat";

        // Fields
        private bool isLocal;           // Is UnitTypes.dat local
        private string fileLocation;    // The location of the UnitTypes.dat
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
            isLocal = true;

            using (BinaryReader br = new BinaryReader(File.OpenRead(fileName)))
            {
                int temp;
                temp = br.ReadInt32();
                if (temp != 8)
                {
                    unitCount = 0;
                    return;
                }

                unitCount = br.ReadInt32();
            }
        }
    }
}
