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

        /// <summary>
        /// This will clear the mouse backup, which will fix problems on mouse click
        /// </summary>
        public static void ClearBackup()
        {
            pixelDatas.Clear();
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
            int i = 0;
            for (uint h = 0; h < 19; h++)
            {
                for (uint w = 0; w < 12; w++)
                {
                    if (cursor[h * 12 + w] == 1)
                    {
                        pixelDatas.Add(GetPixelData((int)w + x, (int)h + y));
                        Display.setPixel((int)w + x, (int)h + y, Color.Black);
                    }
                    if (cursor[h * 12 + w] == 2)
                    {
                        pixelDatas.Add(GetPixelData((int)w + x, (int)h + y));
                        Display.setPixel((int)w + x, (int)h + y,Color.White);
                    }
                    i++;
                }
                i++;
            }
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
            for (int i = 0; i < pixelDatas.Count; i++)
            {
                pixelDatas[i] = new PixelData();
            }
        }
    }

    public class PixelData { public int x = 0; public int y = 0; public Color color = Color.Black;
        public override string ToString()
        {
            return "X: "+x+" Y: "+y+" Color: "+color.ToString();
        }
    }
}
