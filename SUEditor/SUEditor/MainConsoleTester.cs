using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUEditor.Model;
using SUEditor.Types;

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
            UnitList theList = new UnitList();
            Unit curUnit;

            // Try building it with the file name provided
            theFile = new UnitFile(fName);

            // Try initializing it
            try
            {
                theFile.init();
                Console.Out.WriteLine("The file was read and has {0} units", theFile.UnitCount);

                // Try and load all the units
                theFile.loadUnits(theList);

                Console.Out.WriteLine("The list was build and has {0}", theList.TheUnits.Count);

                for (int i = 0; i < theList.TheUnits.Count; i++)
                {
                    curUnit = theList.TheUnits[i];
                    Console.Out.WriteLine("{0} has ModelName of {1}", curUnit.DisplayName.Value, curUnit.ModelName.Value);
                    Console.Out.WriteLine("Has a Flag1or3 of {0} and Attack Range of {1:X}", curUnit.Flag1Or3.Value, curUnit.AttackRange.Value);
                }
            }
            catch (SUE_InvalidFileException sueIFE)
            {
                Console.Out.WriteLine(sueIFE.Message);
            }

            return 0;
        }

    }
}
