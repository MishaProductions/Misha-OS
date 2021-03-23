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
        /// Mouse Y
        /// </summary>
        public static int MouseX { get { return (int)MouseManager.X; } }
        /// <summary>
        /// Mouse X
        /// </summary>
        public static int MouseY { get { return (int)MouseManager.Y; } }
        /// <summary>
        /// Current Mouse State
        /// </summary>
        public static MouseState MouseState { get { return MouseManager.MouseState; } }

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
        /// <summary>
        /// Draws the mouse cursor at a position
        /// </summary>
        /// <param name="x">Mouse X</param>
        /// <param name="y">Mouse Y</param>

        private static void DrawMouse(int x, int y)
        {
            Display.DrawRectangle(x, y, 2, 2, Color.White);
        }
        /// <summary>
        /// Inits the mouse.
        /// </summary>
        public static void Init()
        {
            MouseManager.ScreenHeight = (uint)Display.ScreenHeight;
            MouseManager.ScreenWidth = (uint)Display.ScreenWidth;
        }
    }
}
