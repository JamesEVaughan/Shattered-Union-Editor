using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

using SUEditor.Types;
using SUEditor.Model;

namespace SUEditor.ViewModel
{
    /// <summary>
    /// This is the ViewModel class for the Unit Editor tab
    /// </summary>
    public class UnitEditorVM : INotifyPropertyChanged
    {
        // Fields
        private UnitName curUnitName;
        private string dispName;
        private int cost;
        private int movement;
        private short sightRange;
        private short airAtt;
        private short vehAtt;
        private short infAtt;
        private short def;
        private short health;
        private short attRange;

        // Properties
        /// <summary>
        /// A list of the names for all units we currently have
        /// </summary>
        public ObservableCollection<UnitName> NameList { get; private set; }

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
        public short AttRange
        {
            get { return attRange; }
            set
            {
                attRange = value;
                OnPropertyChanged("AttRange");
            }
        }
        /// <summary>
        /// This is a reference back to the currently selected UnitName from the list
        /// </summary>
        public UnitName CurUnitName
        {
            get { return curUnitName; }
        }
        /// <summary>
        /// Is true iff properties are being loaded due to a ChangedSelection event
        /// </summary>
        public bool IsInitialLoad { get; private set; }

        /// <summary>
        /// The event handler for property change events
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructors
        public UnitEditorVM()
        {
            IsInitialLoad = false;
            NameList = new ObservableCollection<UnitName>();
        }

        // Methods
        public void changeSelection(UnitName uName)
        {
            // Make it easy to access the unit
            Unit tempy = uName.TheUnit;
            // Mark all following actions as an initial load
            IsInitialLoad = true;

            // Unsubscribe old UnitName
            if (curUnitName != null)
            {
                PropertyChanged -= curUnitName.OnNameChange;
            }

            // And slap all the values where they belong
            // Use the fields because we don't need to propogate PorpChange events
            curUnitName = uName;
            DispName = tempy.DisplayName.Value;
            Health = tempy.HitPoints.Value;
            Def = tempy.Defense.Value;
            Movement = tempy.Speed.Value;
            AirAtt = tempy.AirAttack.Value;
            InfAtt = tempy.InfAttack.Value;
            VehAtt = tempy.ArmorAttack.Value;
            SightRange = tempy.Vision.Value;
            Cost = tempy.Cost.Value;
            AttRange = tempy.AttackRange.Value;

            // Subscribe new UnitName
            PropertyChanged += curUnitName.OnNameChange;

            // Ok, we can respond to events normally
            IsInitialLoad = false;
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
