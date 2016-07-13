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

            bool? result = openFile.ShowDialog();

            if (result == true)
            {
                try
                {
                    MainVM.InitUnitFile(openFile.FileName);
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
