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
            get { return armyCount[(int)UnitFaction.NEA]; }
            set
            {
                armyCount[(int)UnitFaction.NEA] = value;
                OnPropertyChanged("New England Alliance");
            }
        }
        /// <summary>
        /// How many of this unit the Confederacy starts with
        /// </summary>
        public byte ConStartingCount
        {
            get { return armyCount[(int)UnitFaction.Con]; }
            set
            {
                armyCount[(int)UnitFaction.Con] = value;
                OnPropertyChanged("The Confederacy");
            }
        }
        /// <summary>
        /// How many of this unit the GPF starts with
        /// </summary>
        public byte GPFStartingCount
        {
            get { return armyCount[(int)UnitFaction.GPF]; }
            set
            {
                armyCount[(int)UnitFaction.GPF] = value;
                OnPropertyChanged("Great Plains Federation");
            }
        }
        /// <summary>
        /// How many of this unit Texas starts with
        /// </summary>
        public byte RoTStartingCount
        {
            get { return armyCount[(int)UnitFaction.RoT]; }
            set
            {
                armyCount[(int)UnitFaction.RoT] = value;
                OnPropertyChanged("Republic of Texas");
            }
        }
        /// <summary>
        /// How many of this unit California starts with
        /// </summary>
        public byte CalStartingCount
        {
            get { return armyCount[(int)UnitFaction.Cal]; }
            set
            {
                armyCount[(int)UnitFaction.Cal] = value;
                OnPropertyChanged("California Commonwealth");
            }
        }
        /// <summary>
        /// How many of this unit Pacifica starts with
        /// </summary>
        public byte PacStartingCount
        {
            get { return armyCount[(int)UnitFaction.Pac]; }
            set
            {
                armyCount[(int)UnitFaction.Pac] = value;
                OnPropertyChanged("Pacifica");
            }
        }
        /// <summary>
        /// How many of this unit the EU starts with
        /// </summary>
        public byte EUStartingCount
        {
            get { return armyCount[(int)UnitFaction.EU]; }
            set
            {
                armyCount[(int)UnitFaction.EU] = value;
                OnPropertyChanged("European Union");
            }
        }
        /// <summary>
        /// How many of this unit Russia starts with
        /// </summary>
        public byte RusStartingCount
        {
            get { return armyCount[(int)UnitFaction.Rus]; }
            set
            {
                armyCount[(int)UnitFaction.Rus] = value;
                OnPropertyChanged("Russia");
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

            armyCount[0] = un.StartsInNEA;
            armyCount[1] = un.StartsInCon;
            armyCount[2] = un.StartsInGPF;
            armyCount[3] = un.StartsInRoT;
            armyCount[4] = un.StartsInCal;
            armyCount[5] = un.StartsInPac;
            armyCount[6] = un.StartsInEU;
            armyCount[7] = un.StartsInRus;

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
