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
        static int OldX = 0;
        static int OldY = 0;

        private static PixelData[] pixelDatas = new PixelData[8];
        /// <summary>
        /// Updates the mouse.
        /// </summary>
        public static void Update()
        {

            int NewX = (int)MouseManager.X;
            int NewY = (int)MouseManager.Y;
            if (NewX != OldX | NewY != OldY)
            {
                try
                {
                    //Restore the backup First
                    RestoreBackup();

                    //Paint New Mouse
                    DrawMouse(NewX, NewY);
                }
                catch { }

                OldX = NewX;
                OldY = NewY;
            }
        }

        public static int MouseX { get { return (int)MouseManager.X; } }
        public static int MouseY { get { return (int)MouseManager.Y; } }
        /// <summary>
        /// Backs up the mouse, then draws it.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>

        private static void DrawMouse(int x, int y)
        {
            pixelDatas[1] = GetPixelData(x + 1, y);
            pixelDatas[2] = GetPixelData(x, y + 1);
            pixelDatas[3] = GetPixelData(x + 1, y + 1);

            pixelDatas[5] = GetPixelData(x - 1, y);
            pixelDatas[6] = GetPixelData(x, y - 1);
            pixelDatas[7] = GetPixelData(x - 1, y - 1);
            Display.disp.DrawPoint(new Cosmos.System.Graphics.Pen(Color.White), x + 1, y);
            Display.disp.DrawPoint(new Cosmos.System.Graphics.Pen(Color.White), x, y + 1);
            Display.disp.DrawPoint(new Cosmos.System.Graphics.Pen(Color.White), x + 1, y + 1);

            Display.disp.DrawPoint(new Cosmos.System.Graphics.Pen(Color.White), x - 1, y);
            Display.disp.DrawPoint(new Cosmos.System.Graphics.Pen(Color.White), x, y - 1);
            Display.disp.DrawPoint(new Cosmos.System.Graphics.Pen(Color.White), x - 1, y - 1);
        }
        /// <summary>
        /// Restore the old pixels as if the mouse was not there.
        /// </summary>
        private static void RestoreBackup()
        {
            foreach (PixelData pix in pixelDatas)
            {
                Display.disp.DrawPoint(new Cosmos.System.Graphics.Pen(pix.color), pix.x, pix.y);
            }
        }

        private static PixelData GetPixelData(int x, int y)
        {
            Color c = Display.disp.GetPointColor(x, y);
            return new PixelData() { x = x, y = y, color = c };
        }
        /// <summary>
        /// Inits the mouse.
        /// </summary>
        public static void Init()
        {
            for (int i = 0; i < pixelDatas.Length; i++)
            {
                pixelDatas[i] = new PixelData();
            }
        }
    }

    public class PixelData { public int x = 0; public int y = 0; public Color color = Color.Black; }
}
