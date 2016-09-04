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
    public class StartingArmy
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
        }
    }
}
