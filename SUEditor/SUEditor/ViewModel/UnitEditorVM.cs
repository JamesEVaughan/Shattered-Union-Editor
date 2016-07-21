using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using SUEditor.Types;
using SUEditor.Model;

namespace SUEditor.ViewModel
{
    /// <summary>
    /// This is the ViewModel class for the Unit Editor tab
    /// </summary>
    class UnitEditorVM : INotifyPropertyChanged
    {
        // Fields
        private Unit curUnit;
        private string dispName;
        private int cost;
        private int movement;
        private short sightRange;
        private short airAtt;
        private short vehAtt;
        private short infAtt;
        private short def;
        private short health;

        // Properties
        /// <summary>
        /// The current unit that we're working with
        /// </summary>
        public Unit CurUnit
        {
            get { return curUnit; }
            set
            {
                curUnit = value;
                OnPropertyChanged("CurUnit");
            }
        }

        public string DispName
        {
            get { return dispName; }
            set
            {
                dispName = value;
                OnPropertyChanged("DispName");
            }
        }

        public int Cost
        {
            get { return cost; }
            set
            {
                cost = value;
                OnPropertyChanged("Cost");
            }
        }
        public int Movement
        {
            get { return movement; }
            set
            {
                movement = value;
                OnPropertyChanged("Movement");
            }
        }
        public short SightRange
        {
            get { return sightRange; }
            set
            {
                sightRange = value;
                OnPropertyChanged("SightRange");
            }
        }
        public short AirAtt
        {
            get { return airAtt; }
            set
            {
                airAtt = value;
                OnPropertyChanged("AirAtt");
            }
        }
        public short VehAtt
        {
            get { return vehAtt; }
            set
            {
                vehAtt = value;
                OnPropertyChanged("VehAtt");
            }
        }
        public short InfAtt
        {
            get { return infAtt; }
            set
            {
                infAtt = value;
                OnPropertyChanged("InfAtt");
            }
        }
        public short Def
        {
            get { return def; }
            set
            {
                def = value;
                OnPropertyChanged("Def");
            }
        }
        public short Health
        {
            get { return health; }
            set
            {
                health = value;
                OnPropertyChanged("Health");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        // Constructors
        public UnitEditorVM()
        {
            
        }

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
