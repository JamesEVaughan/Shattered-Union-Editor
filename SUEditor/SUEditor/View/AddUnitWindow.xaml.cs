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
using System.Collections.ObjectModel;

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
        public string[] UnitNames { get; set; }
        /// <summary>
        /// The name for the new unit, defaults to ""
        /// </summary>
        public NotifyString NewUnitName { get; private set; }
        /// <summary>
        /// The index of the unit to be copied, defaults to -1
        /// </summary>
        public int UnitIndex { get; private set; }

        /// <summary>
        /// Sets up our dialog window
        /// </summary>
        /// <param name="names">An array of strings for the names for all units</param>
        /// <param name="nameRules">The validation rules to be used</param>
        public AddUnitWindow(string[] names)
        {
            // Set the DataContext to this instance
            DataContext = this;

            UnitNames = names;
            NewUnitName = new NotifyString();
            UnitIndex = -1;

            // Once everything is set, initialize our components
            InitializeComponent();

            //NameRules = new ValidateDisplayName(names);


        }

        // Methods

        // Event handlers
        public void ok_click(object obj, EventArgs args)
        {
            // Add validation checks later

            // Set the properties before we close
            UnitIndex = selection_box.SelectedIndex;

            // So this should close the window...
            DialogResult = true;
        }

        public void can_click(object obj, EventArgs args)
        {
            DialogResult = false;
        }

        public void selection_change(Object obj, EventArgs args)
        {
            // Set the value of the textbox to the selection
            NewUnitName.Val = selection_box.SelectedValue as string;
        }
    }

    /// <summary>
    /// A wrapper class for a string that implements INotifyPropertyChanged
    /// </summary>
    public class NotifyString : INotifyPropertyChanged
    {
        private string val;

        public string Val
        {
            get
            {
                return val;
            }

            set
            {
                val = value;
                OnPropertyChanged("Val");
            }
        }

        public NotifyString()
        {
            val = "";
        }

        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return val;
        }

        // INotifyProperyChanged implementation
        // INotifyPropertyChanged implmentation
        protected void OnPropertyChanged(String propName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler hand = PropertyChanged;

            if (hand != null)
            {
                hand(this, args);
            }
        }
    }
}
