using Cosmos.HAL.Drivers;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Drivers.Video;
using MishaOS.Drivers.Video.Screens;
using System.Drawing;

namespace MishaOS.Drivers
{
    public static class Display
    {
        public static int ScreenWidth = 800;
        public static int ScreenHeight = 600;
        private static ushort CurrentColorDepth = 32;
        public static string DisplayDriverName { get { return "VBE VESA"; } }

        private static VBEDriver backend;
        private static Font DefaultFont = PCScreenFont.Default;
        #region Methods

        #region Draw/Clear Methods
        public static void Clear(Color color)
        {
            backend.ClearVRAM((uint)color.ToArgb());
        }
        public static void Render()
        {
            backend.Swap();
        }
        public static void DrawRectangle(int x, int y, int Width, int Height, Color col)
        {
            if (backend == null)
                return;
            int xOffset = GetPointOffset(x, y);
            int xScreenWidthInPixel = ScreenWidth * (CurrentColorDepth / 8);
            Width *= (int)CurrentColorDepth / 8;

            for (int i = 0; i < Height; i++)
            {
                backend.ClearVRAM((i * xScreenWidthInPixel) + xOffset, Width, col.ToArgb());
            }
        }
        public static void DrawString(string str, Color color, int x, int y)
        {
            if (backend == null)
                return;
            foreach (char c in str)
            {
                DrawChar(c, DefaultFont, color, x, y); ;
                x += DefaultFont.Width;
            }
        }

        private static void DrawChar(char c, Font aFont, Color pen, int x, int y)
        {
            int p = aFont.Height * (byte)c;

            for (int cy = 0; cy < aFont.Height; cy++)
            {
                for (byte cx = 0; cx < aFont.Width; cx++)
                {
                    if (aFont.ConvertByteToBitAddres(aFont.Data[p + cy], cx + 1))
                    {
                        setPixel((ushort)((x) + (aFont.Width - cx)), (ushort)((y) + cy), pen);
                    }
                }
            }
        }

        public static void setPixel(int x, int y, Color color)
        {
            var offset = (uint)GetPointOffset(x, y);

            if (color.A == 0)
            {
                return;
            }


            backend.SetVRAM(offset, color.B);
            backend.SetVRAM(offset + 1, color.G);
            backend.SetVRAM(offset + 2, color.R);
            backend.SetVRAM(offset + 3, color.A);
        }
        private static int GetPointOffset(int aX, int aY)
        {
            int xBytePerPixel = (int)CurrentColorDepth / 8;
            int stride = (int)CurrentColorDepth / 8;
            int pitch = ScreenWidth * xBytePerPixel;

            return (aX * stride) + (aY * pitch);
        }
        #endregion
        /// <summary>
        /// Disables the grapics, returning to textmode.
        /// </summary>
        public static void Disable()
        {
            if (backend != null)
                backend.DisableDisplay();
        }
        /// <summary>
        /// Loads the display driver.
        /// </summary>
        public static void Init()
        {
            backend = new VBEDriver((ushort)600, (ushort)800, (ushort)32);
        }

        public static Mode[] GetModes()
        {
            return new Mode[] { new Mode(800, 600, ColorDepth.ColorDepth32) };
        }
        #endregion
    }
}
