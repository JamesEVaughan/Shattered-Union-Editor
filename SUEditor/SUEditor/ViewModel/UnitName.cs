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
        /// <summary>
        /// A reference back to the DisplayName of the unit it represents
        /// </summary>
        SUEString modelName;
        /// <summary>
        /// This is the name that is displayed by the app. Acts as field behind the ViewName
        /// property.
        /// </summary>
        String viewName;

        // Properties
        /// <summary>
        /// Event handler for PropertyChanged events
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public String ViewName
        {
            get { return viewName; }
            set
            {
                if (value != viewName)
                {
                    viewName = value;
                    OnPropertyChanged(ViewName);
                }
            }
        }

        // Constructors
        public UnitName(SUEString uName)
        {
            modelName = uName;
            viewName = uName.Value;
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
    }
}
