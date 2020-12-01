using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Drivers
{
    interface IDisplay
    {
        int ScreenWidth { get; }
        int ScreenHeight { get;}
        void Clear(Color col);
        void Render();
        void DrawRectangle(int x, int y, int Width, int Height, Color col);
        void DrawString(string str, Pen pen, int x, int y);
        void setPixel(int x, int y, Color c);
        void Disable();
        /// <summary>
        /// Enables graphics
        /// </summary>
        void DisplayD();
        void Init(Mode mode);
    }
}
