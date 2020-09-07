using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Drivers;
using MishaOS.Gui.Apps;
using MishaOS.Gui.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Gui.Windows
{
    /// <summary>
    /// Desktop App
    /// </summary>
    public class Desktop : Window
    {
        Cosmos.System.Graphics.Point TerminalLoc = new Cosmos.System.Graphics.Point(0, 40);
        Cosmos.System.Graphics.Point SettingsLoc = new Cosmos.System.Graphics.Point(0, 70);

        public override void Open()
        {
            base.Open();
            ReDraw();
        }

        public void ReDraw()
        {
            //Draw Background
            Display.DrawRectangle(0, 0, Display.getWidth(), Display.getHeight(), Color.ForestGreen);
            //Draw into text
            Display.disp.DrawString("Misha OS Welcome Screen", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(5, 5));
            Display.disp.DrawString("Select an Application to begin.", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(5, 20));
            //Draw Terminal
            Display.disp.DrawString("Terminal", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(TerminalLoc.X + 5, TerminalLoc.Y + 5));
            Display.disp.DrawRectangle(new Pen(Color.White), TerminalLoc, Display.getWidth(), 20);
            //Draw Settings
            Display.disp.DrawString("Settings", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(SettingsLoc.X + 5, SettingsLoc.Y + 5));
            Display.disp.DrawRectangle(new Pen(Color.White), SettingsLoc, Display.getWidth(), 20);
        }

        public override void Update()
        {
            base.Update();
            if (IsOpen)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    //20 is text size.
                    if (UiMouse.MouseY >= TerminalLoc.Y && UiMouse.MouseY <= TerminalLoc.Y + 20)
                    {
                        DesktopManager.CloseWindow(this);
                        DesktopManager.OpenWindow(Kernel.TerminalInstance);
                    }
                    if (UiMouse.MouseY >= SettingsLoc.Y && UiMouse.MouseY <= SettingsLoc.Y + 20)
                    {
                        DesktopManager.CloseWindow(this);
                        DesktopManager.OpenWindow(new Settings());
                    }
                }
            }
        }
    }
}