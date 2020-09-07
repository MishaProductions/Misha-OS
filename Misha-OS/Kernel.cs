

using Cosmos.HAL.Drivers.PCI.Video;
using Cosmos.System;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.Graphics;
using MishaOS.Drivers;
using MishaOS.Gui;
using MishaOS.Gui.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using Sys = Cosmos.System;

namespace MishaOS
{
    public class Kernel : Sys.Kernel
    {
        public static Sys.FileSystem.CosmosVFS FS;
        public static Terminal TerminalInstance;
        public static Desktop DesktopInstance;

        public static string KernelVersion = "MishaOS Version 0.1";
        protected override void BeforeRun()
        {
            try
            {
                BootManager.Boot();

            }
            catch (Exception ex)
            {
                BlueScreen.Panic(ex.Message.ToUpper().Replace(" ", "_"), null);
            }
        }

        protected override void Run()
        {
            try
            {
                UiMouse.Update();
                //TerminalInstance.Update();
                //DesktopInstance.Update();
                foreach(Window w in DesktopManager.OpenWindows)
                {
                    w.Update();
                }
            }
            catch(Exception ex)
            {
                BlueScreen.Panic(ex.Message.ToUpper().Replace(" ","_"),null);
            }
        }
    }
}