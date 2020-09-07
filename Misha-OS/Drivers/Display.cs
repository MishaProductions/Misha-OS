using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Drivers
{
    public static class Display
    {
        public static Canvas disp;
        static int ScreenWidth = 800;
        static int ScreenHeight = 600;

        static public void setPixel(int x, int y, Color c)
        {
            disp.DrawPoint(new Pen(c),new Cosmos.System.Graphics.Point(x,y));
        }
        public static int getWidth()
        {
            return ScreenWidth;
        }

        public static  int getHeight()
        {
            return ScreenHeight;
        }
        /// <summary>
        /// Loads the display driver.
        /// </summary>
        public static void Init()
        {
            MouseManager.ScreenHeight = (uint)ScreenHeight;
            MouseManager.ScreenWidth = (uint)ScreenWidth;
            disp = FullScreenCanvas.GetFullScreenCanvas(new Mode(ScreenWidth, ScreenHeight, ColorDepth.ColorDepth32));
            disp.Clear(Color.DodgerBlue);
        }
        public static void DrawRectangle(int x, int y,int Width,int Height,Color col)
        {
            disp.DrawFilledRectangle(new Pen(col),new Cosmos.System.Graphics.Point(x,y),Width,Height);
        }
    }
}
