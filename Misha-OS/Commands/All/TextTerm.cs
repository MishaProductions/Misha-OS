using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Commands.All
{
    class TextTerm : IGuiConsole
    {
        public object term { get { return this; } set { TextTerm i = this; i = (TextTerm)value; } }
        public string CurrentDIR { get; set; } = @"0:\";
        public int MaxCols { get { return Console.BufferHeight; } set { } }
        public string Title { get; set; }
        public Color foreColor {
            get
            {
                return FromColor(Console.ForegroundColor);
            }
            set
            {
                Console.ForegroundColor = FromColor(value);
            }
        }
        public Color backColor {
            get
            {
                return FromColor(Console.BackgroundColor);
            }
            set
            {
                Console.BackgroundColor = FromColor(value);
            }
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void Pause()
        {
            Console.WriteLine("Press any key to contiune.");
            Console.ReadKey();
        }
        public static System.Drawing.Color FromColor(System.ConsoleColor c)
        {
            int[] cColors = {   0x000000, //Black = 0
                        0x000080, //DarkBlue = 1
                        0x008000, //DarkGreen = 2
                        0x008080, //DarkCyan = 3
                        0x800000, //DarkRed = 4
                        0x800080, //DarkMagenta = 5
                        0x808000, //DarkYellow = 6
                        0xC0C0C0, //Gray = 7
                        0x808080, //DarkGray = 8
                        0x0000FF, //Blue = 9
                        0x00FF00, //Green = 10
                        0x00FFFF, //Cyan = 11
                        0xFF0000, //Red = 12
                        0xFF00FF, //Magenta = 13
                        0xFFFF00, //Yellow = 14
                        0xFFFFFF  //White = 15
                    };
            return Color.FromArgb(cColors[(int)c]);
        }

        public static System.ConsoleColor FromColor(System.Drawing.Color c)
        {
            int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0; // Bright bit
            index |= (c.R > 64) ? 4 : 0; // Red bit
            index |= (c.G > 64) ? 2 : 0; // Green bit
            index |= (c.B > 64) ? 1 : 0; // Blue bit
            return (System.ConsoleColor)index;
        }

        public char ReadKey()
        {
            return Console.ReadKey().KeyChar;
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}
