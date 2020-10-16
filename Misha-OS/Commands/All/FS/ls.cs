using Cosmos.System.FileSystem.Listing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MishaOS.TextUI.Commands.All.General
{
    public class ls : ICommand
    {
        public void Execute(GuiConsole g, string cmdline)
        {
            var fs = g.term.CurrentDir;
            g.WriteLine("Volume Label is " + Kernel.FS.GetFileSystemLabel(Path.GetPathRoot(fs)));
            g.WriteLine("Directory of " + fs);
            g.WriteLine();
            foreach (DirectoryEntry r in Kernel.FS.GetDirectoryListing(fs))
            {
                g.WriteLine(r.mName);
            }
        }

        public void ShowHelp()
        {
            
        }
    }
}
