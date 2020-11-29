using MishaOS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace MishaOS.TextUI.Commands.Help
{
    public class Help : ICommand
    {
        public void Execute(IGuiConsole g, string cmdline)
        {
            g.WriteLine("Help Version 0.1 Beta");
            g.WriteLine("=====================");
            g.WriteLine("---General Commands--");
            g.WriteLine("help - Show Help");
            g.WriteLine("about - Kernel Info");
            g.WriteLine("clear - Clear Screen");
            g.WriteLine("setup - Open Setup");
            g.WriteLine("shutdown - Shutdown");
            g.WriteLine("reboot - Reboots");
            g.WriteLine("cat - Shows File Contents");
            g.WriteLine("beep - Beeps");
            g.WriteLine("exit - Exit Terminal");
            g.WriteLine();
            g.WriteLine("-FileSystem Commands-");
            g.WriteLine("cd - Change Dir");
            g.WriteLine("ls - List Files and Dirs");
            g.WriteLine("mkdir - Creates Dir");

        }

        public void ShowHelp()
        {
            return;
        }
    }
}
