using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUEditor
{
    /// <summary>
    /// This class is a simple console based testing environment for the SUEditor
    /// application. This is a simple testing ground for backend logic.
    /// </summary>
    class MainConsoleTester
    {
        static int Main(string[] args)
        {
            UnitFile theFile;
            string fName = "UnitTypes.dat";

            // Try building it with the file name provided
            theFile = new UnitFile(fName);

            // Try initializing it
            try
            {
                theFile.init();
                Console.Out.WriteLine("The file was read and has {0} units", theFile.UnitCount);
                Console.Out.WriteLine(theFile.UnitDir.buildTable());
            }
            catch (SUE_InvalidFileException sueIFE)
            {
                Console.Out.WriteLine(sueIFE.Message);
            }
            
            /*
            // Try reading the names
            theNames = theFile.getUnitNames();

            // Output them all
            foreach(string name in theNames)
            {
                Console.Out.WriteLine(name);
            }
            */
            return 0;
        }

    }
}
