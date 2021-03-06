using Cosmos.System;
using MishaOS.Drivers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Gui
{
    /// <summary>
    /// Graphics class. Used to draw controls.
    /// </summary>
    public static class Graphics
    {
        /// <summary>
        /// Draws text on the screen.
        /// </summary>
        /// <param name="txt">the text</param>
        /// <param name="x">Text X</param>
        /// <param name="y">Text Y</param>
        /// <param name="data">Control Data</param>
        public static void Label(string txt, int x, int y, ControlData data = null)
        {
            if (data == null)
                Display.DrawString(txt, new Cosmos.System.Graphics.Pen(Color.Black), x, y);
            else
            {
                Display.DrawString(txt, new Cosmos.System.Graphics.Pen(data.ForeColor), x, y);
            }
        }
        /// <summary>
        /// Draws a box on the screen.
        /// </summary>
        /// <param name="x">Box X</param>
        /// <param name="y">Box Y</param>
        /// <param name="w">Box Width</param>
        /// <param name="h">Box Height</param>
        /// <param name="data">Control Data</param>
        public static void Box(int x, int y, int w, int h, ControlData data = null)
        {
            if (data == null)
                Display.DrawRectangle(x, y, w, h, Color.White);
            else
            {
                Display.DrawRectangle(x, y, w, h, data.BackColor);
            }
        }
        /// <summary>
        /// Draws a button on the screen.
        /// </summary>
        /// <param name="text">Button Text</param>
        /// <param name="x">Button X</param>
        /// <param name="y">Button Y</param>
        /// <param name="w">Button Width</param>
        /// <param name="h">Button Height</param>
        /// <param name="data">Control Data</param>
        /// <returns>Weather the button is pressed or not.</returns>
        public static bool Button(string text, int x, int y, int w, int h, ControlData data = null)
        {
            if (data == null)
            {
                Display.DrawRectangle(x, y, w, h, Color.White);
                Display.DrawString(text, new Cosmos.System.Graphics.Pen(Color.Black), x, y);
            }
            else
            {
                Display.DrawRectangle(x, y, w, h, data.BackColor);
                Display.DrawString(text, new Cosmos.System.Graphics.Pen(data.ForeColor), x, y);
            }
            //Redraw mouse (just in case)
            UiMouse.Update();

            return IsObjPressed(x, y,w,h);
        }
        /// <summary>
        /// Checks weather an object is pressed.
        /// </summary>
        /// <param name="x">Object X</param>
        /// <param name="y">Object Y</param>
        /// <param name="w">Object Width</param>
        /// <param name="h">Object Height</param>
        /// <returns>Weather or not the object is clicked</returns>
        public static bool IsObjPressed(int x, int y, int w, int h)
        {
            if (MouseManager.MouseState == MouseState.Left)
            {
                if (UiMouse.MouseY >= y && UiMouse.MouseY <= y + h)
                {
                    if (UiMouse.MouseX >= x && UiMouse.MouseX <= x + w)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
