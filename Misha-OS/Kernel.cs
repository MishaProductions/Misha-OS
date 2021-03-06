using Cosmos.System.Network;
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

        public static string KernelVersion = "MishaOS Version 0.6";
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
                while (CommandParaser.IsGUI)
                {
                    //TODO: maybe add threads to update the screen?
                    Display.Clear(Color.DodgerBlue);

                    DesktopManager.Update();
                    UiMouse.Update();
                    Display.Render();

                    NetworkStack.Update();
                }
            }
            catch (Exception ex)
            {
                BlueScreen.Panic(BlueScreen.GetProperMessage(ex.Message));
            }
        }
    }
}