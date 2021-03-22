using System;
using System.Collections.Generic;
using System.Text;
using vga = Cosmos.HAL.VGADriverII;

namespace Cosmos.System.Graphics
{
    // this class is used to store vga-related graphical functions
    public static class VGAGraphics
    {
        private static int fontHeight = 16;
        private static int fontWidth = 8;

        // clear the screen
        public static void Clear(VGAColor color) { vga.Clear((byte)color); }

        // swap back buffer if able
        public static void Display() { vga.Display(); }

        // draw pixel
        public static void DrawPixel(int x, int y, VGAColor color) { vga.DrawPixel((ushort)x, (ushort)y, (byte)color); }

        // draw filled rectangle
        public static void DrawFilledRect(int x, int y, int w, int h, VGAColor color)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++) { DrawPixel(x + j, y + i, color); }
            }
        }

        // draw horizontal line
        public static void DrawLineX(int x, int y, int w, VGAColor color)
        {
            for (int i = 0; i < w; i++) { DrawPixel(x + i, y, color); }
        }

        // draw vertical line
        public static void DrawLineY(int x, int y, int h, VGAColor color)
        {
            for (int i = 0; i < h; i++) { DrawPixel(x, y + i, color); }
        }

        // draw point-to-point line
        public static void DrawLine(int x0, int y0, int x1, int y1, VGAColor color)
        {
            // calculate
            int xx = x0, yy = y0;
            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);
            var sx = (x0 < x1) ? 1 : -1;
            var sy = (y0 < y1) ? 1 : -1;
            var err = dx - dy;

            while (true)
            {
                // draw pixel
                DrawPixel(xx, yy, color);

                // increment
                if ((x0 == x1) && (y0 == y1)) break;
                var e2 = 2 * err;
                if (e2 > -dy) { err -= dy; xx += (int)sx; }
                if (e2 < dx) { err += dx; yy += (int)sy; }
            }
        }

        // draw character with transparent background
        public static void DrawChar(int x, int y, char c, VGAColor fg, VGAFont font)
        {
            // determine font size
            fontHeight = 8;
            if (font == VGAFont.Font8x8) { fontHeight = 8; }
            else if (font == VGAFont.Font8x16) { fontHeight = 16; }
            else if (font == VGAFont.Font3x5) { fontHeight = 5; }

            if (font == VGAFont.Font3x5)
                fontWidth = 4; //I added a 1 for some spacing
            else
                fontWidth = 8;

            int p = fontHeight * (byte)c;

            // vertical
            for (int cy = 0; cy < fontHeight; cy++)
            {
                // horizontal
                for (byte cx = 0; cx < fontWidth; cx++)
                {
                    // 8x8
                    if (font == VGAFont.Font8x8)
                    {
                        // convert to position and draw
                        if (VGAFontData.ConvertByteToBitAddress(VGAFontData.Font8x8_Data[p + cy], cx + 1))
                        { DrawPixel(x + (fontWidth - cx), y + cy, fg); }
                    }
                    // 8x16
                    else if (font == VGAFont.Font8x16)
                    {
                        // convert to position and draw
                        if (VGAFontData.ConvertByteToBitAddress(VGAFontData.Font8x16_Data[p + cy], cx + 1))
                        { DrawPixel(x + (fontWidth - cx), y + cy, fg); }
                    }
                    // 3x5
                    else if (font == VGAFont.Font3x5)
                    {
                        // convert to position and draw
                        if (VGAFontData.ConvertByteToBitAddress(VGAFontData.Font3x5_Data[p + cy], cx + 1))
                        { DrawPixel(x + (fontWidth - cx), y + cy, fg); }
                    }
                }
            }
        }

        // draw character with background color
        public static void DrawChar(int x, int y, char c, VGAColor fg, VGAColor bg, VGAFont font)
        {
            // determine font size
            fontHeight = 8;
            if (font == VGAFont.Font8x8) { fontHeight = 8; }
            else if (font == VGAFont.Font8x16) { fontHeight = 16; }
            else if (font == VGAFont.Font3x5) { fontHeight = 5; }

            if (font == VGAFont.Font3x5)
                fontWidth = 4; //I added a 1 for some spacing
            else
                fontWidth = 8;


            int p = fontHeight * (byte)c;

            // vertical
            for (int cy = 0; cy < fontHeight; cy++)
            {
                // horizontal
                for (byte cx = 0; cx < fontWidth; cx++)
                {
                    // 8x8
                    if (font == VGAFont.Font8x8)
                    {
                        // convert to position and draw
                        if (VGAFontData.ConvertByteToBitAddress(VGAFontData.Font8x8_Data[p + cy], cx + 1))
                        { DrawPixel(x + (fontWidth - cx), y + cy, fg); }
                        else { DrawPixel(x + (fontWidth - cx), y + cy, bg); }
                    }
                    // 8x16
                    else if (font == VGAFont.Font8x16)
                    {
                        // convert to position and draw
                        if (VGAFontData.ConvertByteToBitAddress(VGAFontData.Font8x16_Data[p + cy], cx + 1))
                        { DrawPixel(x + (fontWidth - cx), y + cy, fg); }
                        else { DrawPixel(x + (fontWidth - cx), y + cy, bg); }
                    }
                    // 3x5
                    else if (font == VGAFont.Font3x5)
                    {
                        // convert to position and draw
                        if (VGAFontData.ConvertByteToBitAddress(VGAFontData.Font3x5_Data[p + cy], cx + 1))
                        { DrawPixel(x + (fontWidth - cx), y + cy, fg); }
                        else { DrawPixel(x + (fontWidth - cx), y + cy, bg); }
                    }
                }
            }
        }

        // draw string with transparent background
        public static void DrawString(int x, int y, string text, VGAColor fg, VGAFont font)
        {
            // determine font size
            fontHeight = 8;


            if (font == VGAFont.Font8x8) { fontHeight = 8; }
            else if (font == VGAFont.Font8x16) { fontHeight = 16; }
            else if (font == VGAFont.Font3x5) { fontHeight = 5; }

            if (font == VGAFont.Font3x5)
                fontWidth = 4; //I added a 1 for some spacing
            else
                fontWidth = 8;

            int xx = x, yy = y;
            for (int i = 0; i < text.Length; i++)
            {
                // new line
                if (text[i] == '\n') { xx = x; yy += fontHeight; }
                // character
                else { DrawChar(xx, yy, text[i], fg, font); xx += fontWidth; }
            }
        }

        // draw string with background color
        public static void DrawString(int x, int y, string text, VGAColor fg, VGAColor bg, VGAFont font)
        {
            // determine font size
            fontHeight = 8;
            if (font == VGAFont.Font8x8) { fontHeight = 8; }
            else if (font == VGAFont.Font8x16) { fontHeight = 16; }
            else if (font == VGAFont.Font3x5) { fontHeight = 5; }

            if (font == VGAFont.Font3x5)
                fontWidth = 4; //I added a 1 for some spacing
            else
                fontWidth = 8;

            int xx = x, yy = y;
            for (int i = 0; i < text.Length; i++)
            {
                // new line
                if (text[i] == '\n') { xx = x; yy += fontHeight; }
                // character
                else { DrawChar(xx, yy, text[i], fg, bg, font); xx += fontWidth; }
            }
        }

        // draw custom image format
        public static void DrawImage(int x, int y, VGAImage image)
        {
            for (int yy = 0; yy < image.Height; yy++)
            {
                for (int xx = 0; xx < image.Width; xx++)
                {
                    DrawPixel(x + xx, y + yy, (VGAColor)image.Data[xx + (yy * image.Width)]);
                }
            }
        }

        // draw custom image format with transparency key
        public static void DrawImage(int x, int y, VGAColor transKey, VGAImage image)
        {
            for (int yy = 0; yy < image.Height; yy++)
            {
                for (int xx = 0; xx < image.Width; xx++)
                {
                    if (image.Data[xx + (yy * image.Width)] != (byte)transKey)
                    { DrawPixel(x + xx, y + yy, (VGAColor)image.Data[xx + (yy * image.Width)]); }
                }
            }
        }
    }
}