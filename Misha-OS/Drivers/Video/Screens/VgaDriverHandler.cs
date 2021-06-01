using Cosmos.HAL;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Drivers.Video.Screens
{
    public class VgaDriverHandler : VideoDriver
    {
        public override string Name => "320x200 VGA driver";
        public override Mode[] SupportedVideoModes => new Mode[] { new Mode(320, 200, ColorDepth.ColorDepth32) };

        public override void Clear(Color color)
        {
            VGAGraphics.Clear(VGAColor.Cyan11);
        }

        public override void Disable()
        {
            VGADriverII.Initialize(VGAMode.Text80x25);
        }

        public override void DrawFilledRectangle(int x, int y, int Width, int Height, Color col)
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
                else if (col == Color.SteelBlue)
                    VGAGraphics.DrawFilledRect(x, y, Width, Height, VGAColor.Cyan12);
                else
                    VGAGraphics.DrawFilledRect(x, y, Width, Height, VGAColor.Red);
            }
            catch { }
        }

        public override void DrawPixel(Color c, int x, int y)
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

        public override void DrawString(string str, Color pen, int x, int y)
        {
            try
            {
                var aFont = MishaOSConfig.DefaultFont;
                foreach (char c in str)
                {
                    DrawChar(c, aFont, pen, x, y); ;
                    x += aFont.Width;
                }
            }
            catch { }
        }
        public void DrawChar(char c, Font aFont, Color pen, int x, int y)
        {
            int p = aFont.Height * (byte)c;

            for (int cy = 0; cy < aFont.Height; cy++)
            {
                for (byte cx = 0; cx < aFont.Width; cx++)
                {
                    if (aFont.ConvertByteToBitAddres(aFont.Data[p + cy], cx + 1))
                    {
                        DrawPixel(pen, (ushort)((x) + (aFont.Width - cx)), (ushort)((y) + cy));
                    }
                }
            }
        }

        public override void Init(int width, int height, int ColorDepth)
        {
            if (Display.ScreenWidth != 320 | Display.ScreenHeight != 200)
            {
                throw new Exception("Unsupported screen res");
            }
            VGADriverII.Initialize(VGAMode.Pixel320x200DB);

            Clear(Color.DodgerBlue);
        }

        public override void Render()
        {
            VGAGraphics.Display();
        }
    }
}
