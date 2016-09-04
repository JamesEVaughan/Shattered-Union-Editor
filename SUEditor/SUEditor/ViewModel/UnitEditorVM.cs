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
    public class UnitEditorVM : INotifyPropertyChanged, IDataErrorInfo
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
        private int fuel;
        private short collDamage;
        private bool isIndirect;
        private bool isSingleUse;
        private bool isNotKept;
        private bool canBuy;
        private UnitMovementClass moveType;
        private UnitArmorClass armorType;
        private VMFact faction;


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

        public int Fuel
        {
            get { return fuel; }
            set
            {
                fuel = value;
                OnPropertyChanged("Fuel");
            }
        }
        public short CollDamage
        {
            get { return collDamage; }
            set
            {
                collDamage = value;
                OnPropertyChanged("CollDamage");
            }
        }
        public bool IsIndirect
        {
            get { return isIndirect; }
            set
            {
                isIndirect = value;
                OnPropertyChanged("IsIndirect");
            }
        }
        public bool IsSingleUse
        {
            get { return isSingleUse; }
            set
            {
                isSingleUse = value;
                OnPropertyChanged("IsSingleUse");
            }
        }
        public bool IsNotKept
        {
            get { return isNotKept; }
            set
            {
                isNotKept = value;
                OnPropertyChanged("IsNotKept");
            }
        }
        public bool CanBuy
        {
            get { return canBuy; }
            set
            {
                canBuy = value;
                OnPropertyChanged("CanBuy");
            }
        }
        public UnitMovementClass MoveType
        {
            get { return moveType; }
            set
            {
                moveType = value;
                OnPropertyChanged("MoveType");
            }
        }
        public UnitArmorClass ArmorType
        {
            get { return armorType; }
            set
            {
                armorType = value;
                OnPropertyChanged("ArmorType");
            }
        }
        public VMFact Faction
        {
            get { return faction; }
            set
            {
                faction = value;
                OnPropertyChanged("Faction");
            }
        }
        public UnitFaction UFaction
        {
            get
            {
                return ((faction != VMFact.USA) ? (UnitFaction)((int)faction) : UnitFaction.USA);
            }

            set
            {
                faction = ((value != UnitFaction.USA) ? (VMFact)((int)value) : VMFact.USA);
                OnPropertyChanged("Faction");
            }
        }

        /// <summary>
        /// This is a reference back to the currently selected UnitName from the list
        /// </summary>
        public UnitName CurUnitName
        {
            get { return curUnitName; }
            private set
            {
                curUnitName = value;
                OnPropertyChanged("CurUnitName");
            }
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
            CurUnitName = uName;
            DispName = tempy.DisplayName.Value;
            Health = tempy.HitPoints.Value;
            Def = tempy.Defense.Value;
            Movement = tempy.Speed.Value;
            AirAtt = tempy.AirAttack.Value;
            InfAtt = tempy.InfAttack.Value;
            VehAtt = tempy.ArmorAttack.Value;
            SightRange = tempy.Vision.Value;
            AttRange = tempy.AttackRange.Value;

            // Multiply by 1000 so that we're showing the "real", in-game value
            Cost = tempy.Cost.Value * 1000;

            // New additions!
            Fuel = tempy.GasTank.Value;
            CollDamage = tempy.CollateralDamage.Value;

            IsIndirect = tempy.IsIndirect;
            IsSingleUse = tempy.IsSingleUse;
            IsNotKept = tempy.IsNotKept;
            CanBuy = tempy.CanBuyFlag;

            // Final additions!
            MoveType = tempy.MoveCat;
            ArmorType = tempy.UnitCat;

            // We have to convert UnitFaction to VMFact
            UFaction = tempy.Faction;

            // Subscribe new UnitName
            PropertyChanged += curUnitName.OnNameChange;

            // Ok, we can respond to events normally
            IsInitialLoad = false;
        }

        // Helpers
        private VMFact FromUnitFact(UnitFaction f)
        {
            if (f != UnitFaction.USA)
            {
                return (VMFact)((int)f);
            }

            return VMFact.USA;
        }

        private bool IsUniqueName(string name)
        {
            foreach (UnitName uName in NameList)
            {
                if (name == uName.ViewName && uName != curUnitName)
                {
                    return false;
                }
            }

            return true;
        }

        // INotifyPropertyChanged implmentation
        protected void OnPropertyChanged(String propName)
        {
            // Don't propogate events on error
            if (!String.IsNullOrEmpty(this[propName]))
            {
                return;
            }
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

        // IDataErrorInfo implementation
        public string this[string propName]
        {
            get
            {
                switch (propName)
                {
                    case "DispName":
                        // Check for uniqueness
                        if (!IsUniqueName(DispName))
                        {
                            return "Display Name must be unique.";
                        }
                        return "";
                    case "Cost":
                        // Make sure it's a multiple of 1000
                        if ((Cost % 1000) != 0)
                        {
                            return "The cost must be in multiples of $1,000";
                        }
                        return "";
                    case "Movement":
                        // If it's a stationary unit ("Dummy"), then Movement must be zero
                        if (MoveType == UnitMovementClass.Dummy && Movement != 00)
                        {
                            return "Units of Movement Type \"Dummy\" must have a Movement value of 0.";
                        }
                        // If it's an airplane ("Planr"), the Movement must be 0xFE
                        else if (MoveType == UnitMovementClass.Planr && Movement != 999)
                        {
                            return "Units of Movement Type \"Airplane\" must have a Movement value of 999";
                        }
                        return "";
                }

                // If we fall out of the switch, just return nothing
                return "";
            }
        }

        public string Error
        {
            get
            {
                // For now, just return the empty string
                return "";
            }
        } 
    }
}
