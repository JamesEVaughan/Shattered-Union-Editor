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

            // Try building it with the file name provided
            theFile = new UnitFile(fName);

            // Try initializing it
            try
            {
                theFile.init();
                Console.Out.WriteLine("The file was read and has {0} units", theFile.UnitCount);

                // Try and load a specific unit
                Unit curUnit = new Unit();
                theFile.loadUnit(3, curUnit);

                Console.Out.WriteLine("Unit name: " + curUnit.DisplayName.Value);
                Console.Out.WriteLine("Hit points: {0}", (short)curUnit.HitPoints);
                Console.Out.WriteLine("Vehicle Attack: {0}", (short)curUnit.ArmorAttack);
                Console.Out.WriteLine("Unit Armor Class: {0}", curUnit.UnitCat);
            }
            catch (SUE_InvalidFileException sueIFE)
            {
                Console.Out.WriteLine(sueIFE.Message);
            }

            return 0;
        }

    }
}
