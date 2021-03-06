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
using static MishaOS.Gui.Windows.Controls.Button;

namespace MishaOS.Gui.Windows
{
    /// <summary>
    /// Desktop App
    /// </summary>
    public class Desktop : Window
    {
        public List<AppReference> apps = new List<AppReference>();
        int AppY = 40;
        public Desktop()
        {
            this.Text = "Desktop";
            BackgroundColor = Color.ForestGreen;
            //Add apps
            //   AddApp(new Terminal());
            //  AddApp(new Settings());
        }

        public override void Draw()
        {
            base.Draw();
            Graphics.Label("Misha OS App Launcher", this.ClientLocation.X + 5, this.ClientLocation.Y + 5, new ControlData() { ForeColor = Color.White });
            Graphics.Label("Select an Application to begin.", this.ClientLocation.X + 5, this.ClientLocation.Y + 20, new ControlData() { ForeColor = Color.White });

            if (Graphics.Button("MishaOS Terminal", this.ClientLocation.X + 5, this.ClientLocation.Y + 40, this.Size.Width, 20))
            {
                DesktopManager.OpenWindow(new Terminal());
            }

            if (Graphics.Button("MishaOS Settings", this.ClientLocation.X + 5, this.ClientLocation.Y + 70, this.Size.Width, 20))
            {
                DesktopManager.OpenWindow(new Settings());
            }
        }
        /// <summary>
        /// Add a app
        /// </summary>
        /// <param name="window">TThe window</param>
        public void AddApp(Window window)
        {
            Cosmos.System.Graphics.Point loc = new Cosmos.System.Graphics.Point(this.Location.X, AppY);

            Button btn = new Button();
            btn.Location = new System.Drawing.Point(loc.X, loc.Y + 5);
            btn.Size = new Size(this.Size.Width, 20);
            btn.ForeColor = Color.Black;
            btn.Text = window.Text;
            btn.OnClick += delegate (object s, EventArgs e)
            {
                DesktopManager.OpenWindow(window);
                // DesktopManager.CloseWindow(this);
            };
            this.Controls.Add(btn);

            apps.Add(new AppReference() { window = window, loc = loc });
            AppY += 30;
        }
    }
    public class AppReference
    {
        public Cosmos.System.Graphics.Point loc;
        public Window window;
    }
}