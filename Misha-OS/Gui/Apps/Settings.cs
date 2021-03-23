using Cosmos.Core;
using Cosmos.Core.IOGroup;
using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using Microsoft.VisualBasic;
using Microsoft.Win32.SafeHandles;
using MishaOS.Drivers;
using MishaOS.Gui.Windows;
using MishaOS.Gui.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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
            DisplaySize.Location = new System.Drawing.Point(5, 25);
            DisplaySize.Size = new Size(120, 10);
            DisplaySize.Text = "Display Settings are not supported.";
            this.Controls.Add(DisplaySize);
        }
        public void AddInfoText(string text)
        {
            Label lbl = new Label() { Text = text, ForeColor = Color.White, Location = new System.Drawing.Point(5, infoTextY) };
            this.Controls.Add(lbl);
            infoTextY += 10;
        }
    }
}
