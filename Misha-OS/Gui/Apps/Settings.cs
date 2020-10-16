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
        private int infoTextY = 20;
        public Settings()
        {
            this.Text = "Setings";
            this.BackgroundColor = Color.DodgerBlue;
            AddInfoText(Kernel.KernelVersion);
            AddInfoText("CPU Manufacture: " + CPU.GetCPUVendorName());
            AddInfoText("Installed RAM: " + CPU.GetAmountOfRAM() + " MB");
            AddInfoText("BIOS Time: " + Cosmos.HAL.RTC.Hour + ":" + Cosmos.HAL.RTC.Minute);
        }
        public void AddInfoText(string text)
        {
            Label lbl = new Label() { Text = text,ForeColor=Color.White,Location= new System.Drawing.Point(5, infoTextY) };
            this.Controls.Add(lbl);
            infoTextY += 20;
        }
    }
}
