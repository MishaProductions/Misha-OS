﻿using Cosmos.HAL;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using MishaOS.Commands;
using MishaOS.Commands.All;
using MishaOS.Drivers.Video;
using MishaOS.Gui;
using MishaOS.Gui.Windows;
using MishaOS.TextUI.Commands;
using System;
using System.IO;

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
                return Cosmos.HAL.PCI.GetDevice(Cosmos.HAL.VendorID.VMWare, Cosmos.HAL.DeviceID.SVGAIIAdapter) != null ||
                    Cosmos.HAL.PCI.GetDevice(Cosmos.HAL.VendorID.Bochs, Cosmos.HAL.DeviceID.BGA) != null ||
                    Cosmos.HAL.PCI.GetDevice(Cosmos.HAL.VendorID.VirtualBox, Cosmos.HAL.DeviceID.VBVGA) != null;
            }
        }
        /// <summary>
        /// Should Misha OS Load the file system on boot?
        /// </summary>
        public static bool EnableFileSystem = false;
        /// <summary>
        /// Set this to true if you are building for real hardware.
        /// </summary>
        private static bool StartedFS = false;
        public static void Boot()
        {
            if (!StartedFS && EnableFileSystem)
            {
                Kernel.FS = new CosmosVFS();
                VFSManager.RegisterVFS(Kernel.FS);

                StartedFS = true;
            }

            VGAImage img = new VGAImage(320, 200);
            img.ParseData(Utils.BootScreen);

            VGADriverII.Initialize(VGAMode.Pixel320x200DB); //Init VGA

            VGADriverII.Clear(0); // clear screen with black

            VGAGraphics.DrawImage(0, 0, img);

            //Render the screen
            VGADriverII.Display();
            ColorPalette.Init();
            //Wait 2 seconds
            Cosmos.HAL.Global.PIT.Wait(2000);

            //If we have not enabled file system, show a message and boot dirrectly to the GUI
            if (!EnableFileSystem)
            {
                VGADriverII.Clear(0); // clear screen with black
                VGAGraphics.DrawString(0, 0, "MishaOS has detected that you are using", VGAColor.White, VGAFont.Font8x8); 
                VGAGraphics.DrawString(0, 9, "real hardware or an unknown virtual", VGAColor.White, VGAFont.Font8x8);
                VGAGraphics.DrawString(0, 18, "machine. File system support has", VGAColor.White, VGAFont.Font8x8);
                VGAGraphics.DrawString(0, 27, "been disabled.", VGAColor.White, VGAFont.Font8x8);
                VGADriverII.Display();
                Cosmos.HAL.Global.PIT.Wait(3000);
                initGui();
                return;
            }

            //Check if MishaOS is installed. If not, show a message

            if (!File.Exists(@"0:\installed.bif"))
            {
                VGADriverII.Clear(0); // clear screen with black

                VGAGraphics.DrawString(0, 0, "MishaOS is not detected on hard drive.", VGAColor.White, VGAFont.Font8x8);
                VGAGraphics.DrawString(0, 9, "Press S to install MishaOS.", VGAColor.White, VGAFont.Font8x8);
                VGAGraphics.DrawString(0, 18, "Otherwise, press anything else to not", VGAColor.White, VGAFont.Font8x8);
                VGAGraphics.DrawString(0, 27, "install Misha OS.", VGAColor.White, VGAFont.Font8x8);
                VGADriverII.Display();

                while (true)
                {
                    var eventt = Cosmos.System.KeyboardManager.ReadKey();

                    if (eventt != null)
                    {
                        if (eventt.KeyChar == 'S' | eventt.KeyChar == 's')
                        {
                            CommandParaser.IsGUI = true;

                            Display.Init();
                            UiMouse.Init();
                            DesktopManager.OpenWindow(new SetupWindow());
                            Display.Render();
                            return;
                        }
                        else
                        {
                            InterfaceSelector();
                            break;
                        }
                    }
                }
            }
            else
            {
                InterfaceSelector();
            }
        }
        /// <summary>
        /// Shows interface selector
        /// </summary>
        private static void InterfaceSelector()
        {
            //Disable the VGA driver
            VGADriverII.Initialize(VGAMode.Text80x50);


            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            Console.WriteLine("Interfaces: \n1. GUI\n2. CLI");
            TextWindows.Draw("Enter interface number", 0, 5);


            var input = Console.ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            if (input.KeyChar == '1')
            {
                initGui();
            }
            else if (input.KeyChar == '2')
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
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
                InterfaceSelector();
            }
        }
        private static void initGui()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            CommandParaser.IsGUI = true;

            Display.Init();
            UiMouse.Init();
            DesktopManager.OpenWindow(new Taskbar());
            Display.Render();
        }
    }
}