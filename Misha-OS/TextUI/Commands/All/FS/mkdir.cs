using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MishaOS.TextUI.Commands.All.General
{
    public class mkdir : ICommand
    {
        public void Execute(GuiConsole g, string cmdline)
        {
            string newdirname = cmdline.Replace("mkdir ", "");
            string[] paramss = newdirname.Split();
           // Arguments CommandLine = new Arguments(paramss);
            bool quiet = false;
            if(paramss.Length == 0) { g.WriteLine("The syntax of the command is incorrect. Use /? for help."); return; }
            if (paramss[0] == "/?")
            {
                g.WriteLine("mkdir help");
                g.WriteLine("Syntax: mkdir <options> <FolderName>");
                g.WriteLine("====================");
                g.WriteLine("Options:");
                g.WriteLine("-q     Disable Output");
                g.WriteLine("====================");
                g.WriteLine("Example: mkdir -q lalala");
                g.WriteLine("Example: mkdir lalala");
                g.WriteLine("These examples will create a dirrectory names lalala.");
                return;
            }
            else if (paramss[0] == "-q")
            {
                quiet = true;
            }

            if (!string.IsNullOrWhiteSpace(paramss[paramss.Length - 1]) && paramss[paramss.Length - 1] != "mkdir")
            {
                string dirName = paramss[paramss.Length - 1];
                if (quiet)
                {
                    try
                    {
                        Directory.CreateDirectory(g.term.CurrentDir + @"\" + dirName);
                    }
                    catch { }
                }
                else
                {
                    bool sucess = true;
                    try
                    {
                        Directory.CreateDirectory(g.term.CurrentDir + @"\" + dirName);
                    }
                    catch (Exception ex)
                    {
                        sucess = false;
                        g.WriteLine("Error: "+ex.Message);
                    }
                    if (sucess)
                        g.WriteLine("Created Dirrectory: "+ dirName);
                }
            }
            else
            {
                g.WriteLine("The syntax of the command is incorrect. Use /? for help.");
            }
            

            //if (paramss.Length == 0)
            //{
            //    g.WriteLine("The syntax of the command is incorrect. Use mkdir /? for help");
            //    return;
            //}
            //else if (paramss.Length == 1)
            //{
            //    g.WriteLine("Comming Soon: dir name is: " + newdirname);
            //    //return;
            //}
            //else if (paramss.Length == 2)
            //{
            //    g.WriteLine("<QUIET MODE> Comming Soon: dir name is: " + newdirname);
            //   // return;
            //}

            //if (paramss[0] == "/?")
            //{
                
            //}
            //else
            //{
            //    g.WriteLine("The syntax of the command is incorrect. Use mkdir /? for help");
            //    return;
            //}
        }

        public void ShowHelp()
        {
            throw new NotImplementedException();
        }
    }
}
