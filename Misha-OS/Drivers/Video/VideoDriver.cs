using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Drivers.Video
{
    public abstract class VideoDriver
    {
        public abstract string Name { get; }
        public abstract Mode[] SupportedVideoModes { get; }
        public abstract void Init(int width, int height, int ColorDepth);
        public abstract void Disable();
        public abstract void Render();
        public abstract void Clear(Color color);
        public abstract void DrawFilledRectangle(int x, int y, int Width, int Height, Color col);
        public abstract void DrawString(string str, Color pen, int x, int y);
        public abstract void DrawPixel(Color pen, int x, int y);
    }
}
