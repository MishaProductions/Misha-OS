using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Drivers.Video.Screens.SVGA;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Drivers.Video.Screens
{
    public class SVGAIIHandler : VideoDriver
    {
        public override string Name => "SVGAII Display device";
        public override Mode[] SupportedVideoModes => new Mode[] { new Mode(320, 200, ColorDepth.ColorDepth32), new Mode(640, 480, ColorDepth.ColorDepth32), new Mode(800, 600, ColorDepth.ColorDepth32) }; //I will make this list bigger as time goes on.

        private DoubleBufferedVMWareSVGAII d;
        public override void Clear(Color color)
        {
            d.DoubleBuffer_Clear(ColorToUint(color));
        }

        public override void Disable()
        {
            d.Disable();
        }

        public override void DrawFilledRectangle(int x, int y, int Width, int Height, Color col)
        {
            d.DoubleBuffer_DrawFillRectangle((uint)x, (uint)y, (uint)Width, (uint)Height, ColorToUint(col));
        }
        private uint ColorToUint(Color col)
        {
            return (uint)col.ToArgb();
        }
        public override void DrawPixel(Color pen, int x, int y)
        {
            d.DoubleBuffer_SetPixel((uint)x, (uint)y, ColorToUint(pen));
        }

        public override void DrawString(string str, Color pen, int x, int y)
        {
            Font aFont = MishaOSConfig.DefaultFont;
            foreach (char c in str)
            {
                DrawChar(c, aFont, pen, x, y);
                x += aFont.Width;
            }
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
            d = new DoubleBufferedVMWareSVGAII();
            d.SetMode((uint)Display.ScreenWidth, (uint)Display.ScreenHeight);
        }

        public override void Render()
        {
            d.DoubleBuffer_Update();
        }
    }
}
