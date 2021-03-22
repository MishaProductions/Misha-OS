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

        public static string KernelVersion = "MishaOS Version 0.7a";
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
                    //TODO: maybe add threads to update the screen?
                    Display.Clear(Color.DodgerBlue);

                    DesktopManager.Update();
                    UiMouse.Update();
                    if (FPSCounter.ShouldRender)
                    {
                        Display.DrawRectangle(0, 0, 40, 10, Color.Black);
                        Display.DrawString("FPS: " + FPSCounter.FPS, Pens.White, 0, 0);
                    }
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