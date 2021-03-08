using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Drivers.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Drivers
{
    public static class Display
    {
        
        public static int ScreenWidth = 640;
        public static int ScreenHeight = 480;
        private static IDisplay Backend;
        public static string DisplayDriverName { get; private set; }
        #region Methods

        #region Draw/Clear Methods
        public static void Clear(Color col)
        {
            Backend.Clear(col);
        }
        public static void Render()
        {
            try
            {
                Backend.Render();
            }
            catch { }
        }
        public static void DrawRectangle(int x, int y, int Width, int Height, Color col)
        {
            try
            {
                Backend.DrawRectangle(x, y, Width, Height, col);
            }
            catch { }
        }
        public static void DrawString(string str, Pen pen, int x, int y)
        {
            try
            {
                Backend.DrawString(str, pen, x, y);
            }
            catch { }
        }
        public static void setPixel(int x, int y, Color c)
        {
            try
            {
                Backend.setPixel(x, y, c);
            }
            catch { }
        }
        #endregion
        /// <summary>
        /// Disables the grapics, returning to textmode.
        /// </summary>
        public static void Disable()
        {
            Backend.Disable();
        }
        /// <summary>
        /// Enables graphics
        /// </summary>
        public static void DisplayD()
        {
            Backend.DisplayD();
        }
        /// <summary>
        /// Loads the display driver.
        /// </summary>
        public static void Init()
        {
            if (BootManager.IsBootedInVmvare)
            {
                DisplayDriverName = "DoubleBufferedVMWareSVGAII";
                Backend = new BufferedIVmvare();
            }
            else
            {
                DisplayDriverName = "Canvas";
                Backend = new BufferedIDisplay();
            }
            Backend.Init(new Mode(ScreenWidth, ScreenHeight,ColorDepth.ColorDepth32));
            MouseManager.ScreenHeight = (uint)ScreenHeight;
            MouseManager.ScreenWidth = (uint)ScreenWidth;
            Clear(Color.DodgerBlue);
        }
        #endregion
    }
}
