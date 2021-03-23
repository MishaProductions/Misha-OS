using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.Graphics;
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
            VGAGraphics.Clear(VGAColor.Cyan11);
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
                if (col == Color.Black)
                    VGAGraphics.DrawFilledRect(x, y, Width, Height, VGAColor.Black);
                else if (col == Color.White)
                    VGAGraphics.DrawFilledRect(x, y, Width, Height, VGAColor.White);
                else if (col == Color.ForestGreen)
                    VGAGraphics.DrawFilledRect(x, y, Width, Height, VGAColor.Green12);
                else if (col == Color.Green)
                    VGAGraphics.DrawFilledRect(x, y, Width, Height, VGAColor.Green14);
                else if (col == Color.DodgerBlue)
                    VGAGraphics.DrawFilledRect(x, y, Width, Height, VGAColor.Blue10);
                else
                    VGAGraphics.DrawFilledRect(x, y, Width, Height, VGAColor.Red);
            }
            catch { }
        }
        public static void DrawString(string str, Pen pen, int x, int y)
        {
            try
            {
                if (pen.Color == Color.White)
                    VGAGraphics.DrawString(x, y, str, VGAColor.White, VGAFont.Font3x5);
                else if (pen.Color == Color.Black)
                    VGAGraphics.DrawString(x, y, str, VGAColor.Black, VGAFont.Font3x5);
                else
                    VGAGraphics.DrawString(x, y, str, VGAColor.Red, VGAFont.Font3x5);
            }
            catch { }
        }
        public static void setPixel(int x, int y, Color c)
        {
            try
            {
                if (c == Color.Black)
                    VGAGraphics.DrawPixel(x, y, VGAColor.Black);
                else if (c == Color.White)
                    VGAGraphics.DrawPixel(x, y, VGAColor.White);
                else if (c == Color.Magenta)
                    VGAGraphics.DrawPixel(x, y, VGAColor.Magenta);
                else if (c == Color.ForestGreen)
                    VGAGraphics.DrawPixel(x, y, VGAColor.Green11);
                else if (c == Color.Green)
                    VGAGraphics.DrawPixel(x, y, VGAColor.Green);
                else
                    VGAGraphics.DrawPixel(x, y, VGAColor.Red);
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


            MouseManager.ScreenHeight = (uint)ScreenHeight;
            MouseManager.ScreenWidth = (uint)ScreenWidth;
            Clear(Color.DodgerBlue);
        }
        #endregion
    }
}
