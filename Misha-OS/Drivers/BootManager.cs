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
            //Canvas bootScreen = FullScreenCanvas.GetFullScreenCanvas(new Mode(800,600,ColorDepth.ColorDepth32));
            //bootScreen.Clear();
            //bootScreen.DrawString("Starting Misha OS",PCScreenFont.Default,new Pen(Color.White),new Cosmos.System.Graphics.Point());
            //Cosmos.HAL.Global.PIT.Wait(3000);
            //bootScreen.Disable();
            BootMessages.Print(SystemdPrintType.Ok, "Boot to console");
            // Cosmos.HAL.Global.PIT.Wait(2000);
            //     VGAScreen.SetTextMode(Cosmos.HAL.VGADriver.TextSize.Size40x50);
            BootMessages.Print(SystemdPrintType.Ok, "Load fonts");

            //   Cosmos.HAL.Global.PIT.Wait(2000);
            if (!StartedFS)
            {
                BootMessages.Print(SystemdPrintType.Ok, "Start File Systems");
                Kernel.FS = new Cosmos.System.FileSystem.CosmosVFS();
                Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(Kernel.FS);
                StartedFS = true;
            }



            BootMessages.Print(SystemdPrintType.Ok, "Loading gui..");
            // Cosmos.HAL.Global.PIT.Wait(2000);
            AfterBootScreen();
        }
        /// <summary>
        /// Occurs after the boot screen has loaded.
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
            Kernel.TerminalInstance = new Terminal();
            Kernel.DesktopInstance = new Desktop();
            DesktopManager.OpenWindow(Kernel.DesktopInstance);
        }
    }
}