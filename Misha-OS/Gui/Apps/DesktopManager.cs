using MishaOS.Gui.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace MishaOS.Gui
{
    /// <summary>
    /// This class manages all windows.
    /// </summary>
    public static class DesktopManager
    {
        public static List<Window> OpenWindows = new List<Window>();
        /// <summary>
        /// Opens a window.
        /// </summary>
        /// <param name="win">The Window.</param>
        public static void OpenWindow(Window win)
        {
            UiMouse.ClearBackup();
            OpenWindows.Add(win);
            win.Open();
            UiMouse.Update();
        }
        /// <summary>
        /// Closes a window.
        /// </summary>
        /// <param name="win">The Window.</param>
        public static void CloseWindow(Window win)
        {
            //OpenWindows.Remove(win); //For some reason, this causes the OS to hang...
            win.Close();
        }
    }
}
