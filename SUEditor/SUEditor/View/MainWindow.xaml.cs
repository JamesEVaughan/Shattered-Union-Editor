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
            // We should start in the Program Files (x86) directory
            openFile.InitialDirectory = Environment.Is64BitOperatingSystem ? 
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) : // If we're 64-bit
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles); // If we're not
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
        }

        private void UnitBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UnitBox.SelectedItem as UnitName == null)
            {
                return;
            }
            string sel = (UnitBox.SelectedItem as UnitName).ViewName;

            
        }
    }
}
