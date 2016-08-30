using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

using SUEditor.ViewModel;
using SUEditor.View;

namespace SUEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Properties
        /// <summary>
        /// This is the our entrypoint to the Model
        /// </summary>
        public MainViewModel MainVM { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            MainVM = new MainViewModel();

            // And, finally, set our datacontext to MainVM
            this.DataContext = MainVM;
            //DisplayName.DataContext = MainVM.UnitEditor;

            // Subscribe to any events we haven't with our ViewModel
            App.Current.Exit += OnExit;

        }

        /// <summary>
        /// Event handler for the Open menu option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenClick(object sender, EventArgs e)
        {
            // Create the window handler
            OpenFileDialog openFile = new OpenFileDialog();

            // Set it to .dat files
            openFile.Filter = "Data Files|*.dat";
            // We can only open one file
            openFile.Multiselect = false;
            // No reason to show this
            openFile.ShowReadOnly = false;
            // We should start in the game's directory, if we can
            openFile.InitialDirectory = Environment.Is64BitOperatingSystem ? 
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) : // If we're 64-bit
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles); // If we're not
            // Try to set it to the default directory of the game
            if (System.IO.Directory.Exists(System.IO.Path.Combine(openFile.InitialDirectory, "PopTop Software\\Shattered Union")))
            {
                openFile.InitialDirectory = System.IO.Path.Combine(openFile.InitialDirectory, "PopTop Software\\Shattered Union");
            }

            // These bools are for controlling flow because of an exception
            bool tryAgain;   // If the user decides to not try a different file

            // This is the bulk of the method. We're wrapping it in a do-while loop
            // so that we can respond to exceptions without losing control
            do
            {
                // Safety check, only flag true after we need to
                tryAgain = false;

                bool? result = openFile.ShowDialog();

                if (!result.HasValue || !result.Value)
                {
                    // If result is null or false, do nothing.
                    return;
                }
            
                try
                {
                    MainVM.InitUnitFile(openFile.FileName);
                }
                catch (Exception err)
                {
                    if (err is SUEditor.Model.SUE_InvalidFileException)
                    {
                        // This was the wrong file, allow user to find the right file
                        String question = "\n\nWould you like to try and find the correct file?";
                        MessageBoxResult ans = MessageBox.Show((err.Message + question), 
                            "Invalid File", MessageBoxButton.YesNo);
                        if (ans == MessageBoxResult.Yes)
                        {
                            // Go back and run the file selection method
                            tryAgain = true;
                            continue;
                        }
                        else
                        {
                            // If they don't want to, we do nothing and return main app control
                            return;
                        }
                    }

                    // We're only really catching this exception for now
                    else
                    {
                        // So just throw all other exceptions
                        throw err;
                    }
                }
            } while (tryAgain);

            // And, before we go, see if the user wants to create a backup before starting
            String backupQuest = "Would you like to create a backup of the file before you begin editing?";
            MessageBoxResult backupAns = MessageBox.Show(backupQuest, "Create Backup", MessageBoxButton.YesNo);
            if (backupAns == MessageBoxResult.Yes)
            {
                // Then we'll open up a save file dialog
                SaveFileDialog backupDiag = new SaveFileDialog();
                backupDiag.DefaultExt = ".dat";
                backupDiag.Filter = "Data Files|*.dat";
                backupDiag.InitialDirectory = System.IO.Path.GetFullPath("Backups");
                backupDiag.FileName = openFile.SafeFileName; // Dirty hack to get just the file name
                backupDiag.OverwritePrompt = true;

                bool? backupResult; // If the backup file dialog completed successfully

                // Again, a little loop to allow for the useer to fix mistakes
                do
                {
                    // Only flag this as true if we need to
                    tryAgain = false;
                    backupResult = backupDiag.ShowDialog();

                    if (backupResult == true)
                    {
                        // We've got the green light, so go for it
                        MainVM.createBackup(backupDiag.FileName, true);
                    }
                    else
                    {
                        backupAns = MessageBox.Show("A backup file was not created. Would you like to continue without creating a backup file?",
                            "Backup Not Created", MessageBoxButton.YesNo);
                        if (backupAns == MessageBoxResult.No)
                        {
                            tryAgain = true;
                        }
                    }

                } while (tryAgain);
            }

            // Here, we do final setup of the validation rules
            DispVal.SetUnitList(MainVM.UnitEditor.NameList);
            UnitBox.SelectionChanged += DispVal.OnSelectionChange;
        }

        public void SaveClick(object obj, EventArgs e)
        {
            // Just run OnSave, we don't need parameters provided
            OnSave();
        }

        public void OnSave()
        {
            // Simple sanity check, if MainVM hasn't been initialized
            if (!MainVM.IsInitialized())
            {
                // Do nothing
                return;
            }
            // Force controls to update that haven't lost focus
            TextBox tempUEBox = FocusManager.GetFocusedElement(this) as TextBox;
            if (tempUEBox != null)
            {
                // Tell the object to update
                tempUEBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }

            string newFile = "";
            // First, ask if the user wants to overwrite the existing file
            string mess = "Would you like to overwrite your current UnitTypes.dat?";
            MessageBoxResult messAns = MessageBox.Show(mess, "Save and Overwrite File", MessageBoxButton.YesNoCancel);

            if (messAns == MessageBoxResult.Cancel)
            {
                // Abort!
                return;
            }
            else if (messAns == MessageBoxResult.No)
            {
                bool tryAgain;
                do
                {
                    // Always reset to false!
                    tryAgain = false;

                    // Show them a save file dialog
                    SaveFileDialog saveDiag = new SaveFileDialog();
                    saveDiag.DefaultExt = ".dat";
                    saveDiag.Filter = "Data Files|*.dat";
                    saveDiag.InitialDirectory = System.IO.Path.GetFullPath("Save Files");
                    saveDiag.OverwritePrompt = true;

                    bool? saveResult = saveDiag.ShowDialog();

                    if (saveResult == true)
                    {
                        newFile = saveDiag.FileName;
                    }
                    else
                    {
                        mess = "You didn't select a new file to save to. Would you like to overwrite your current UnitType.dat?";
                        messAns = MessageBox.Show(mess, "Did Not Select New File", MessageBoxButton.YesNoCancel);
                        if (messAns == MessageBoxResult.Cancel)
                        {
                            // Bug out!
                            return;
                        }
                        else if (messAns == MessageBoxResult.No)
                        {
                            tryAgain = true;
                        }
                    }
                } while (tryAgain);

                // Ok, we've handled the overwrite question, now to try and actually save!
                try
                {
                    MainVM.SaveUnitFile(newFile);
                }
                catch (Exception err)
                {
                    if (err is UnauthorizedAccessException || err is System.Security.SecurityException)
                    {
                        mess = "We cannot write to the specified location due to either limited access or permissions. Please " +
                            "select a different file location and try again.";
                        MessageBox.Show(mess, "Could Not Save File", MessageBoxButton.OK);
                    }
                    else
                    {
                        // Those are the only exceptions we're handling
                        throw err;
                    }
                }
            }
        }

        private void UnitBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UnitBox.SelectedItem as UnitName == null)
            {
                return;
            }

            MainVM.UnitEditor.changeSelection(UnitBox.SelectedItem as UnitName);
            Move_box.SelectedIndex = (int)MainVM.UnitEditor.MoveType;
            ArmType_box.SelectedIndex = (int)MainVM.UnitEditor.ArmorType;
            Fact_box.SelectedIndex = (MainVM.UnitEditor.Faction == Types.UnitFaction.USA) ? 8 : (int)MainVM.UnitEditor.Faction;
        }

        private void MoveBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            // Sanity check
            if (!MainVM.IsInitialized())
            {
                // Do nothing!
                return;
            }

            int tempInd = Move_box.SelectedIndex;
            MainVM.UnitEditor.MoveType = (Types.UnitMovementClass)tempInd;
        }

        public void FactBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            // Sanity check
            if (!MainVM.IsInitialized())
            {
                // Do nothing!
                return;
            }

            int tempInd = Fact_box.SelectedIndex;
            MainVM.UnitEditor.Faction = (tempInd == 8) ? Types.UnitFaction.USA : (Types.UnitFaction)tempInd;
        }

        public void ArmTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            // Sanity check
            if (!MainVM.IsInitialized())
            {
                // Do nothing!
                return;
            }

            int tempInd = ArmType_box.SelectedIndex;
            MainVM.UnitEditor.ArmorType = (Types.UnitArmorClass)tempInd;
        }

        public void OnExit(object obj, ExitEventArgs args)
        {
            // Simple sanity check, if MainVM hasn't been initialized
            if (!MainVM.IsInitialized())
            {
                // Do nothing
                return;
            }

            // Check to see if the user wants to save
            string exitMess = "Would you like to save your work before exiting?";
            MessageBoxResult ans = MessageBox.Show(exitMess, "Save before exit", MessageBoxButton.YesNo);

            // Only do stuff if the ans is yes
            if (ans == MessageBoxResult.Yes)
            {
                OnSave();
            }
        }

        public void OnAddUnit(Object obj, EventArgs args)
        {
            // Quickly build the array of strings\
            int tempCount = MainVM.UnitEditor.NameList.Count;
            string[] tempNames = new string[tempCount];
            for (int i = 0; i < tempCount; i++)
            {
                tempNames[i] = MainVM.UnitEditor.NameList[i].ViewName;
            }

            AddUnitWindow addDlg = new AddUnitWindow(tempNames);

            // And don't forget to pass along the ValidationRules!
            addDlg.NameRules.SetUnitList(MainVM.UnitEditor.NameList);

            bool? addRes = addDlg.ShowDialog();

            // Push results if the user hit "Okay"
            if (addRes == true)
            {
                MainVM.AddUnit(addDlg.UnitIndex, addDlg.NewUnitName.Val);
            }
        }

    }
}
