using Cosmos.Core;
using Cosmos.Core.Memory;
using MishaOS.Drivers;
using MishaOS.Gui;
using MishaOS.Gui.Windows;
using MishaOS.TextUI.Commands;
using System;
using System.Drawing;
using System.Linq;
using Sys = Cosmos.System;

namespace MishaOS
{
    public class Kernel : Sys.Kernel
    {
        public static Sys.FileSystem.CosmosVFS FS;
        public static string KernelVersion = "MishaOS Version 1.0";

        private static ulong UsedMemory = 0;
        protected override void BeforeRun()
        {
            try
            {
                BootManager.Boot();
            }
            catch (Exception ex)
            {
                BlueScreen.Panic(BlueScreen.GetProperMessage(ex.Message));
            }
        }

        protected override void Run()
        {
            try
            {
                Display.Clear(Color.Black);

                DesktopManager.Update();
                UiMouse.Update();

                if (FPSCounter.ShouldRender)
                {
                    UsedMemory = GCImplementation.GetUsedRAM() / 1024 / 2024;
                    var freeRam = GCImplementation.GetAvailableRAM();
                    var f = Heap.Collect();


                    var str = "FPS: " + FPSCounter.FPS + ", " + UsedMemory + "MB used ram, " + freeRam + "MB free, freed " + f + " objects";
                    Display.DrawRectangle(0, 0, MishaOSConfig.DefaultFont.Width * str.Length, MishaOSConfig.DefaultFont.Height, Color.Black);
                    Display.DrawString(str, Color.White, 0, 0);
                }
                Display.Render();
            }
            catch (Exception ex)
            {
                BlueScreen.Panic(BlueScreen.GetProperMessage(ex.Message));
            }
        }
    }
}