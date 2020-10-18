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

        private static List<PixelData> pixelDatas = new List<PixelData>();
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
        /// This will clear the mouse backup, which will fix problems on mouse click
        /// </summary>
        public static void ClearBackup()
        {
            pixelDatas.Clear();
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
            ImageUtil.DrawImage(cursor,x,y,12,19);
        }
        /// <summary>
        /// Inits the mouse.
        /// </summary>
        public static void Init()
        {
            for (int i = 0; i < pixelDatas.Count; i++)
            {
                pixelDatas[i] = new PixelData();
            }
        }
    }

    public class PixelData
    {
        public int x = 0; public int y = 0; public Color color = Color.Black;
        public override string ToString()
        {
            return "X: " + x + " Y: " + y + " Color: " + color.ToString();
        }
    }
}
