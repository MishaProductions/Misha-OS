using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.Graphics;
using MishaOS.Drivers.Video;
using System.Drawing;

namespace MishaOS.Drivers
{
    public static class Display
    {
        public static int ScreenWidth = 320;
        public static int ScreenHeight = 200;
        public static string DisplayDriverName { get; private set; }
        #region Methods

        #region Draw/Clear Methods
        public static void Clear(Color col)
        {
            VGAGraphics.Clear(RGBColor.Get(col.R, col.G, col.B));
        }
        public static void Render()
        {
            try
            {
                VGAGraphics.Display();
            }
            catch { }
        }
        public static void DrawRectangle(int x, int y, int Width, int Height, Color col)
        {
            try
            {
                VGAGraphics.DrawFilledRect(x, y, Width, Height, RGBColor.Get(col.R, col.G, col.B));
            }
            catch { }
        }
        public static void DrawString(string str, Pen pen, int x, int y)
        {
            try
            {
                VGAGraphics.DrawString(x, y, str, RGBColor.Get(pen.Color.R, pen.Color.G, pen.Color.B), VGAFont.Font3x5);
            }
            catch { }
        }
        public static void setPixel(int x, int y, Color c)
        {
            try
            {
                VGAGraphics.DrawPixel(x, y, RGBColor.Get(c.R, c.G, c.B));
            }

            catch { }
        }
        #endregion
        /// <summary>
        /// Disables the grapics, returning to textmode.
        /// </summary>
        public static void Disable()
        {
            VGADriverII.Initialize(VGAMode.Text80x25);
        }
        /// <summary>
        /// Loads the display driver.
        /// </summary>
        public static void Init()
        {
            VGADriverII.Initialize(VGAMode.Pixel320x200DB);
            DisplayDriverName = "Double buffered 320x200 Generic VGA driver";

            Clear(Color.DodgerBlue);
        }
        #endregion
    }
}
