using MishaOS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace MishaOS.TextUI.Commands
{
    interface ICommand
    {
        void ShowHelp();
        void Execute(IGuiConsole g, string cmdline);
    }
}
