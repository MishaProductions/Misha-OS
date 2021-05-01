using MishaOS.Drivers;
using MishaOS.Gui.Apps;
using MishaOS.Gui.Windows.Controls;
using System;
using System.Drawing;

namespace MishaOS.Gui.Windows
{
    /// <summary>
    /// Desktop App
    /// </summary>
    public class Taskbar : Window
    {
        private StartMenu StartMenu;
        public Taskbar()
        {
            this.Text = "Taskbar UI";
            BackgroundColor = Color.ForestGreen;

            this.Size = new Size(Display.ScreenWidth, 15);
            this.Location = new Point(0, Display.ScreenHeight - 15);
            this.ShouldDrawTitleBar = false;


            Button start = new Button
            {
                Location = new Point(0, 0),
                Size = new Size(30, 15),
                Text = "Start",
                BackgroundColor = Color.Green
            };
            start.OnClick += Start_OnClick;
            this.Controls.Add(start);
        }
        private void Start_OnClick(object sender, EventArgs e)
        {
            StartMenu = new StartMenu();

            DesktopManager.OpenWindow(StartMenu);
        }
    }
}