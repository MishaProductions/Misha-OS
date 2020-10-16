using Cosmos.Core.IOGroup;
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
            //Boot screen Animation
            Boot(".",true);
            Boot("..");
            Boot("...");
            Boot("....",false,true);
            //After boot screen
            AfterBootScreen();
        }
        //Basic display
        private static Canvas boot;
        static void Boot(string dots,bool isFirst=false,bool isLast=false)
        {
            //INTS
            int barHeight = 50;
            if (isFirst)
            {
                boot = FullScreenCanvas.GetFullScreenCanvas(new Mode(800, 600, ColorDepth.ColorDepth32));
                //Draw stuff
                boot.Clear(Color.DeepSkyBlue);
                boot.DrawFilledRectangle(new Pen(Color.DodgerBlue), new Cosmos.System.Graphics.Point(), 800, barHeight);
                boot.DrawFilledRectangle(new Pen(Color.DodgerBlue), new Cosmos.System.Graphics.Point(0, 600 - barHeight), 800, barHeight);
                boot.DrawString("-----MishaOS Debug Screen-----", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(0, 0));

            }
            boot.DrawFilledRectangle(new Pen(Color.DeepSkyBlue), new Cosmos.System.Graphics.Point(0, barHeight + 20), 800, 20);
            boot.DrawString("MishaOS is loading" + dots, PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(0,barHeight+20));
            //Wait 1 sec
            DelayInMS(1000);
            //Clean up
            if (isLast)
            boot.Disable();
        }
        static void DelayInMS(int ms) // Stops the code for milliseconds and then resumes it (Basically It's delay)
        {
            for (int i = 0; i < ms * 100000; i++)
            {
                ;
                ;
                ;
                ;
                ;
            }
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