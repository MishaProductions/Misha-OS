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

        public static string KernelVersion = "MishaOS Version 0.9e";
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
                    UiMouse.Update();
                    if (FPSCounter.ShouldRender)
                    {
                        var str = "FPS: " + FPSCounter.FPS;
                        Display.DrawRectangle(0, 0, MishaOSConfig.DefaultFont.Width * str.Length, MishaOSConfig.DefaultFont.Height, Color.Black);
                        Display.DrawString(str, Color.White, 0, 0);
                    }
                    Display.Render();
                }
            }
            catch (Exception ex)
            {
                BlueScreen.Panic(BlueScreen.GetProperMessage(ex.Message));
            }
        }
    }
}