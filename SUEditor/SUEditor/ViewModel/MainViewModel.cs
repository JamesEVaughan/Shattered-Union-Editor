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
    class MainViewModel
    {
        // Fields

        // Properties
        public UnitFile MainUnitFile { get; private set; }
        public Unit CurrentUnit { get; private set; }

        // Constructors
        /// <summary>
        /// Basic default constructor. Must make separate method calls to initialize the properties
        /// </summary>
        public MainViewModel()
        {
            /// Oddly enough, we don't initialize anything here. We have to wait for the user to
            /// give us a file to work with.
        }

        // Methods
        /// <summary>
        /// Initializes MainUnitFile with the given file name.
        /// </summary>
        /// <param name="fName">The name of the file</param>
        /// <param name="fDir">The full path for the file directory. Ignore if local</param>
        public void InitUnitFile(string fName, string fDir = "")
        {
            /// We're wrapping most of this method in a try-catch block to catch file exceptions.
            /// This will allow the app to quickly recover from incorrect file selections
            try
            {
                // First things first, initialize MainUnitFile
                MainUnitFile = new UnitFile(fName, fDir);
            }
            catch (SUE_InvalidFileException)
            {
                // Send message to user, redo file open.
            }

        }
     }
}
