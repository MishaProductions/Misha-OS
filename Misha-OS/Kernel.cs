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

        public static string KernelVersion = "MishaOS Version 0.4 (Unstable)";
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
               if (CommandParaser.IsGUI)
                {
                    Display.Clear(Color.DodgerBlue);
                    DesktopManager.Update();
                    foreach (Window w in DesktopManager.OpenWindows)
                    {
                        if (w != null)
                        {
                            w.UpdateAll();
                            w.Draw();
                        }
                    }
                    UiMouse.Update();
                    Display.Render();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                BlueScreen.Panic(BlueScreen.GetProperMessage(ex.Message));
            }
        }
    }
}