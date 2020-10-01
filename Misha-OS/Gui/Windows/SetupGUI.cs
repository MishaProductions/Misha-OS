using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;

namespace MishaOS.Gui
{
    public static class SetupGUI
    {
        static Canvas c;
        public static void Begin()
        {
            try
            {
                c = FullScreenCanvas.GetFullScreenCanvas(new Mode(800, 600, ColorDepth.ColorDepth32));
                c.Clear(Color.DodgerBlue);
                WelcomeUI();
                while (KeyboardManager.ReadKey().Key != ConsoleKeyEx.Enter) { }

                //FORMATING SCREEN
                FormatUI(50);
                Kernel.FS.Format("0", "FAT32", true);

                //Write Screen
                WriteUI(0);
                Kernel.FS.CreateFile(@"0:\installed.bif");
                Kernel.FS.SetFileSystemLabel("0", "MishaOS");

                WriteUI(50);

                Kernel.FS.CreateDirectory(@"0:\System");

                WriteUI(98);

                Cosmos.HAL.Global.PIT.Wait(5000);

                WriteUI(99);

                Cosmos.HAL.Global.PIT.Wait(1000);

                CompleteUI();
                while (KeyboardManager.ReadKey().Key != ConsoleKeyEx.Enter) { }

                Power.Reboot();
            }
            catch(Exception ex)
            {
                c.Clear(Color.DodgerBlue);
                DrawString("Fatal Error: "+ex.Message, Color.White, new Cosmos.System.Graphics.Point(0,0));
            }
        }
        public static void WelcomeUI()
        {
            int ScreenWidth = 800;
            int ScreenHeight = 600;
            int panSizeH = 150;
            int panSizeW = 300;
            int panX = ScreenHeight / 2 - panSizeH / 2;
            int panY = ScreenWidth / 2 - panSizeW / 2;
            //Welcome Screen
            c.DrawFilledRectangle(new Pen(Color.White), new Cosmos.System.Graphics.Point(panY, panX), panSizeW, panSizeH);
            DrawString("MishaOS Setup", Color.Black, new Cosmos.System.Graphics.Point(panY + 5, panX + 5));

            DrawString("Enter = Contine", Color.White, new Cosmos.System.Graphics.Point(panY, panX + panSizeH));
        }
        public static void WriteUI(int prg)
        {
            int panSizeH = 150;
            int panSizeW = 300;
            int ScreenWidth = 800;
            int ScreenHeight = 600;

            int panX = ScreenHeight / 2 - panSizeH / 2;
            int panY = ScreenWidth / 2 - panSizeW / 2;
            c.Clear(Color.DodgerBlue);
            c.DrawFilledRectangle(new Pen(Color.White), new Cosmos.System.Graphics.Point(panY, panX), panSizeW, panSizeH);
          //  DrawString("MishaOS Setup", Color.Black, new Cosmos.System.Graphics.Point(panY + 5, panX + 5));
            DrawString("Writing - "+prg.ToString()+"% Complete", Color.Black, new Cosmos.System.Graphics.Point(panY + 40, panX));
            DrawString("Setup Is Creating Files...", Color.White, new Cosmos.System.Graphics.Point(panY, panX + panSizeH));
        }
        public static void FormatUI(int prg)
        {
            int ScreenWidth = 800;
            int ScreenHeight = 600;
            int panSizeH = 150;
            int panSizeW = 300;
            int panX = ScreenHeight / 2 - panSizeH / 2;
            int panY = ScreenWidth / 2 - panSizeW / 2;
            c.Clear(Color.DodgerBlue);
            c.DrawFilledRectangle(new Pen(Color.White), new Cosmos.System.Graphics.Point(panY, panX), panSizeW, panSizeH);
          //  DrawString("MishaOS Setup", Color.Black, new Cosmos.System.Graphics.Point(panY + 5, panX + 5));
            DrawString("Formating - 0% Complete", Color.Black, new Cosmos.System.Graphics.Point(panY + 40, panX));
            DrawString("Setup Is Formating...", Color.White, new Cosmos.System.Graphics.Point(panY, panX + panSizeH));
        }
        public static void CompleteUI()
        {
            int ScreenWidth = 800;
            int ScreenHeight = 600;
            int panSizeH = 150;
            int panSizeW = 300;
            int panX = ScreenHeight / 2 - panSizeH / 2;
            int panY = ScreenWidth / 2 - panSizeW / 2;
            //Welcome Screen
            c.Clear(Color.DodgerBlue);
            c.DrawFilledRectangle(new Pen(Color.White), new Cosmos.System.Graphics.Point(panY, panX), panSizeW, panSizeH);
            DrawString("MishaOS Setup", Color.Black, new Cosmos.System.Graphics.Point(panY + 5, panX + 5));

            DrawString("Enter = Finish", Color.White, new Cosmos.System.Graphics.Point(panY, panX + panSizeH));
        }
        public static void DrawString(string str,Color col, Cosmos.System.Graphics.Point p)
        {
            c.DrawString(str,PCScreenFont.Default,new Pen(col),p);
        }
    }
}
