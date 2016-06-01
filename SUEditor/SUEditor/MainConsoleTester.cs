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
            UnitFile theFile = new UnitFile();
            string fName = "UnitTypes.dat";
            string[] theNames;
            // Try setting the file
            theFile.setFile(fName);

            Console.Out.WriteLine(theFile.showUnitCount());
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
