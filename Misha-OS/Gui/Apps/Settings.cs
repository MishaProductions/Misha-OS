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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Gui.Apps
{
    public class Settings : Window
    {
        public Cosmos.System.Graphics.Point WindowCLoseP;

        public Settings()
        {
            this.Text = "Setings";
        }
        public void ReDraw()
        {
            //TODO: move all this into the Window Class
            int WindowTittleBarHeight = 20, width = Display.getWidth(), height = Display.getHeight();
            Display.disp.DrawFilledRectangle(new Pen(TitlebarColor), new Cosmos.System.Graphics.Point(), width, WindowTittleBarHeight);//Top Bar
            Display.disp.DrawFilledRectangle(new Pen(Color.DodgerBlue), new Cosmos.System.Graphics.Point(0, 20), width, height);//Contents                                                                                                          //Draw Title
            Display.disp.DrawString(this.Text, PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(0, 0)); //Title bar text
            //Draw Close Button
            Display.disp.DrawFilledRectangle(new Pen(Color.Red), new Cosmos.System.Graphics.Point(width - 20, 0), 25, WindowTittleBarHeight);
            Display.disp.DrawString("X", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(width - 20, 0));
            //End
     
            //Draw the settings contents
            AddInfoText(Kernel.KernelVersion);
            AddInfoText("CPU Manufacture: " + CPU.GetCPUVendorName());
            AddInfoText("Installed RAM: " + CPU.GetAmountOfRAM() + " MB");
            AddInfoText("BIOS Time: " + Cosmos.HAL.RTC.Hour + ":" + Cosmos.HAL.RTC.Minute);
        }
        int infoTextY = 20;
        public void AddInfoText(string text)
        {
            Display.disp.DrawString(text, PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(5, infoTextY));
            infoTextY += 20;
        }
        public override void Open()
        {
            base.Open();
            ReDraw();
        }
        public override void Close()
        {
            base.Close();
        }

        public override void Update()
        {
            base.Update();
            if (IsOpen)
            {
                if (MouseManager.MouseState == MouseState.Left)
                {
                    //20 is the title bar size.
                    if (UiMouse.MouseY >= WindowCLoseP.Y && UiMouse.MouseY <= WindowCLoseP.Y + 20)
                    {
                        DesktopManager.CloseWindow(this);
                        DesktopManager.OpenWindow(new Desktop());
                    }
                }
            }
        }
    }
}
