using Cosmos.Core;
using Cosmos.Debug.Kernel;
using Cosmos.HAL;
using System;

namespace MishaOS.Drivers
{
    public static class BootMessages
    {

        public static void Print(SystemdPrintType type, string name)
        {
            ConsoleColor old = Console.ForegroundColor;
            if (type == SystemdPrintType.Fail)
            {
                Console.Write(" [");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("FAIL");
                Console.ForegroundColor = old;
                Console.Write(" ] " + name);
                Console.WriteLine();

            }
            else
            {
                Console.Write("[ ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("OK");
                Console.ForegroundColor = old;
                Console.Write(" ] " + name);
                Console.WriteLine();
            }
        }
    }

    public enum SystemdPrintType { Ok, Fail }
}
