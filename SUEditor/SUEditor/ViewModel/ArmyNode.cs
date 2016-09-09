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
    public class ArmyNode : INotifyPropertyChanged
    {
        // Constants
        private const int NEA_INDEX = 0;
        private const int CON_INDEX = 1;
        private const int GPF_INDEX = 2;
        private const int ROT_INDEX = 3;
        private const int CAL_INDEX = 4;
        private const int PAC_INDEX = 5;
        private const int EU_INDEX = 6;
        private const int RUS_INDEX = 7;

        // Fields
        /// <summary>
        /// Field behind DispName property
        /// </summary>
        private string dispName;
        /// <summary>
        /// Field behind the ###StatingCount properties
        /// </summary>
        private byte[] armyCount;

        private Unit theUnit;

        // Properties
        /// <summary>
        /// The name of the unit. Don't allow others to change this
        /// </summary>
        public string DispName
        {
            get { return dispName; }
            private set
            {
                dispName = value;
                OnPropertyChanged("DispName");
            }
        }
        /// <summary>
        /// How many of this unit the NEA starts with
        /// </summary>
        public byte NEAStartingCount
        {
            get { return armyCount[NEA_INDEX]; }
            set
            {
                armyCount[NEA_INDEX] = value;
                OnPropertyChanged("NEAStartingCount");
            }
        }
        /// <summary>
        /// How many of this unit the Confederacy starts with
        /// </summary>
        public byte ConStartingCount
        {
            get { return armyCount[CON_INDEX]; }
            set
            {
                armyCount[CON_INDEX] = value;
                OnPropertyChanged("ConStartingCount");
            }
        }
        /// <summary>
        /// How many of this unit the GPF starts with
        /// </summary>
        public byte GPFStartingCount
        {
            get { return armyCount[GPF_INDEX]; }
            set
            {
                armyCount[GPF_INDEX] = value;
                OnPropertyChanged("GPFStartingCount");
            }
        }
        /// <summary>
        /// How many of this unit Texas starts with
        /// </summary>
        public byte RoTStartingCount
        {
            get { return armyCount[ROT_INDEX]; }
            set
            {
                armyCount[ROT_INDEX] = value;
                OnPropertyChanged("RoTStartingCount");
            }
        }
        /// <summary>
        /// How many of this unit California starts with
        /// </summary>
        public byte CalStartingCount
        {
            get { return armyCount[CAL_INDEX]; }
            set
            {
                armyCount[CAL_INDEX] = value;
                OnPropertyChanged("CalStartingCount");
            }
        }
        /// <summary>
        /// How many of this unit Pacifica starts with
        /// </summary>
        public byte PacStartingCount
        {
            get { return armyCount[PAC_INDEX]; }
            set
            {
                armyCount[PAC_INDEX] = value;
                OnPropertyChanged("PacStartingCount");
            }
        }
        /// <summary>
        /// How many of this unit the EU starts with
        /// </summary>
        public byte EUStartingCount
        {
            get { return armyCount[EU_INDEX]; }
            set
            {
                armyCount[EU_INDEX] = value;
                OnPropertyChanged("EUStartingCount");
            }
        }
        /// <summary>
        /// How many of this unit Russia starts with
        /// </summary>
        public byte RusStartingCount
        {
            get { return armyCount[RUS_INDEX]; }
            set
            {
                armyCount[RUS_INDEX] = value;
                OnPropertyChanged("RusStartingCount");
            }
        }

        public Unit TheUnit => theUnit;


        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructors
        public ArmyNode()
        {
            dispName = "";
            armyCount = new byte[8];
        }

        public ArmyNode(Unit un)
        {
            dispName = un.DisplayName.Value;
            armyCount = new byte[8];

            armyCount[NEA_INDEX] = un.StartsInNEA;
            armyCount[CON_INDEX] = un.StartsInCon;
            armyCount[GPF_INDEX] = un.StartsInGPF;
            armyCount[ROT_INDEX] = un.StartsInRoT;
            armyCount[CAL_INDEX] = un.StartsInCal;
            armyCount[PAC_INDEX] = un.StartsInPac;
            armyCount[EU_INDEX] = un.StartsInEU;
            armyCount[RUS_INDEX] = un.StartsInRus;

            theUnit = un;
        }

        // Event listeners
        public void OnNameChange(object sender, PropertyChangedEventArgs args)
        {
            // First, we'll listening to when our UnitName changes
            UnitName tempUN = sender as UnitName;
            if (tempUN == null)
            {
                // Ignore
                return;
            }

            if (args.PropertyName == "ViewName")
            {
                DispName = tempUN.ViewName;
            }
        }


        // Implementation of INotifyPropertyChanged
        protected void OnPropertyChanged(string prop)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(prop));
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
