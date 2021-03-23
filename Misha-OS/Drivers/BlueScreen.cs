using MishaOS.TextUI.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace MishaOS.Drivers
{
    public static class BlueScreen
    {
        /// <summary>
        /// Displays a blue screen and stops the OS.
        /// </summary>
        /// <param name="Error"></param>
        public static void Panic(string Error)
        {
            Display.Disable();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
            Console.Clear();
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            Console.WriteLine("A problem has been detected and MishaOS has been shut down to prevent damage to your computer.\n");

            Console.WriteLine(Error);
            Console.WriteLine(@"
If this is the first time you've seen this stop error screen, 
restart you computer. If this screen appears again follow
these steps:
Check to make sure any new hardware is properly installed.
If this is a new installation, check your hardware to see if it is 
compatible with your computer's BIOS.
If problems continue, disable or remove any newly installed hardware. 
Disable BIOS memory options such as caching or shadowing.");
            CommandParaser.IsGUI = false;
            while (true); //lock up
        }
        /// <summary>
        /// Makes string look uglier
        /// </summary>
        /// <param name="Message">The string. Example: Fatal error</param>
        /// <returns>The uglyer string. Example: FATAL_ERROR</returns>
        public static string GetProperMessage(string Message) { return Message.ToUpper().Replace(" ", "_"); }
    }
}
