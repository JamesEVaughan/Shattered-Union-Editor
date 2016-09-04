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
    public class UnitName : INotifyPropertyChanged, IEquatable<UnitName>
    {
        // Fields
        /// <summary>
        /// A reference back to the Unit this represents
        /// </summary>
        private Unit theUnit;
        /// <summary>
        /// This is the name that is displayed by the app. Acts as field behind the ViewName
        /// property.
        /// </summary>
        private string viewName;
        /// <summary>
        /// File index of theUnit. Used for finding it in the UnitList
        /// </summary>
        private long index;

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

        public long Index
        {
            get { return index; }
        }

        // Constructors
        public UnitName(Unit u, long ind)
        {
            theUnit = u;
            viewName = u.DisplayName.Value;
            index = ind;
        }

        // Methods
        /// <summary>
        /// Listener for PropertChanged events
        /// </summary>
        /// <param name="obj">The object that fired the event</param>
        /// <param name="args">The arguments sent from the event</param>
        public void OnNameChange(object obj, PropertyChangedEventArgs args)
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

            // Lastly, we're only looking to update the name here
            if (args.PropertyName.Equals("DispName"))
            {
                ViewName = tempUE.DispName;
            }
        }

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

        // Implementation of IEquitable
        public override bool Equals(object obj)
        {
            // Use our equals function if obj is a UnitName
            UnitName tempUN = obj as UnitName;
            if (tempUN != null)
            {
                return Equals(tempUN);
            }
            return base.Equals(obj);
        }

        public bool Equals(UnitName uName)
        {
            return (index == uName.Index) && (viewName == uName.ViewName);
        }

        public override int GetHashCode()
        {
            // We're using ModelName over the view name because it's unlikely to change
            return unchecked(theUnit.ModelName.GetHashCode() + 7 * (int)index);
        }
    }
}
