using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUEditor.Types;
using SUEditor.Model;

namespace SUEditor.ViewModel
{
    /// <summary>
    /// This is the central class for the ViewModel and specifically acts as the interface between
    /// the Model and the rest of the ViewModel.
    /// </summary>
    public class MainViewModel
    {
        // Fields

        // Properties
        /// <summary>
        /// This is our doorway to the model for dealing with units.
        /// </summary>
        public UnitFile MainUnitFile { get; private set; }

        // Constructors
        /// <summary>
        /// Basic default constructor. Must make separate method calls to initialize the properties
        /// </summary>
        public MainViewModel()
        {
            // Oddly enough, we don't initialize anything here. We have to wait for the user to
            // give us a file to work with. So set everything to null to be safe.
            MainUnitFile = null;
        }

        // Methods
        /// <summary>
        /// Initializes MainUnitFile with the given file name.
        /// </summary>
        /// <param name="fName">The name of the file</param>
        public void InitUnitFile(string filePath)
        {
            /// We're wrapping most of this method in a try-catch block to catch file exceptions.
            /// This will allow the app to quickly recover from incorrect file selections
            try
            {
                // First things first, initialize MainUnitFile
                MainUnitFile = new UnitFile(filePath);
                MainUnitFile.init();
            }
            catch (SUE_InvalidFileException ivfe)
            {
                // Send message to user, redo file open.
                throw ivfe;
            }

        }
     }
}
