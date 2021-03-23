//#define GUIBOOT //Uncomment to enable GUI boot screen
using Cosmos.Core;
using Cosmos.System.FileSystem;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Commands;
using MishaOS.Commands.All;
using MishaOS.Gui;
using MishaOS.Gui.Windows;
using MishaOS.TextUI.Commands;
using System;
using System.Drawing;
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
        public static bool EnableFileSystem = true;
        /// <summary>
        /// Has Misha OS Started file system support
        /// </summary>
        private static bool StartedFS = false;
        public static void Boot()
        {
            BootMessages.Print(SystemdPrintType.Ok, "Boot to console");

            if (!StartedFS && EnableFileSystem)
            {
                BootMessages.Print(SystemdPrintType.Ok, "Start File Systems");
                Kernel.FS = new CosmosVFS();
                Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(Kernel.FS);
                StartedFS = true;
            }

            BootMessages.Print(SystemdPrintType.Ok, "Loading gui..");
            //Boot screen Animation

#if GUIBOOT
Boot(".", true);
Boot("..", false, true);
#else
            VGAScreen.SetTextMode(Cosmos.HAL.VGADriver.TextSize.Size80x50);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.CursorVisible = false;

            Console.WriteLine("MishaOS Version " + Kernel.KernelVersion + ". Kernel Version: unknown-devkit");
            Console.WriteLine("Amount of memory: " + CPU.GetAmountOfRAM() + "mb");
            Cosmos.HAL.Global.PIT.Wait(1000);
            Console.CursorVisible = true;
            VGAScreen.SetTextMode(Cosmos.HAL.VGADriver.TextSize.Size80x25);
#endif

            //After boot screen
            AfterBootScreen();
        }
#if GUIBOOT
        //Basic display
        private static Canvas boot;
        static void Boot(string dots, bool isFirst = false, bool isLast = false)
        {
            if (isFirst)
            {
                boot = FullScreenCanvas.GetFullScreenCanvas(new Mode(800, 600, ColorDepth.ColorDepth32));
                boot.Clear(Color.Black);
            }

            boot.DrawString("MishaOS is starting", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(0, 0));
            //Wait 1 sec
            Cosmos.HAL.Global.PIT.Wait(1000);

            //Clean up
            if (isLast)
                boot.Disable();
        }
#endif
        /// <summary>
        /// Occurs after the system has booted.
        /// </summary>
        public static void AfterBootScreen()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            if (!EnableFileSystem)
            {
                BootToGui();
                return;
            }
            if (!IsBootedInVM)
            {
                Console.WriteLine("MishaOS has detected that you are using real hardware or an unknown virtual machine.");
                Console.WriteLine("File system support has been disabled");
                Console.WriteLine("");
                Console.WriteLine("Amount of memory: " + CPU.GetAmountOfRAM() + "mb");
                Cosmos.HAL.Global.PIT.Wait(1000);
                Console.CursorVisible = true;
                VGAScreen.SetTextMode(Cosmos.HAL.VGADriver.TextSize.Size80x25);
                initGui();
            }
            if (!File.Exists(@"0:\installed.bif"))
            {
                Console.WriteLine("MishaOS is not detected on hard drive. Enter S to install MishaOS.");
                Console.WriteLine("Otherwise, press anything else to not install Misha OS.");
                string i = Console.ReadLine();
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
                BootToGui();
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