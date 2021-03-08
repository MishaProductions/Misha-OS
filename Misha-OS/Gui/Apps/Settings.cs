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
        Button a = new Button();
        Button b = new Button();
        private void DisplaySize_OnClick(object sender, EventArgs e)
        {
            a = new Button();
            a.Text = "640x480";
            a.Location = new System.Drawing.Point(5, 45);
            a.Size = new Size(100, 20);
            a.OnClick += A_OnClick;
            a.Visible = true;
            this.Controls.Add(a);

            b = new Button();
            b.Text = "800x600";
            b.Location = new System.Drawing.Point(5, 65);
            b.Size = new Size(100, 20);
            b.OnClick += B_OnClick;
            b.Visible = true;
            this.Controls.Add(b);
        }
        private void B_OnClick(object sender, EventArgs e)
        {
            a.Visible = false;
            b.Visible = false;
            Display.ScreenWidth = 800;
            Display.ScreenHeight = 600;
            Display.Init();
        }
        private void A_OnClick(object sender, EventArgs e)
        {
            a.Visible = false;
            b.Visible = false;
            Display.ScreenWidth = 640;
            Display.ScreenHeight = 480;
            Display.Init();
        }
        private void InfoTabbtn_OnClick(object sender, EventArgs e)
        {
            InfoTab();
        }
        private void ColorsBtn_OnClick(object sender, EventArgs e)
        {
            ColorTab();
        }
        public void AddTabButtons()
        {
            Button settingsTab = new Button();
            settingsTab.Text = "Settings";
            settingsTab.Location = new System.Drawing.Point();
            settingsTab.Size = new Size(100, 20);
            settingsTab.OnClick += SettingsTab_OnClick;
            this.Controls.Add(settingsTab);

            Button colorsBtn = new Button();
            colorsBtn.Text = "Colors";
            colorsBtn.Location = new System.Drawing.Point(105, 0);
            colorsBtn.Size = new Size(100, 20);
            colorsBtn.OnClick += ColorsBtn_OnClick;
            this.Controls.Add(colorsBtn);


            Button InfoTabbtn = new Button();
            InfoTabbtn.Text = "Info";
            InfoTabbtn.Location = new System.Drawing.Point(210, 0);
            InfoTabbtn.Size = new Size(100, 20);
            InfoTabbtn.OnClick += InfoTabbtn_OnClick;
            this.Controls.Add(InfoTabbtn);
        }

        public void ColorTab()
        {
            infoTextY = 40;
            CurrentTab = 1;
            this.Controls.Clear();
            AddTabButtons();
            this.DrawAll();
            AddInfoText("Coming Soon!");
        }
        public void InfoTab()
        {
            infoTextY = 40;
            CurrentTab = 2;
            this.Controls.Clear();
            AddTabButtons();
            this.DrawAll();

            AddInfoText(Kernel.KernelVersion);
            AddInfoText("CPU: " + CPU.GetCPUBrandString());
            AddInfoText("Installed RAM: " + CPU.GetAmountOfRAM() + " MB");
            AddInfoText("BIOS Time: " + Cosmos.HAL.RTC.Hour + ":" + Cosmos.HAL.RTC.Minute);

            AddInfoText("screen resolution: " + Display.ScreenWidth + "x" + Display.ScreenHeight);
            string driver = "Cosmos Canvas";

            if (BootManager.IsBootedInVmvare)
                driver = "VMvare double buffered display device";

            AddInfoText("Display Driver: " + driver);
            this.DrawAll();
        }
        public void SettingsTab()
        {
            Button DisplaySize = new Button();
            DisplaySize.Location = new System.Drawing.Point(5, 25);
            DisplaySize.Size = new Size(250, 20);
            DisplaySize.Text = "Change Display Size";
            DisplaySize.OnClick += DisplaySize_OnClick;
            this.Controls.Add(DisplaySize);
        }
        public void AddInfoText(string text)
        {
            Label lbl = new Label() { Text = text,ForeColor=Color.White,Location= new System.Drawing.Point(5, infoTextY) };
            this.Controls.Add(lbl);
            infoTextY += 20;
        }
    }
}
