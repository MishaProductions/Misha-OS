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

        public static string KernelVersion = "MishaOS Version 0.5.1";
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
                   // DesktopManager.Update();
                    foreach (Window w in DesktopManager.OpenWindows)
                    {
                        if (w != null)
                        {
                            w.UpdateAll();
                            w.Draw();
                        }
                    }
                    UiMouse.Update();
                    if (FPSCounter.ShouldRender)
                    {
                        Display.DrawRectangle(0, 0, 100, 20, Color.Black);
                        Display.DrawString("FPS: " + FPSCounter.FPS, new Sys.Graphics.Pen(Color.White), 0, 0);
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