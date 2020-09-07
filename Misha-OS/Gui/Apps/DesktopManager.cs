using MishaOS.Gui.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace MishaOS.Gui
{
    public static class DesktopManager
    {
        public static List<Window> OpenWindows = new List<Window>();
        /// <summary>
        /// Opens a window.
        /// </summary>
        /// <param name="win">The Window.</param>
        public static void OpenWindow(Window win)
        {
            OpenWindows.Add(win);
            win.Open();
        }
        /// <summary>
        /// Closes a window.
        /// </summary>
        /// <param name="win">The Window.</param>
        public static void CloseWindow(Window win)
        {
            OpenWindows.Add(win);
            win.Close();
        }
    }
}
