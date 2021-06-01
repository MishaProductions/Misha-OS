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
        public const int TaskbarHeight = 30;
        public Button btnStart;
        internal static StartMenu StartMenuInstance;
        public Taskbar()
        {
            this.Text = "Taskbar UI";
            BackgroundColor = Color.FromArgb(49, 54, 58);

            this.Size = new Size(Display.ScreenWidth, TaskbarHeight);
            this.Location = new Point(0, Display.ScreenHeight - TaskbarHeight);
            this.ShouldDrawTitleBar = false;
            StartMenuInstance = new StartMenu(this);
            btnStart = new Button
            {
                Location = new Point(0, 0),
                Size = new Size(40, TaskbarHeight),
                Text = "Start",
                BackgroundColor = Color.FromArgb(49, 54, 40),
                ForeColor = Color.White
            };
            btnStart.OnClick += btnStart_OnClick;

            this.Controls.Add(btnStart);
            DesktopManager.OpenWindow(StartMenuInstance);
            StartMenuInstance.Close();
        }
        private void btnStart_OnClick(object sender, EventArgs e)
        {
            btnStart.BackgroundColor = Color.Green;
            StartMenuInstance.Open();
        }
    }
}