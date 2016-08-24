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
using System.Windows.Shapes;
using System.ComponentModel;

using SUEditor.ViewModel;

namespace SUEditor.View
{
    /// <summary>
    /// Interaction logic for AddUnitWindow.xaml
    /// </summary>
    public partial class AddUnitWindow : Window
    {
        // Properties
        /// <summary>
        /// A list of the names of units available to be copied
        /// </summary>
        public List<string> UnitNames { get; set; }
        /// <summary>
        /// The name for the new unit, defaults to ""
        /// </summary>
        public string NewUnitName { get; private set; }
        /// <summary>
        /// The index of the unit to be copied, defaults to -1
        /// </summary>
        public int UnitIndex { get; private set; }

        /// <summary>
        /// Default constructor, don't use
        /// </summary>
        public AddUnitWindow(string[] names)
        {
            // Set the DataContext to this instance
            DataContext = this;

            // Default to an empty list
            UnitNames = new List<string>(names);
            NewUnitName = "";
            UnitIndex = -1;

            // Once everything is set, initialize our components
            InitializeComponent();


        }

        // Methods

        // Event handlers
        public void ok_click(object obj, EventArgs args)
        {
            // Add validation checks later

            // Set the properties before we close
            UnitIndex = selection_box.SelectedIndex;
            // This only really needs to be done once, so no reason to wire it up
            NewUnitName = name_txtbox.Text;

            // So this should close the window...
            DialogResult = true;
        }

        public void selection_change(Object obj, EventArgs args)
        {
            // Set the value of the textbox to the selection
            name_txtbox.Text = selection_box.SelectedValue as string;
        }
    }
}
