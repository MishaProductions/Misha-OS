﻿using Cosmos.System;
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
        public static Window[] OpenWindows = new Window[256];
        private static int c;

        /// <summary>
        /// Opens a window.
        /// </summary>
        /// <param name="win">The Window.</param>
        public static void OpenWindow(Window win)
        {
            Kernel.PrintDebug("Creating Window: "+win.Text);
            OpenWindows[c] = win;
            c++;
            win.Open();
            UiMouse.Update();
        }
        /// <summary>
        /// Closes a window.
        /// </summary>
        /// <param name="win">The Window.</param>
        public static void CloseWindow(Window win)
        {
            Kernel.PrintDebug("Closing Window: " + win.Text);
            int index = 1;
            int winindex = 1;
            foreach (Window w in OpenWindows)
            {
                if (w == win)
                {
                    winindex = index;
                }
                index++;
            }
            OpenWindows[winindex] = null;
            win.Enabled = false;
            win.Close();
            Display.DrawRectangle(win.Location.X,win.Location.Y,win.Size.Width,win.Size.Height,Color.DodgerBlue);
            UiMouse.Update();
        }
        public static void Update()
        {

        }
    }
}
