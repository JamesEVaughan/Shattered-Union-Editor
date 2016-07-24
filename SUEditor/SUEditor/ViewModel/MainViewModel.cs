using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SUEditor.Types;
using SUEditor.Model;

namespace SUEditor.ViewModel
{
    /// <summary>
    /// This is the central class for the ViewModel and specifically acts as the interface between
    /// the Model and the rest of the ViewModel.
    /// </summary>
    public class MainViewModel
    {
        // Fields

        // Properties
        /// <summary>
        /// This is our doorway to the model for dealing with units.
        /// </summary>
        public UnitFile MainUnitFile { get; private set; }
        /// <summary>
        /// A list of the names for all units we currently have
        /// </summary>
        public ObservableCollection<UnitName> NameList { get; private set; }
        /// <summary>
        /// This is the view model for the UnitEditor tab.
        /// </summary>
        public UnitEditorVM UnitEditor { get; private set; }

        // Constructors
        /// <summary>
        /// Basic default constructor. Must make separate method calls to initialize the properties
        /// </summary>
        public MainViewModel()
        {
            // Oddly enough, we don't initialize anything here. We have to wait for the user to
            // give us a file to work with. So set everything to null to be safe.
            MainUnitFile = null;
            NameList = new ObservableCollection<UnitName>();
            UnitEditor = new UnitEditorVM();

            // As this is the entry point of the program, we should handle the some amount of
            // file directory work we need done.
            System.IO.Directory.CreateDirectory("Backups"); // Sets up a folder for backup files
        }

        // Methods
        /// <summary>
        /// Initializes MainUnitFile with the given file name.
        /// </summary>
        /// <param name="fName">The name of the file</param>
        public void InitUnitFile(string filePath)
        {
            /// We're wrapping most of this method in a try-catch block to catch file exceptions.
            /// This will allow the app to quickly recover from incorrect file selections
            try
            {
                // First things first, initialize MainUnitFile
                MainUnitFile = new UnitFile(filePath);
                MainUnitFile.init();

                // Now that we've initialized the file, pull out the names for each unit
                foreach (UnitFileNode node in MainUnitFile.UnitDir.TheUnits)
                {
                    NameList.Add(new UnitName(node.TheUnit));
                }
            }
            catch (SUE_InvalidFileException ivfe)
            {
                // Send message to user, redo file open.
                throw ivfe;
            }

        }

        /// <summary>
        /// This method creates a backup of the currently selected file at the specified location.
        /// Can force an overwrite if desired.
        /// </summary>
        /// <param name="backupLoc">File location for the backup file to be written to</param>
        /// <param name="overwrite">Default false, if the method should overwrite an existing file</param>
        /// <returns>True a backup was created, false otherwise</returns>
        public bool createBackup(string backupLoc, bool overwrite = false)
        {
            // This is really just to expose UnitFile.createBackup to the app at large.
            return MainUnitFile.createBackup(backupLoc, overwrite);
        }
     }
}
