using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

using SUEditor.Model;

namespace SUEditor.ViewModel
{
    public class StartingArmy : INotifyPropertyChanged
    {
        // Fields
        private ObservableCollection<ArmyNode> startingArmies;

        // Properties
        /// <summary>
        /// Indexer to access a specific node in startingArmies
        /// </summary>
        /// <param name="index">The index in startingArmies</param>
        /// <returns>The ArmyNode at the specified index</returns>
        public ArmyNode this[int index] => startingArmies[index];
        /// <summary>
        /// A simple accessor for the total number of units in the collection
        /// </summary>
        public int UnitCount => startingArmies.Count;
        /// <summary>
        /// A simple accessor for the collection
        /// </summary>
        public ObservableCollection<ArmyNode> StartingArmies => startingArmies;

        public StartingArmy()
        {
            startingArmies = new ObservableCollection<ArmyNode>();
        }

        // Methods
        public void AddUnit(UnitName uName)
        {
            // Add the unit to the collection
            ArmyNode tempANode = new ArmyNode(uName.TheUnit);
            startingArmies.Add(tempANode);

            // Listen to changes in the Display Name
            uName.PropertyChanged += tempANode.OnNameChange;

            // Listen to changes in the ArmyNode
            tempANode.PropertyChanged += OnArmyNodeChanged;
        }


        #region INotifyPropertyChanged
        /// <summary>
        /// The event handler for PropertyChanged events, includes updates to StartingArmies
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Our listener for changes to an ArmyNode inside StartingArmies
        /// </summary>
        /// <param name="sender">The ArmyNode that fired a PropertyChanged event</param>
        /// <param name="args">The property that was changed</param>
        protected void OnArmyNodeChanged(object sender, PropertyChangedEventArgs args)
        {
            // First, set up some ease of use handlers
            ArmyNode tempAN = sender as ArmyNode; 
            
            if (tempAN == null)
            {
                // We don't care
                return;
            }

            // We actually don't care about this event, but want subsribers of our PropertyChanged
            // to be able to listen for changes in StartingArmies
            if (PropertyChanged != null)
            {
                // Bubble up
                PropertyChanged(tempAN, args);
            }
        }

        #endregion
    }
}
