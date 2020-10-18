using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.Graphics;
using MishaOS.Drivers;
using MishaOS.Gui;
using MishaOS.Gui.Windows;
using MishaOS.TextUI.Commands.All.General;
using MishaOS.TextUI.Commands.Help;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace MishaOS.TextUI.Commands
{
    public static class CommandParaser
    {
        /// <summary>
        /// Process a command
        /// </summary>
        /// <param name="g">The Console.</param>
        /// <param name="cmd">The Command</param>
        public static void ProcessCommand(GuiConsole g,string cmd)
        {
            if (cmd.ToLower().StartsWith("help"))
            {
                new Help.Help().Execute(g,cmd);
            }
            else if (cmd.ToLower().StartsWith("clear"))
            {
                g.Clear();
            }
            else if (cmd.ToLower().StartsWith("about"))
            {
                g.WriteLine(Kernel.KernelVersion);
            }
            else if (cmd.ToLower().StartsWith("ls"))
            {
                new ls().Execute(g,cmd);
            }
            else if (cmd.ToLower().StartsWith("cd"))
            {
                string NewName = cmd.Replace("cd ","");
                var fs = g.term.CurrentDir;
                if (Directory.Exists(fs + @"\"+ NewName))
                {
                    g.term.CurrentDir = fs + NewName+@"\";
                }
                else if (NewName == "..")
                {
                    g.term.CurrentDir = Directory.GetParent(fs).FullName;
                }
                else if (NewName == "/" | NewName == @"\")
                {
                    g.term.CurrentDir = Directory.GetDirectoryRoot(fs);
                }
                else
                {
                    g.WriteLine("ERROR: Dirrectory "+ fs + @"\" + NewName+" Does not exist.");
                }
            }
            else if (cmd.ToLower().StartsWith("setup"))
            {
                Display.Disable();
                
                new Setup(Kernel.FS).StartSetup();
            }
            else if (cmd.ToLower().StartsWith("shutdown"))
            {
                Cosmos.System.Power.Shutdown();
            }
            else if (cmd.ToLower().StartsWith("reboot"))
            {
                Cosmos.System.Power.Reboot();
            }
            else if (cmd.ToLower().StartsWith("mkdir"))
            {
                new mkdir().Execute(g,cmd);
            }
            else if (cmd.ToLower().StartsWith("cat"))
            {
                new cat().Execute(g, cmd);
            }
            else if (cmd.ToLower().StartsWith("beep"))
            {
                System.Console.Beep();
            }
            else if (cmd.ToLower().StartsWith("edit"))
            {
                new edit().Execute(g,cmd);
            }
            else if (cmd.ToLower().StartsWith("exit"))
            {
                DesktopManager.CloseWindow(g.term);
                DesktopManager.OpenWindow(new Desktop());
            }
            else if (string.IsNullOrEmpty(cmd))
            {

            }
            else
            {
                g.WriteLine($"\"{cmd}\" is not recognized as an internal or external command,operable program.");
            }
        }
    }
}
