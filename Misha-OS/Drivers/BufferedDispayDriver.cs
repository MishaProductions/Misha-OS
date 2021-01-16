using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Drivers
{
    public class BufferedCanvas : Canvas
    {
        private Canvas display;
        public int screenH;
        public int screenW;
        public static Color[] SBuffer;
        public static Color[] SBufferOld;
        public override List<Mode> AvailableModes => display.AvailableModes;

        public override Mode DefaultGraphicMode => display.DefaultGraphicMode;

        public override Mode Mode { get { return display.Mode; } set { display.Mode = value; } }

        public BufferedCanvas(Mode mode)
        {
            display = FullScreenCanvas.GetFullScreenCanvas(mode);
            screenW = mode.Columns;
            screenH = mode.Rows;

            SBuffer = new Color[(screenW * screenH) + screenW];
            SBufferOld = new Color[(screenW * screenH) + screenW];
        }

        public override void Disable()
        {
            display.Disable();
        }

        public override void Display()
        {
            display.Display();
        }

        public override void DrawArray(Color[] colors, int x, int y, int width, int height)
        {
            display.DrawArray(colors, x, y, width, height);
        }

        public override void DrawPoint(Pen pen, int x, int y)
        {
            if (x > screenW || y > screenH) return;
            if (SBuffer[(y * screenW) + x] == pen.Color)
                return;

            SBuffer[(y * screenW) + x] = pen.Color;
        }
        public override void DrawPoint(Pen pen, float x, float y)
        {
            DrawPoint(pen, (int)x, (int)y);
        }

        public override Color GetPointColor(int x, int y)
        {
            return SBuffer[(y * screenW) + x];
        }
        public override void Clear(Color color)
        {
            for (int i = 0, len = SBuffer.Length; i < len; i++)
            {
                if (SBuffer[i] != color)
                    SBuffer[i] = color;
            }
        }

        public void Render()
        {
            Pen pen = new Pen(Color.Black);
            for (int y = 0, h = screenH; y < h; y++)
            {
                for (int x = 0, w = screenW; x < w; x++)
                {
                    Color currentCol = SBuffer[(y * screenW) + x];
                    Color OldCol = SBufferOld[(y * screenW) + x];
                    if (currentCol != OldCol) //Only draw what changed.
                    {
                        pen.Color = currentCol;
                        display.DrawPoint(pen, x, y);
                    }
                }
            }
            //Now, copy the SBuffer[] to SBufferOld[]
            for (int i = 0, len = SBuffer.Length; i < len; i++)
            {
                if (SBufferOld[i] != SBuffer[i])
                {
                    SBufferOld[i] = SBuffer[i];
                }
            }
        }

        public override void DrawFilledRectangle(Pen pen, int x_start, int y_start, int width, int height)
        {
            int endX = x_start + width;
            int endY = y_start + height;

            for (int x = x_start; x < endX; x++)
            {
                for (int y = y_start; y < endY; y++)
                {
                    DrawPoint(pen, x, y);
                }
            }
        }
    }
}