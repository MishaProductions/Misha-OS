using MishaOS.Drivers;
using MishaOS.Gui;
using MishaOS.Gui.Windows;
using System;
using Sys = Cosmos.System;

namespace MishaOS
{
    public class Kernel : Sys.Kernel
    {
        public static Sys.FileSystem.CosmosVFS FS;

        public static string KernelVersion = "MishaOS Version 0.1.5";
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
                UiMouse.Update();
                foreach (Window w in DesktopManager.OpenWindows)
                {
                    w.Update();
                }
            }
            catch (Exception ex)
            {
                BlueScreen.Panic(BlueScreen.GetProperMessage(ex.Message));
            }
        }
    }
}