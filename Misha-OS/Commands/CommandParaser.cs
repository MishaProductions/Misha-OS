﻿using Cosmos.HAL;
using Cosmos.System;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.Graphics;
using MishaOS.Commands;
using MishaOS.Commands.All;
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
        public static bool IsGUI = true;
        /// <summary>
        /// Process a command
        /// </summary>
        /// <param name="g">The Console.</param>
        /// <param name="cmd">The Command</param>
        public static void ProcessCommand(IGuiConsole g,string cmd)
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
                var fs = g.CurrentDIR;
                if (Directory.Exists(fs + @"\"+ NewName))
                {
                    g.CurrentDIR = fs + NewName+@"\";
                }
                else if (NewName == "..")
                {
                    g.CurrentDIR = Directory.GetParent(fs).FullName;
                }
                else if (NewName == "/" | NewName == @"\")
                {
                    g.CurrentDIR = Directory.GetDirectoryRoot(fs);
                }
                else
                {
                    g.WriteLine("ERROR: Dirrectory "+ fs + @"\" + NewName+" Does not exist.");
                }
            }
            else if (cmd.ToLower().StartsWith("setup"))
            {
                if (IsGUI)
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
            else if (cmd.ToLower().StartsWith("mode"))
            {
                var args = cmd.ToLower().Split();

                if (args.Length == 1 | args.Length == 0)
                {
                    g.WriteLine("Invaild Usage. Use mode help for help.");
                }
                else if (args.Length == 2)
                {
                    if (args[1] == "text")
                    {
                       if (IsGUI== true)
                        {
                            if (g.term is Window)
                            {
                                DesktopManager.CloseWindow((Window)g.term);
                            }
                            IsGUI = false;
                            //Switch over to text terminal.
                            g = new TextTerm();
                            g.term = new TextTerm();
                            Display.Disable();
                            g.WriteLine("Now in text mode!");
                            g.Write(g.CurrentDIR);
                            while (IsGUI == false)
                            {
                                var input = g.ReadLine();
                                ProcessCommand(g, input);
                                g.Write(g.CurrentDIR);
                            }
                        }
                        else
                        {
                            g.WriteLine("ERROR: Already in text mode");
                        }
                    }
                    else if (args[1] == "gui")
                    {
                        if (!IsGUI)
                        {
                            IsGUI = true;
                            Display.Init();
                            g.term = new GuiConsole(new Terminal(), 80);
                            DesktopManager.OpenWindow(new Desktop());
                        }
                        else
                        {
                            g.WriteLine("ERROR: Already in GUI mode");
                        }
                    }
                    else if (args[1] == "help")
                    {
                        g.WriteLine("Mode command line usage");
                        g.WriteLine("======================");
                        g.WriteLine("mode <DisplayMode>");
                        g.WriteLine("Display Modes: ");
                        g.WriteLine("GUI - Switches over to GUI mode");
                        g.WriteLine("TEXT - Switches over to Text mode");
                    }
                    else
                    {
                        g.WriteLine("Invaild Usage. Use \"mode help\" for help.");
                    }
                }
                g.WriteLine("[DEBUG] args length: "+args.Length);
                foreach (var item in args)
                    g.WriteLine(item);

            }
            else if (cmd.ToLower().StartsWith("exit"))
            {
                if (IsGUI)
                {
                    if (g.term is Window)
                    {
                        DesktopManager.CloseWindow((Window)g.term);
                    }
                    DesktopManager.OpenWindow(new Desktop());
                }
                else
                {
                    g.WriteLine("Exit cannot be used on a text session. Please use gui mode to use exit command.");
                }

            }
            else if (string.IsNullOrEmpty(cmd))
            {

            }
            else
            {
                g.WriteLine($"\"{cmd}\" is not recognized as an internal command,operable program.");
            }
        }
    }
}