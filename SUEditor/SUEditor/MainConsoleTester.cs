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
            Unit curUnit;

            // Try building it with the file name provided
            theFile = new UnitFile(fName);

            // Try initializing it
            try
            {
                theFile.init();
                Console.Out.WriteLine("The file was read and has {0} units", theFile.UnitCount);

                List<UnitFactionNode> theFacts = theFile.UnitDir.GetStartingUnits();
                Console.Out.WriteLine("______________________________ NEA\tCon\tGPF\tRoT\tCal\tPac\tEU\tRUS");
                foreach (UnitFactionNode fact in theFacts)
                {
                    Console.Out.WriteLine("{0,-30} {1,5} {2,5} {3,5} {4,5} {4,5} {5,5} {6,5} {7,5} {8,5}",
                        fact.Name.Value,
                        fact.FactionCounts[(int)UnitFaction.NEA].Value,
                        fact.FactionCounts[(int)UnitFaction.Con].Value,
                        fact.FactionCounts[(int)UnitFaction.GPF].Value,
                        fact.FactionCounts[(int)UnitFaction.RoT].Value,
                        fact.FactionCounts[(int)UnitFaction.Cal].Value,
                        fact.FactionCounts[(int)UnitFaction.Pac].Value,
                        fact.FactionCounts[(int)UnitFaction.EU].Value,
                        fact.FactionCounts[(int)UnitFaction.Rus].Value);
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
