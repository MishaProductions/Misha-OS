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
        int AppY = 0;
        Taskbar taskbar;
        public StartMenu(Taskbar parent)
        {
            this.Text = "Start Menu UI";
            this.ShouldDrawTitleBar = false;
            BackgroundColor = Color.ForestGreen;
            var height = 100;
            this.Size = new Size(100, height);
            this.Location = new Point(0, Display.ScreenHeight - Taskbar.TaskbarHeight - height);
            this.ShouldDrawTitleBar = false;
            taskbar = parent;

            AddApp(new Terminal());
            AddApp(new Settings());
        }

        public override void Update()
        {
            base.Update();
            if (UiMouse.MouseState == Cosmos.System.MouseState.Left)
            {
                //If we are clicking on something other then the startmenu, close
                if (!Utils.DoesMouseCollideWithArea(Location.X, Location.Y, Size.Width, Size.Height + 15))
                {
                    CloseStart();
                }
            }
        }
        private void CloseStart()
        {
            taskbar.btnStart.BackgroundColor = Color.FromArgb(49, 54, 40);
            Close();
        }

        /// <summary>
        /// Add a app to the start menu
        /// </summary>
        /// <param name="window">The window</param>
        public void AddApp(Window window)
        {
            Cosmos.System.Graphics.Point loc = new Cosmos.System.Graphics.Point(this.Location.X, AppY);

            Button btn = new Button
            {
                Location = new System.Drawing.Point(loc.X, loc.Y + 5),
                Size = new Size(this.Size.Width, 15),
                ForeColor = Color.Black,
                Text = window.Text
            };
            btn.OnClick += delegate (object s, EventArgs e)
            {
                DesktopManager.OpenWindow(window);
                CloseStart();
            };
            this.Controls.Add(btn);
            AppY += 20;
        }
    }
}
