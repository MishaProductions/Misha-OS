using Cosmos.Core;
using Cosmos.System.Graphics;
using MishaOS.Drivers;
using MishaOS.Gui.Windows;
using MishaOS.Gui.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MishaOS.Gui.Apps
{
    public class Settings : Window
    {
        private int infoTextY = 40;

        public int CurrentTab = 0;
        Button InfoTabbtn = new Button();
        Button colorsBtn = new Button();
        Button settingsTab = new Button();
        public Settings()
        {
            this.Text = "Setings";
            this.BackgroundColor = Color.DodgerBlue;
            this.Size = new Size(250, 170);
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
        public void AddTabButtons()
        {
            settingsTab.Text = "Settings";
            settingsTab.Location = new System.Drawing.Point();
            settingsTab.Size = new Size(55, 15);
            settingsTab.OnClick += SettingsTab_OnClick;
            this.Controls.Add(settingsTab);


            colorsBtn.Text = "Colors";
            colorsBtn.Location = new System.Drawing.Point(60, 0);
            colorsBtn.Size = new Size(55, 15);
            colorsBtn.OnClick += ColorsBtn_OnClick;
            this.Controls.Add(colorsBtn);



            InfoTabbtn.Text = "Info";
            InfoTabbtn.Location = new System.Drawing.Point(120, 0);
            InfoTabbtn.Size = new Size(55, 15);
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
                Size = new Size(170, 15),
                Text = "Change Display Size"
            };
            DisplaySize.OnClick += DisplaySize_OnClick;
            this.Controls.Add(DisplaySize);
        }
        private void DisplaySize_OnClick(object sender, EventArgs e)
        {
            var modes = Display.GetModes();
            int y = 45;
            List<Button> btns = new List<Button>();
            foreach (var item in modes)
            {
                var btn = new Button
                {
                    Text = item. Columns+"x"+item.Rows,
                    Location = new System.Drawing.Point(5, y),
                    Size = new Size(60, 15),
                    Tag = item
                };
                btn.OnClick += delegate (object sender2, EventArgs e2)
                {
                    var bb = (Button)sender2;
                    var mode = (Mode)bb.Tag;
                    Display.ScreenWidth = mode.Columns;
                    Display.ScreenHeight = mode.Rows;
                    Display.Init();
                    foreach (var b in btns)
                    {
                        b.Visible = false;
                    }
                    btns.Clear();
                };
                y += 17;
                btns.Add(btn);
                this.Controls.Add(btn);
            }
        }
        public void AddInfoText(string text)
        {
            Label lbl = new Label() { Text = text, ForeColor = Color.White, Location = new System.Drawing.Point(5, infoTextY),BackgroundColor=Color.Transparent };
            this.Controls.Add(lbl);
            infoTextY += 15;
        }
    }
}
