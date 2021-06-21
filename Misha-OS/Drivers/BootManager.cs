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
        /// Should Misha OS Load the file system on boot? Set this to false if building on real hardware.
        /// </summary>
        public static bool EnableFileSystem = true;
        /// <summary>
        /// Set this to true if you are building for real hardware.
        /// </summary>
        private static bool StartedFS = false;
        /// <summary>
        /// Starts MishaOS
        /// </summary>
        public static void Boot()
        {
            if (!StartedFS && EnableFileSystem)
            {
                Console.WriteLine("Starting FileSystem Driver");
                Kernel.FS = new CosmosVFS();
                VFSManager.RegisterVFS(Kernel.FS);
                StartedFS = true;
            }

            CommandParaser.IsGUI = false;

            Console.WriteLine("Starting VGA Driver");
            VGAImage img = new VGAImage(320, 200);
            img.ParseData(Utils.BootScreen);

            VGADriverII.Initialize(VGAMode.Pixel320x200DB); //Init VGA

            VGADriverII.Clear(0); // clear screen with black

            VGAGraphics.DrawImage(0, 0, img);

            //Render the screen
            VGADriverII.Display();

            //Init stuff
            MishaOSConfig.Init();

            //Wait 3 seconds
            DelayInMS(3000);

            //If we have not enabled file system, show a message and boot dirrectly to the GUI
            if (!EnableFileSystem)
            {
                VGADriverII.Clear(0); // clear screen with black
                VGAGraphics.DrawString(0, 0, "MishaOS has detected that you are using", VGAColor.White, VGAFont.Font8x8);
                VGAGraphics.DrawString(0, 9, "real hardware or an unknown virtual", VGAColor.White, VGAFont.Font8x8);
                VGAGraphics.DrawString(0, 18, "machine. File system support has", VGAColor.White, VGAFont.Font8x8);
                VGAGraphics.DrawString(0, 27, "been disabled.", VGAColor.White, VGAFont.Font8x8);
                VGADriverII.Display();
                DelayInMS(3000);
                initGui();
                return;
            }

            //Check if MishaOS is installed. If not, show a message
            //!MishaOSConfig.IsInstalled()
            if (!MishaOSConfig.IsInstalled())
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
            VGADriverII.Clear(0); // clear screen with black

            VGAGraphics.DrawString(0, 0, "Interfaces: ", VGAColor.White, VGAFont.Font8x8);
            VGAGraphics.DrawString(0, 9, "1. Graphical user interface", VGAColor.White, VGAFont.Font8x8);
            VGAGraphics.DrawString(0, 18, "2. Console interface", VGAColor.White, VGAFont.Font8x8);
            VGAGraphics.DrawString(0, 27, "Please enter the interface number", VGAColor.White, VGAFont.Font8x8);
            VGADriverII.Display();

            var input = Console.ReadKey();
            if (input.KeyChar == '1')
            {
                initGui();
            }
            else if (input.KeyChar == '2')
            {
                VGADriverII.SetMode(VGAMode.Text80x25);
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
            //disable vga
            VGADriverII.SetMode(VGAMode.Text80x25);

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