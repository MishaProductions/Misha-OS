using System;
using System.Collections.Generic;
using System.Text;

namespace MishaOS.TextUI.Commands
{
    interface ICommand
    {
        void ShowHelp();
        void Execute(GuiConsole g, string cmdline);
    }
}
