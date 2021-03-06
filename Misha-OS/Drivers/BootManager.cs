using Cosmos.Core.IOGroup;
using Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Commands;
using MishaOS.Commands.All;
using MishaOS.Gui;
using MishaOS.Gui.Windows;
using MishaOS.TextUI.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace MishaOS.Drivers
{
    public class BootManager
    {
        /// <summary>
        /// Is the computer booted in a vm?
        /// This is used for the display driver.
        /// </summary>
        public static bool IsBootedInVM
        {
            get
            {
                return Cosmos.HAL.PCI.GetDevice(Cosmos.HAL.VendorID.VMWare, Cosmos.HAL.DeviceID.SVGAIIAdapter) != null;
            }
        }
        static bool StartedFS = false;
        public static void Boot()
        {
            BootMessages.Print(SystemdPrintType.Ok, "Boot to console");
            if (!StartedFS)
            {
                BootMessages.Print(SystemdPrintType.Ok, "Start File Systems");
                Kernel.FS = new CosmosVFS();
                Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(Kernel.FS);
                StartedFS = true;
            }

            BootMessages.Print(SystemdPrintType.Ok, "Loading gui..");
            //Boot screen Animation
            Boot(GetProgressString(0), true);
            Boot(GetProgressString(798), false, true);

            //After boot screen
            AfterBootScreen();
        }

        private static string GetProgressString(int prg)
        {
            // Screen width - 2 brackets - progress = Progress char size
            var Maxlen = 798;
            string prgBar = "";

            for (int i = 0; i < Maxlen; i++)
            {
                if (prg > 0)
                    prgBar += "X";
                else
                    prgBar += " ";

                prg--;
            }
            return "[" + prgBar + "]";
        }
        //Basic display
        private static Canvas boot;
        static void Boot(string dots, bool isFirst = false, bool isLast = false)
        {

            //INTS
            if (isFirst)
            {
                boot = FullScreenCanvas.GetFullScreenCanvas(new Mode(800, 600, ColorDepth.ColorDepth32));
                boot.Clear(Color.Black);
            }
            boot.DrawString("MishaOS is starting", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(0, 0));

            boot.DrawString(dots, PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(0, 600 - 20));
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
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.BackgroundColor = ConsoleColor.DarkBlue;
                System.Console.Clear();
                System.Console.WriteLine("MishaOS is not detected on hard drive. Enter S to install MishaOS.");
                System.Console.WriteLine("Otherwise, press anything else to not install Misha OS.");
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
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.DarkBlue;
            System.Console.Clear();

            System.Console.WriteLine("Interfaces: \n1. GUI\n2. CLI");
            TextWindows.Draw("Enter interface number", 0, 5);
            var input = System.Console.ReadLine();
            System.Console.Clear();
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.Black;
            if (input == "1")
            {
                System.Console.Clear();
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.BackgroundColor = ConsoleColor.Black;
                CommandParaser.IsGUI = true;

                Display.Init();
                UiMouse.Init();
                DesktopManager.OpenWindow(new Desktop());
                Display.Render();
            }
            else if (input == "2")
            {
                System.Console.Clear();
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.BackgroundColor = ConsoleColor.Black;
                CommandParaser.IsGUI = false;

                var term = new TextTerm();
                term.Write(term.CurrentDIR);
                while (!CommandParaser.IsGUI)
                {
                    var input2 = term.ReadLine();
                    CommandParaser.ProcessCommand(term, input2);
                    term.Write(term.CurrentDIR);
                }
            }
            else
            {
                System.Console.Clear();
                System.Console.WriteLine("Unknown Input");
                BootToGui();
            }
        }
    }
}