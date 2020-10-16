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
            Label line1 = new Label();
            Label line2 = new Label();
            //Line 1
            line1.Text = "Misha OS App Launcher";
            line1.ForeColor = Color.White;
            line1.Location = new System.Drawing.Point(5, 5);

            //Line 2
            line2.Text = "Select an Application to begin.";
            line2.ForeColor = Color.White;
            line2.Location = new System.Drawing.Point(5, 20);

            //Window
            this.Controls.Add(line1);
            this.Controls.Add(line2);

            //Add apps
            AddApp(new Terminal());
            AddApp(new Settings());
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
            btn.OnClick += delegate(object s, EventArgs e)
            {
                DesktopManager.CloseWindow(this);
                DesktopManager.OpenWindow(window);
            };
            this.Controls.Add(btn);

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