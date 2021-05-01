using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MishaOS.Commands
{
    public static class TextWindows
    {
        public static void Draw(string text, int x, int y)
        {
            var ofg = System.Console.ForegroundColor;
            var ogc = System.Console.BackgroundColor;
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.Blue;
            var line0 = string.Empty;
            var line1 = string.Empty;
            var line2 = string.Empty;
            var line3 = string.Empty;
            var line4 = string.Empty;
            var line5 = string.Empty;

            line0 += "+";
            for (int i = 0; i < text.Length; i++)
            {
                line0 += "=";
            }
            line0 += "+";

            line1 += "|";
            line1 += text;
            line1 += "|";

            line2 += "|";
            for (int i = 0; i < text.Length; i++)
            {
                line2 += " ";
            }
            line2 += "|";

            line3 += "|";
            for (int i = 0; i < text.Length; i++)
            {
                line3 += "=";
            }
            line3 += "|";

            line4 += "|";
            for (int i = 0; i < text.Length; i++)
            {
                line4 += " ";
            }
            line4 += "|";

            line5 += "+";
            for (int i = 0; i < text.Length; i++)
            {
                line5 += "=";
            }
            line5 += "+";
            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.WriteLine(line0);
            Console.WriteLine(line1);
            Console.WriteLine(line2);
            Console.WriteLine(line3);
            Console.WriteLine(line4);
            Console.WriteLine(line5);

            Console.CursorTop = y + 4;
            Console.CursorLeft = x + 1;
            Console.ForegroundColor = ofg;
            Console.BackgroundColor = ogc;
        }
    }
}
