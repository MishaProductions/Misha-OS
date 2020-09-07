using MishaOS.Gui.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace MishaOS.Gui.Windows
{
    /// <summary>
    /// A class for displaying windows.
    /// </summary>
    public class Window
    {
        public Window()
        {
        }
        private bool _IsOpen = false;
        /// <summary>
        /// Checks if window is open.
        /// </summary>
        public bool IsOpen { get { return _IsOpen; } }
        /// <summary>
        /// Opens the window.
        /// </summary>
        public virtual void Open()
        {
            _IsOpen = true;
        }
        /// <summary>
        /// Closes the window.
        /// </summary>
        public virtual void Close()
        {
            _IsOpen = false;
        }
        /// <summary>
        /// Main window update function.
        /// You can use this function for checking mouse clicks.
        /// </summary>
        public virtual void Update()
        {
            
        }
    }
}
