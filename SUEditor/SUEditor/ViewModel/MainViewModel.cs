using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        /// This is the view model for the UnitEditor tab.
        /// </summary>
        public UnitEditorVM UnitEditor { get; private set; }

        // Constructors
        /// <summary>
        /// Basic default constructor. Must make separate method calls to initialize the properties
        /// </summary>
        public MainViewModel()
        {
            // Initialize our properties
            MainUnitFile = null; // UnitFile is initialized in InitMainUnitFile
            UnitEditor = new UnitEditorVM();

            // Subscribe to our events
            UnitEditor.PropertyChanged += OnUnitTabChange;

            // As this is the entry point of the program, we should handle the some amount of
            // file directory work we need done.
            System.IO.Directory.CreateDirectory("Backups"); // Sets up a folder for backup files
            System.IO.Directory.CreateDirectory("Save Files"); // Sets up a folder for saving files
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
                    UnitEditor.NameList.Add(new UnitName(node.TheUnit, node.Index));
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

        /// <summary>
        /// Save the current unit file to disk. Will overwrite existing file unless newFileLoc is overwritten
        /// </summary>
        /// <param name="newFileLoc">Where to save the current unit file. Default is an empty string.
        /// Will overwrite existing file if not overwritten.</param>
        public void SaveUnitFile(string newFileLoc = "")
        {
            try
            {
                if (newFileLoc != "")
                {
                    MainUnitFile.SetWriteFile(newFileLoc);
                }
                MainUnitFile.saveUnitFile();
            }
            catch
            {
                // Pass any exceptions to the controlling method
                throw;
            }
        }

        /// <summary>
        /// Adds a new unit to the unit file
        /// </summary>
        /// <param name="unInd">The index of the unit to be cloned</param>
        /// <param name="newName">The name of the unit</param>
        public void AddUnit(int unInd, string newName)
        {
            // First, find the unit to be cloned
            Unit tempy = UnitEditor.NameList[unInd].TheUnit;

            // Pass the info to MainUnitFile
            long tempInd = MainUnitFile.UnitDir.CloneUnit(tempy, newName);

            // Now set tempy to the newly formed unit
            tempy = MainUnitFile.UnitDir.TheUnits.Find(x => x.Index == tempInd).TheUnit;

            // Now, if everything went right, we should have a new unit in UnitDir
            // Add it to UnitEditorVM
            UnitEditor.NameList.Add(new UnitName(tempy, tempInd));
        }

        // Event delegates
        protected void OnUnitTabChange(object obj, EventArgs args)
        {
            // If obj isn't a UnitEditorVM...
            UnitEditorVM tempUE = obj as UnitEditorVM;
            if (tempUE == null)
            {
                // ...Don't do anything
                return;
            }

            if (tempUE.IsInitialLoad)
            {
                // Ignore, no values are actually being changed
                return;
            }

            // This switch handles the bulk of our updating to the model
            string propChanged = (args as PropertyChangedEventArgs).PropertyName;
            switch (propChanged)
            {
                case "DispName":
                    tempUE.CurUnitName.TheUnit.DisplayName.Value = tempUE.DispName;
                    // Also, propogate changes back to the NameList
                    tempUE.CurUnitName.ViewName = tempUE.DispName;
                    break;
                case "Cost":
                    tempUE.CurUnitName.TheUnit.Cost.Value = tempUE.Cost;
                    break;
                case "Movement":
                    tempUE.CurUnitName.TheUnit.Speed.Value = tempUE.Movement;
                    break;
                case "SightRange":
                    tempUE.CurUnitName.TheUnit.Vision = tempUE.SightRange;
                    break;
                case "AirAtt":
                    tempUE.CurUnitName.TheUnit.AirAttack.Value = tempUE.AirAtt;
                    break;
                case "VehAtt":
                    tempUE.CurUnitName.TheUnit.ArmorAttack.Value = tempUE.VehAtt;
                    break;
                case "InfAtt":
                    tempUE.CurUnitName.TheUnit.InfAttack.Value = tempUE.InfAtt;
                    break;
                case "Def":
                    tempUE.CurUnitName.TheUnit.Defense.Value = tempUE.Def;
                    break;
                case "Health":
                    tempUE.CurUnitName.TheUnit.HitPoints.Value = tempUE.Health;
                    break;
                case "AttRange":
                    tempUE.CurUnitName.TheUnit.AttackRange.Value = tempUE.AttRange;
                    break;
                case "Fuel":
                    tempUE.CurUnitName.TheUnit.GasTank.Value = tempUE.Fuel;
                    break;
                case "CollDamage":
                    tempUE.CurUnitName.TheUnit.CollateralDamage.Value = tempUE.CollDamage;
                    break;
                case "IsIndirect":
                    tempUE.CurUnitName.TheUnit.IsIndirect = tempUE.IsIndirect;
                    break;
                case "IsSingleUse":
                    tempUE.CurUnitName.TheUnit.IsSingleUse = tempUE.IsSingleUse;
                    break;
                case "IsNotKept":
                    tempUE.CurUnitName.TheUnit.IsNotKept = tempUE.IsNotKept;
                    break;
                case "CanBuy":
                    tempUE.CurUnitName.TheUnit.CanBuyFlag = tempUE.CanBuy;
                    break;
            }
        }

        /// <summary>
        /// Because MainViewModel isn't fully initialized via its constructor, this tells whoever
        /// asks if this instance has been fully initialized.
        /// </summary>
        /// <returns>True if this instance is initialized and can be used. False otherwise.</returns>
        public bool IsInitialized()
        {
            // The easiest test is if MainUnitFile has been initialized.
            return !(MainUnitFile == null);
        }
     }
}
