using Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Gui;
using MishaOS.Gui.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace MishaOS.Drivers
{
    public class BootManager
    {
        static bool StartedFS = false;
        public static void Boot()
        {
            BootMessages.Print(SystemdPrintType.Ok, "Boot to console");
            if (!StartedFS)
            {
                BootMessages.Print(SystemdPrintType.Ok, "Start File Systems");
                Kernel.FS = new Cosmos.System.FileSystem.CosmosVFS();
                Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(Kernel.FS);
                StartedFS = true;
            }
            BootMessages.Print(SystemdPrintType.Ok, "Loading gui..");
            AfterBootScreen();
        }
        /// <summary>
        /// Occurs after the system has booted.
        /// </summary>
        public static void AfterBootScreen()
        {
            if (!File.Exists(@"0:\installed.bif"))
            {
                System.Console.WriteLine("MishaOS not detected... Enter S to install MishaOS.");
                System.Console.WriteLine("Otherwise, press anything else to run it of Device.");
                string i = System.Console.ReadLine();
                if (i == "s" | i == "S")
                {
                    Setup s = new Setup(Kernel.FS);
                    s.StartSetup();
                }
                else
                {
                    BootToGui();
                }
            }
            else
            {
                BootToGui();
            }
        }

        private static void BootToGui()
        {
            Display.Init();
            UiMouse.Init();
            DesktopManager.OpenWindow(new Desktop());
        }
    }
}