using MishaOS.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MishaOS.TextUI.Commands.All.General
{
    public class cat : ICommand
    {
        public void Execute(IGuiConsole g, string cmdline)
        {
            string newcmdlane = cmdline.Replace("cat", "");
            string[] args = newcmdlane.Split();
            if (args.Length == 1)
            {
                g.WriteLine("Bad Syntax. Use cat /? for help.");
            }
            else if (args.Length == 2)
            {
                if (args[1] == "/?")
                {
                    g.WriteLine("CAT Help:");
                    g.WriteLine("=============");
                    g.WriteLine("Syntax: cat <Filename>");
                }
                else
                {
                    if (!File.Exists(args[1]))
                    {
                        g.WriteLine("Error: File "+args[1]+" does not exist!");
                        return;
                    }
                    try
                    {
                        foreach (var item in File.ReadAllLines(args[1]))
                        {
                            g.WriteLine(item);
                        }
                    }
                    catch(Exception ex)
                    {
                        g.WriteLine("ERROR: "+ex.Message);
                    }
                }
            }
        }

        public void ShowHelp()
        {
            
        }
    }
}
