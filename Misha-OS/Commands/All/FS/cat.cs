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
            if (args.Length == 0)
            {
                g.WriteLine("Bad Syntax. Use cat /? for help.");
            }
            else if (args.Length == 1)
            {
                if (args[0] == "/?")
                {
                    g.WriteLine("CAT Help:");
                    g.WriteLine("=============");
                    g.WriteLine("Syntax: cat <Filename>");
                }
                else
                {
                    try
                    {
                        foreach (var item in File.ReadAllLines(args[0]))
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
