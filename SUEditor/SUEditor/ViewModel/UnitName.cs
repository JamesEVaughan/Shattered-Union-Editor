using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using SUEditor.Model;
using SUEditor.Types;

namespace SUEditor.ViewModel
{
    /// <summary>
    /// UnitName wraps up the name of a unit and implements INotifyPropertyChange. Acts as a node
    /// in an ObserveableCollection.
    /// </summary>
    public class UnitName : INotifyPropertyChanged
    {
        // Fields
        private Unit theUnit;
        /// <summary>
        /// This is the name that is displayed by the app. Acts as field behind the ViewName
        /// property.
        /// </summary>
        private string viewName;

        // Properties
        /// <summary>
        /// Event handler for PropertyChanged events
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public string ViewName
        {
            get { return viewName; }
            set
            {
                if (value != viewName)
                {
                    viewName = value;
                    OnPropertyChanged("ViewName");
                }
            }
        }

        public Unit TheUnit
        {
            get { return theUnit; }
        }

        // Constructors
        public UnitName(Unit u)
        {
            theUnit = u;
            viewName = u.DisplayName.Value;
        }

        // Methods

        // INotifyPropertyChanged Methods
        protected void OnPropertyChanged(string propName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler hand = PropertyChanged;

            if(hand != null)
            {
                hand(this, args);
            }
        }

        public void OnNameChange (object obj, PropertyChangedEventArgs args)
        {
            // First, we need to be dealing with the right stuff
            UnitEditorVM tempUE = obj as UnitEditorVM;
            if (tempUE == null)
            {
                // Not what we're looking for
                return;
            }

            if (tempUE.IsInitialLoad)
            {
                // We're just changing to a different unit, ignore
                return;
            }

            // Lastly, we're only looking to undate the name here
            if (args.PropertyName.Equals("DispName"))
            {
                ViewName = tempUE.DispName;
            }
        }
    }
}
