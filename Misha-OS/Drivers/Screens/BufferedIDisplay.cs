using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Drivers.Screens
{
    public class BufferedIDisplay : IDisplay
    {
        private static BufferedCanvas disp;
        public int ScreenWidth { get; private set; } = 640;
        public int ScreenHeight { get; private set; } = 480;

        public void Clear(Color col)
        {
            disp.Clear(col);
        }

        public void Disable()
        {
            disp.Disable();
        }

        public void DisplayD()
        {
            disp.Display();
        }

        public void DrawRectangle(int x, int y, int Width, int Height, Color col)
        {
            disp.DrawFilledRectangle(new Pen(col),x,y, Width, Height);
        }

        public void DrawString(string str, Pen pen, int x, int y)
        {
            disp.DrawString(str,PCScreenFont.Default,pen,x,y);
        }
        public void Init(Mode mode)
        {
            disp = new BufferedCanvas(new Mode(mode.Columns, mode.Rows,ColorDepth.ColorDepth32));
        }
        public void Render()
        {
            disp.Render();
        }
        public void setPixel(int x, int y, Color c)
        {
            disp.DrawPoint(new Pen(c),x,y);
        }
    }
}
