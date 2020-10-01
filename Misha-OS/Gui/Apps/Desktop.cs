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
            //Draw intro text
            Display.disp.DrawString("Misha OS Welcome Screen", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(5, 5));
            Display.disp.DrawString("Select an Application to begin.", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(5, 20));

            AddApp(new Terminal());
            AddApp(new Settings());

        }

        public override void Update()
        {
            base.Update();
            if (IsOpen)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    //20 is text size.

                    //Check if each app was clicked
                    foreach(AppReference app in apps)
                    {
                        Window win = app.window;
                        if (UiMouse.MouseY >= app.loc.Y && UiMouse.MouseY <= app.loc.Y + 20)
                        {
                            DesktopManager.CloseWindow(this);
                            DesktopManager.OpenWindow(win);
                        }
                    }
                }
            }
        }
        public List<AppReference> apps = new List<AppReference>();
        int AppY = 40;
        /// <summary>
        /// Add a app
        /// </summary>
        /// <param name="window">TThe window</param>
        public void AddApp(Window window)
        {
            Cosmos.System.Graphics.Point loc = new Cosmos.System.Graphics.Point(0, AppY);
            Display.disp.DrawString(window.Text, PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(loc.X + 5, loc.Y + 5));
            Display.disp.DrawRectangle(new Pen(Color.White), loc, Display.getWidth(), 20);
            apps.Add(new AppReference() { window=window,loc=loc});
            AppY += 30;
        }
    }
    public class AppReference
    {
        public Cosmos.System.Graphics.Point loc;
        public Window window;
    }
}