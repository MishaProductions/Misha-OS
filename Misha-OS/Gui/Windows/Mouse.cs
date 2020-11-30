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
        /// Copied from https://github.com/nifanfa/Cosmos-GUI-Sample
        /// </summary>
        static int[] cursor = new int[]
            {
                1,0,0,0,0,0,0,0,0,0,0,0,
                1,1,0,0,0,0,0,0,0,0,0,0,
                1,2,1,0,0,0,0,0,0,0,0,0,
                1,2,2,1,0,0,0,0,0,0,0,0,
                1,2,2,2,1,0,0,0,0,0,0,0,
                1,2,2,2,2,1,0,0,0,0,0,0,
                1,2,2,2,2,2,1,0,0,0,0,0,
                1,2,2,2,2,2,2,1,0,0,0,0,
                1,2,2,2,2,2,2,2,1,0,0,0,
                1,2,2,2,2,2,2,2,2,1,0,0,
                1,2,2,2,2,2,2,2,2,2,1,0,
                1,2,2,2,2,2,2,2,2,2,2,1,
                1,2,2,2,2,2,2,1,1,1,1,1,
                1,2,2,2,1,2,2,1,0,0,0,0,
                1,2,2,1,0,1,2,2,1,0,0,0,
                1,2,1,0,0,1,2,2,1,0,0,0,
                1,1,0,0,0,0,1,2,2,1,0,0,
                0,0,0,0,0,0,1,2,2,1,0,0,
                0,0,0,0,0,0,0,1,1,0,0,0
            };

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
            try
            {
                ImageUtil.DrawImage(cursor, x, y, 12, 19);
            }
            catch
            {

            }
        }
        /// <summary>
        /// Inits the mouse.
        /// </summary>
        public static void Init()
        {

        }
    }
}
