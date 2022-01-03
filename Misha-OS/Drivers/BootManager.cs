using Cosmos.Core;
using Cosmos.HAL;
using Cosmos.HAL.Drivers;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using MishaOS.Commands.All;
using MishaOS.Gui;
using MishaOS.Gui.Windows;
using MishaOS.TextUI.Commands;
using System;

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
        /// Has SVGAII?
        /// </summary>
        public static bool HasSVGA
        {
            get
            {
                return Cosmos.HAL.PCI.GetDevice(Cosmos.HAL.VendorID.VMWare, Cosmos.HAL.DeviceID.SVGAIIAdapter) != null;
            }
        }
        /// <summary>
        /// Has VBE
        /// </summary>
        public static bool HasVBE
        {
            get
            {
                return VBE.IsAvailable() ||
                    ((PCI.GetDevice(VendorID.VirtualBox, DeviceID.VBVGA)) != null) || //VirtualBox Video Adapter PCI Mode
                    ((PCI.GetDevice(VendorID.Bochs, DeviceID.BGA)) != null) || //BOCHS vbe
                    VBEDriver.ISAModeAvailable();
            }
        }
        /// <summary>
        /// Set this to true if you don't want to enable filesystem.
        /// </summary>
        private static bool StartedFS = false;
        /// <summary>
        /// Starts MishaOS
        /// </summary>
        public static void Boot()
        {
            if (!StartedFS)
            {
                Console.WriteLine("Starting FileSystem Driver");
                Kernel.FS = new CosmosVFS();
                VFSManager.RegisterVFS(Kernel.FS);
                StartedFS = true;
            }

            CommandParaser.IsGUI = true;

            Display.Init();
            UiMouse.Init();
            //DesktopManager.OpenWindow(new Taskbar());
            return;

            CommandParaser.IsGUI = false;
            //Init stuff
            MishaOSConfig.Init();

            //Check if MishaOS is installed. If not, show a message
            if (!MishaOSConfig.IsInstalled())
            {
                Console.Clear();
                Console.WriteLine("MishaOS is not installed. Would you like to install MishaOS now? Press S, if yes. If not, press anything else.");
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
        /// Waits in Milliseconds
        /// </summary>
        /// <param name="ms"></param>
        private static void DelayInMS(int ms)
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
        /// Shows interface selector
        /// </summary>
        private static void InterfaceSelector()
        {
            Console.Clear();
            Console.WriteLine("Interfaces:");
            Console.WriteLine("1. Graphical user interface");
            Console.WriteLine("2. Console interface");
            Console.WriteLine();
            Console.WriteLine("Please enter the interface number");

            var input = Console.ReadKey();
            if (input.KeyChar == '1')
            {
                initGui();
            }
            else if (input.KeyChar == '2')
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.BackgroundColor = ConsoleColor.Black;
                CommandParaser.IsGUI = false;

                var term = new TextTerm();
                term.WriteLine("Misha OS Console");
                term.WriteLine("Version: " + Kernel.KernelVersion);
                term.WriteLine("Running on a " + CPU.GetCPUBrandString());
                term.Write(term.CurrentDIR + ">");
                while (!CommandParaser.IsGUI)
                {
                    var input2 = term.ReadLine();
                    CommandParaser.ProcessCommand(term, input2);
                    term.Write(term.CurrentDIR + ">");
                }
            }
            else
            {
                InterfaceSelector();
            }
        }
        /// <summary>
        /// Starts the GUI
        /// </summary>
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