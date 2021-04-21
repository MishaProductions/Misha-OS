using Cosmos.System;
using MishaOS.Drivers;
using MishaOS.Gui.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            try
            {
                OpenWindows.Add(win);
                win.Open();
            }
            catch
            {

            }
        }
        /// <summary>
        /// Closes a window.
        /// </summary>
        /// <param name="win">The Window.</param>
        public static void CloseWindow(Window win)
        {
            try
            {
                Kernel.PrintDebug("Closing Window: " + win.Text);
                int index = 1;
                int winindex = 1;
                foreach (Window w in OpenWindows)
                {
                    if (w != null)
                    {
                        if (w == win)
                        {
                            winindex = index;
                            w.Dispose();
                            break;
                        }
                    }
                    index++;
                }
                OpenWindows.RemoveAt(winindex);

                //Redraw other windows
                Update();
            }
            catch
            {

            }
        }
        /// <summary>
        /// Redraws and updates all opened windows
        /// </summary>
        public static void Update()
        {
            foreach (Window w in OpenWindows)
            {
                if (w != null)
                {
                    //Only draw/update opened windows
                    if (w.IsOpen)
                    {
                        w.UpdateAll();
                        w.DrawAll();
                    }
                }
            }
        }
    }
}
