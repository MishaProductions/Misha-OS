using Cosmos.System;
using MishaOS.Drivers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Gui
{
    /// <summary>
    /// This class manages the mouse.
    /// </summary>
    public static class UiMouse
    {

        /// <summary>
        /// Updates the mouse.
        /// </summary>
        public static void Update()
        {
            int NewX = (int)MouseManager.X;
            int NewY = (int)MouseManager.Y;
            try
            {
                //Paint New Mouse
                DrawMouse(NewX, NewY);
            }
            catch { }
        }
        public static int MouseX { get { return (int)MouseManager.X; } }
        public static int MouseY { get { return (int)MouseManager.Y; } }

        public static MouseState MouseState { get { return MouseManager.MouseState; } }
        /// <summary>
        /// Backs up the mouse, then draws it.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>

        private static void DrawMouse(int x, int y)
        {
            Display.DrawRectangle(x, y, 5, 5, Color.White);
        }
        /// <summary>
        /// Inits the mouse.
        /// </summary>
        public static void Init()
        {

        }
    }
}
