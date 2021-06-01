using Cosmos.System.Graphics;
using MishaOS.Drivers.Video;
using MishaOS.Drivers.Video.Screens;
using System.Drawing;

namespace MishaOS.Drivers
{
    public static class Display
    {
        public static int ScreenWidth = 320;
        public static int ScreenHeight = 200;
        public static string DisplayDriverName { get { return driver.Name; } }

        private static VideoDriver driver;
        #region Methods

        #region Draw/Clear Methods
        public static void Clear(Color col)
        {
            driver.Clear(col);
        }
        public static void Render()
        {
            driver.Render();
        }
        public static void DrawRectangle(int x, int y, int Width, int Height, Color col)
        {
            driver.DrawFilledRectangle(x, y, Width, Height, col);
        }
        public static void DrawString(string str, Color c, int x, int y)
        {
            driver.DrawString(str.ToString(), c, x, y);
        }
        public static void setPixel(int x, int y, Color c)
        {
            driver.DrawPixel(c, x, y);
        }
        #endregion
        /// <summary>
        /// Disables the grapics, returning to textmode.
        /// </summary>
        public static void Disable()
        {
            driver.Disable();
        }
        /// <summary>
        /// Loads the display driver.
        /// </summary>
        public static void Init()
        {
            if (BootManager.HasVBE)
            {
                driver = new VbeHandler();
                ScreenWidth = driver.SupportedVideoModes[0].Columns;
                ScreenHeight = driver.SupportedVideoModes[0].Rows;

                driver.Init(ScreenWidth, ScreenHeight, 32);
            }
            //else if (BootManager.HasSVGA)
            //{
            //    driver = new SVGAIIHandler();
            //    driver.Init(Display.ScreenWidth, Display.ScreenHeight, 0);

            //    //reinit mouse
            //    UiMouse.Init();
            //}
            else
            {
                driver = new VgaDriverHandler();
                ScreenWidth = driver.SupportedVideoModes[0].Columns;
                ScreenHeight = driver.SupportedVideoModes[0].Rows;

                driver.Init(ScreenWidth, ScreenHeight, 4);
            }
        }

        public static Mode[] GetModes()
        {
            return driver.SupportedVideoModes;
        }
        #endregion
    }
}
