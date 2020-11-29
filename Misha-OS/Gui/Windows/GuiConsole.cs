using Cosmos.System;
using MishaOS.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS
{
    /// <summary>
    /// A wraper class to Terminal.
    /// </summary>
    public class GuiConsole:IGuiConsole
    {
        public List<string> Lines = new List<string>();
        public Terminal term;
        public int MaxCols { get; set; }
        public GuiConsole(Terminal term, int MaxCols)
        {
            this.term = term;
            this.MaxCols = MaxCols;
        }
        /// <summary>
        /// Gets or sets the title of the console
        /// </summary>
        public string Title
        {
            get
            {
                return term.Text;
            }
            set
            {
                term.Text = value;
            }
        }

        public Color foreColor { get; set; } = Color.White;
        public Color backColor { get; set; } = Color.Black;

        public Terminal _term;
        object IGuiConsole.term { get => _term; set => _term = (Terminal)value; }
        public string CurrentDIR { get; set; } = @"0:\";

        /// <summary>
        /// Writes text to the console.
        /// </summary>
        /// <param name="txt">The Text</param>
        /// <param name="InvertColor">Invert Color?</param>
        public void Write(string txt)
        {
            try
            {
                Lines[Lines.Count - 1] = Lines[Lines.Count] + txt;

            }
            catch { }
            foreach (char x in txt)
            {
                term.Drawstring(x.ToString(), new Cosmos.System.Graphics.Pen(foreColor), new Cosmos.System.Graphics.Pen(backColor), false, true);
            }
            Scoll();
        }
        private void Write(string txt,Color BackCcolor,Color ForeColor)
        {
            try
            {
                Lines[Lines.Count - 1] = Lines[Lines.Count] + txt;

            }
            catch { }
            foreach (char x in txt)
            {
                term.Drawstring(x.ToString(), new Cosmos.System.Graphics.Pen(ForeColor), new Cosmos.System.Graphics.Pen(BackCcolor), false, true);
            }
            Scoll();
        }
        /// <summary>
        /// Writes text to the console and creates a new line.
        /// </summary>
        /// <param name="txt"></param>
        public void WriteLine(string txt)
        {
            Lines.Add(txt);
            term.Drawstring(txt, new Cosmos.System.Graphics.Pen(foreColor), new Cosmos.System.Graphics.Pen(backColor));

            Scoll();
        }
        /// <summary>
        /// Writes text to the console and creates a new line.
        /// </summary>
        public void WriteLine()
        {
            Lines.Add("");
            term.Drawstring("", new Cosmos.System.Graphics.Pen(foreColor), new Cosmos.System.Graphics.Pen(backColor));

            Scoll();
        }
        /// <summary>
        /// Clears the console using a very hacky method
        /// </summary>
        public void Clear()
        {
            term.StringindexX = 0;
            term.StringindexY = 0;
            Lines.Clear(); //Clear lines first to prevent loop
        }
        /// <summary>
        /// Scolls the screen. This method temporly clears the screen.
        /// </summary>
        private void Scoll()
        {
            if (Lines.Count == MaxCols)
            {
                Clear();
                return;
            }
        }
        /// <summary>
        /// Shows a message and waits for the user to press any key.
        /// </summary>
        public void Pause()
        {
            WriteLine("Press any key to contine....");
            while (KeyboardManager.ReadKey().KeyChar.ToString() == null) { }
        }
        /// <summary>
        /// Reads a key
        /// For example, if the user presses M then this function will return M.
        /// </summary>
        /// <returns>A key</returns>
        public char ReadKey()
        {
            while (true)
            {
                string r = KeyboardManager.ReadKey().KeyChar.ToString();
                if (r == null) { }
                else
                {
                    return r[0];
                }
            }
        }
        /// <summary>
        /// Reads a line
        /// For example, if the user types in "A cow.", then this function will return "A cow."
        /// Do not use this function in the terminal's update function.
        /// </summary>
        /// <returns>The full string</returns>
        public string ReadLine()
        {
            string totalstr = "";
            while (true)
            {
                KeyEvent k = KeyboardManager.ReadKey();
                string r = k.KeyChar.ToString();
                if (r == null) { }
                else if (k.Key == ConsoleKeyEx.Enter)
                {
                    return totalstr;
                }
                else
                {
                    term.Update();
                    totalstr += r;
                }
            }
        }
    }
}
