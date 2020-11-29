using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Gui.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Drivers
{
    public static class Display
    {
        private static BufferedCanvas disp;
        public static int ScreenWidth = 640;
        public static int ScreenHeight = 480;



        #region Methods

        #region Draw/Clear Methods
        public static void Clear(Color col)
        {
            disp.Clear(col);
        }
        public static void Render()
        {
            disp.Render();
        }
        public static void DrawRectangle(int x, int y, int Width, int Height, Color col)
        {
            disp.DrawFilledRectangle(new Pen(col),new Cosmos.System.Graphics.Point(x,y),Width,Height);
        }
        public static void DrawString(string str, Pen pen, int x, int y)
        {
            disp.DrawString(str,PCScreenFont.Default,pen,new Cosmos.System.Graphics.Point(x,y));
        }
        public static void setPixel(int x, int y, Color c)
        {
            disp.DrawPoint(new Pen(c),x,y);
        }
        #endregion
        /// <summary>
        /// Disables the grapics, returning to textmode.
        /// </summary>
        public static void Disable()
        {
            disp.Disable();
        }

        public static void DisplayD()
        {
            disp.Display();
        }


        /// <summary>
        /// Loads the display driver.
        /// </summary>
        public static void Init()
        {
            MouseManager.ScreenHeight = (uint)ScreenHeight;
            MouseManager.ScreenWidth = (uint)ScreenWidth;
            disp = new BufferedCanvas(new Mode(ScreenWidth, ScreenHeight, ColorDepth.ColorDepth32));
            
            Clear(Color.DodgerBlue);
        }
        #endregion
    }
}
