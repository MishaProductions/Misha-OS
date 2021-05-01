using Cosmos.Core;
using MishaOS.Drivers;
using MishaOS.Gui.Windows;
using MishaOS.Gui.Windows.Controls;
using System;
using System.Drawing;

namespace MishaOS.Gui.Apps
{
    public class Settings : Window
    {
        private int infoTextY = 40;

        public int CurrentTab = 0;
        public Settings()
        {
            this.Text = "Setings";
            this.BackgroundColor = Color.DodgerBlue;
            this.Size = new Size(200, 150);
            AddTabButtons();
            SettingsTab();
        }

        private void SettingsTab_OnClick(object sender, EventArgs e)
        {
            CurrentTab = 0;
            this.Controls.Clear();
            AddTabButtons();
            this.DrawAll();
            SettingsTab();
        }
        private void InfoTabbtn_OnClick(object sender, EventArgs e)
        {
            InfoTab();
        }
        private void ColorsBtn_OnClick(object sender, EventArgs e)
        {
            ColorTab();
        }
        Button InfoTabbtn = new Button();
        Button colorsBtn = new Button();
        Button settingsTab = new Button();
        public void AddTabButtons()
        {

            settingsTab.Text = "Settings";
            settingsTab.Location = new System.Drawing.Point();
            settingsTab.Size = new Size(50, 10);
            settingsTab.OnClick += SettingsTab_OnClick;
            this.Controls.Add(settingsTab);


            colorsBtn.Text = "Colors";
            colorsBtn.Location = new System.Drawing.Point(55, 0);
            colorsBtn.Size = new Size(50, 10);
            colorsBtn.OnClick += ColorsBtn_OnClick;
            this.Controls.Add(colorsBtn);



            InfoTabbtn.Text = "Info";
            InfoTabbtn.Location = new System.Drawing.Point(110, 0);
            InfoTabbtn.Size = new Size(50, 10);
            InfoTabbtn.OnClick += InfoTabbtn_OnClick;
            this.Controls.Add(InfoTabbtn);
        }

        public void ColorTab()
        {
            infoTextY = 20;
            CurrentTab = 1;
            this.Controls.Clear();
            AddTabButtons();
            this.DrawAll();
            AddInfoText("Coming Soon!");
        }
        public void InfoTab()
        {
            infoTextY = 20;
            CurrentTab = 2;
            this.Controls.Clear();
            AddTabButtons();
            this.DrawAll();

            AddInfoText(Kernel.KernelVersion);
            AddInfoText("CPU: " + CPU.GetCPUBrandString());
            AddInfoText("Installed RAM: " + CPU.GetAmountOfRAM() + " MB");
            AddInfoText("BIOS Time: " + Cosmos.HAL.RTC.Hour + ":" + Cosmos.HAL.RTC.Minute);
            AddInfoText("screen resolution: " + Display.ScreenWidth + "x" + Display.ScreenHeight);
            AddInfoText("Display Driver: " + Display.DisplayDriverName);
            this.DrawAll();
        }
        Button DisplaySize = new Button();
        public void SettingsTab()
        {
            DisplaySize = new Button
            {
                Location = new System.Drawing.Point(5, 25),
                Size = new Size(220, 15),
                Text = "Change Display Size"
            };
            DisplaySize.OnClick += DisplaySize_OnClick;
            this.Controls.Add(DisplaySize);
        }

        Button DisplaySizeOptionA = new Button();
        Button DisplaySizeOptionB = new Button();
        private void DisplaySize_OnClick(object sender, EventArgs e)
        {
            DisplaySizeOptionA = new Button
            {
                Text = "320x200",
                Location = new System.Drawing.Point(5, 45),
                Size = new Size(60, 15)
            };
            DisplaySizeOptionA.OnClick += A_OnClick;
            DisplaySizeOptionA.Visible = true;
            this.Controls.Add(DisplaySizeOptionA);

            if (BootManager.HasSVGA)
            {
                DisplaySizeOptionB = new Button
                {
                    Text = "640x480",
                    Location = new System.Drawing.Point(5, 65),
                    Size = new Size(60, 15)
                };
                DisplaySizeOptionB.OnClick += B_OnClick;
                DisplaySizeOptionB.Visible = true;
                this.Controls.Add(DisplaySizeOptionB);
            }
        }
        private void B_OnClick(object sender, EventArgs e)
        {
            DisplaySizeOptionA.Visible = false;
            DisplaySizeOptionB.Visible = false;
            Display.ScreenWidth = 640;
            Display.ScreenHeight = 480;
            Display.Init();
        }
        private void A_OnClick(object sender, EventArgs e)
        {
            DisplaySizeOptionA.Visible = false;
            DisplaySizeOptionB.Visible = false;
            Display.ScreenWidth = 320;
            Display.ScreenHeight = 200;
            Display.Init();
        }
        public void AddInfoText(string text)
        {
            Label lbl = new Label() { Text = text, ForeColor = Color.White, Location = new System.Drawing.Point(5, infoTextY) };
            this.Controls.Add(lbl);
            infoTextY += 10;
        }
    }
}
