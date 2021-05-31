using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Gui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Drivers.Video.Screens
{
    public class VbeHandler : VideoDriver
    {
        public override string Name => "Generic VBE driver";
        //I will make this list bigger as time goes on.
        public override Mode[] SupportedVideoModes => new Mode[] {new Mode(640, 480, ColorDepth.ColorDepth32), new Mode(800, 600, ColorDepth.ColorDepth32) };

        private VBECanvas backend;

        public override void Clear(Color color)
        {
            backend.Clear(color);
        }

        public override void Disable()
        {
            backend.Disable();
        }

        public override void DrawFilledRectangle(int x, int y, int Width, int Height, Color col)
        {
            Pen p = new Pen(col);
            backend.DrawFilledRectangle(p, new Cosmos.System.Graphics.Point(x, y), Width, Height);
            p = null;
        }

        public override void DrawPixel(Color pen, int x, int y)
        {
            backend.DrawPoint(new Pen(pen), new Cosmos.System.Graphics.Point(x, y));
        }

        public override void DrawString(string str, Color pen, int x, int y)
        {
            backend.DrawString(str, PCScreenFont.Default, new Pen(pen), new Cosmos.System.Graphics.Point(x, y));
        }

        public override void Init(int width, int height, int ColorDepth)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("[VBE] starting...");
            Console.WriteLine(width + "x" + height);
            backend = new VBECanvas(new Mode(width, height, Cosmos.System.Graphics.ColorDepth.ColorDepth32));
            UiMouse.Init();
        }

        public override void Render()
        {
            backend.Display();
        }
    }
}
