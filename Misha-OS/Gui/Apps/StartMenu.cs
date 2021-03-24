using MishaOS.Drivers;
using MishaOS.Gui.Windows;
using MishaOS.Gui.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MishaOS.Gui.Apps
{
    public class StartMenu : Window
    {
        public List<AppReference> apps = new List<AppReference>();
        int AppY = 0;
        public StartMenu()
        {
            this.Text = "Start Menu UI";
            this.ShouldDrawTitleBar = false;
            BackgroundColor = Color.ForestGreen;
            var height = 100;
            this.Size = new Size(100, height);
            this.Location = new Point(0, Display.ScreenHeight - 15 - height);
            this.ShouldDrawTitleBar = false;

            AddApp(new Terminal());
            AddApp(new Settings());
        }

        public override void Update()
        {
            base.Update();
            if (UiMouse.MouseState == Cosmos.System.MouseState.Left)
            {
                //If we are clicking on something other then the startmenu, close
                if (UiMouse.MouseY >= this.Location.Y && UiMouse.MouseY <= this.Location.Y + this.Size.Height)
                {
                    if (UiMouse.MouseX >= this.Location.X && UiMouse.MouseX <= this.Location.X + this.Size.Width)
                    {
                        return;
                    }
                    else { DesktopManager.CloseWindow(this); return; }
                }
                else { DesktopManager.CloseWindow(this); return; }
            }
        }

        /// <summary>
        /// Add a app to the start menu
        /// </summary>
        /// <param name="window">The window</param>
        public void AddApp(Window window)
        {
            Cosmos.System.Graphics.Point loc = new Cosmos.System.Graphics.Point(this.Location.X, AppY);

            Button btn = new Button();
            btn.Location = new System.Drawing.Point(loc.X, loc.Y + 5);
            btn.Size = new Size(this.Size.Width, 10);
            btn.ForeColor = Color.Black;
            btn.Text = window.Text;
            btn.OnClick += delegate (object s, EventArgs e)
            {
                DesktopManager.OpenWindow(window);
            };
            this.Controls.Add(btn);

            apps.Add(new AppReference() { window = window, loc = loc });
            AppY += 11;
        }
    }
}
