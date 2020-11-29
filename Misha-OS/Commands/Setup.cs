using Cosmos.HAL;
using Cosmos.System.FileSystem;
using MishaOS.Drivers;
using MishaOS.Gui;
using MishaOS.Gui.Windows;
using MishaOS.TextUI.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace MishaOS
{
    public class Setup
    {
        private CosmosVFS fs;
        public Setup(CosmosVFS fs)
        {
            this.fs = fs;
        }

        public void StartSetup()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("┌──────────────────────────────────┐");
            Console.WriteLine("|          Setup                   |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|       Press q for GUI Setup      |");
            Console.WriteLine("|  Otherwise press anything else   |");
            Console.WriteLine("|        for CLI Setup.            |");
            Console.WriteLine("└──────────────────────────────────┘");
            string o = Console.ReadLine();
            if (o == "Q" | o=="q")
            {
                CommandParaser.IsGUI = true;

                Display.Init();
                UiMouse.Init();
                DesktopManager.OpenWindow(new SetupWindow());
                Display.Render();
                return;
            }




            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("┌──────────────────────────────────┐");
            Console.WriteLine("|          Setup                   |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|            ENTER = Begin         |");
            Console.WriteLine("└──────────────────────────────────┘");
            Console.ReadLine();
            FormatScreen(0);
            fs.Format("0","FAT32",true);
            FormatScreen(100);
            CreateScreen(0);
            fs.CreateFile(@"0:\installed.bif");
            fs.SetFileSystemLabel("0","MishaOS");
            CreateScreen(50);
            fs.CreateDirectory(@"0:\System");
            CreateScreen(100);
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("┌──────────────────────────────────┐");
            Console.WriteLine("|          Setup                   |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|            ENTER = Finish        |");
            Console.WriteLine("└──────────────────────────────────┘");
            Console.ReadLine();
            Cosmos.System.Power.Reboot();
        }

        public void FormatScreen(int progress)
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────┐");
            Console.WriteLine("|          Setup                   |");
            Console.WriteLine("|   This might take a few minutes  |");
            Console.WriteLine("|Depending on the size of the disk.|");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|     Setup is Formating - "+FormatPerCent(progress) + "  %|");
            Console.WriteLine("└──────────────────────────────────┘");
        }

        public void CreateScreen(int progress)
        {
            Console.Clear();
            Console.WriteLine("┌──────────────────────────────────┐");
            Console.WriteLine("|          Setup                   |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|                                  |");
            Console.WriteLine("|     Setup is Copying - " + FormatPerCent(progress) + "  %|");
            Console.WriteLine("└──────────────────────────────────┘");
        }
        /// <summary>
        /// Formats a int to a percent that is more human readable.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string FormatPerCent(int i)
        {
            if (i >= 10)
            {
                return i.ToString()+" %";
            }
            else
            {
                return "0" + i.ToString() + " %"; ;
            }
        }
    }
}
