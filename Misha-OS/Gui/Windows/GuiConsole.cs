using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS
{
    /// <summary>
    /// A wraper class to Terminal.
    /// </summary>
    public class GuiConsole
    {
        public List<string> Lines = new List<string>();
        public Terminal term;
        private int MaxCols;
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
                return Lines[0];
            }
            set
            {
                int oldX = term.StringindexX;
                int oldY = term.StringindexY;
                term.StringindexX = 0;
                term.StringindexY = 0;
                Write(value,term.TitlebarColor, Color.White);
                term.StringindexX = oldX;
                term.StringindexY = oldY;
            }
        }

        public Color Forecolor = Color.White;
        public Color BackColor = Color.Black;
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
                term.Drawstring(x.ToString(), new Cosmos.System.Graphics.Pen(Forecolor), new Cosmos.System.Graphics.Pen(BackColor), false, true);
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
            term.Drawstring(txt, new Cosmos.System.Graphics.Pen(Forecolor), new Cosmos.System.Graphics.Pen(BackColor));

            Scoll();
        }
        /// <summary>
        /// Writes text to the console and creates a new line.
        /// </summary>
        public void WriteLine()
        {
            Lines.Add("");
            term.Drawstring("", new Cosmos.System.Graphics.Pen(Forecolor), new Cosmos.System.Graphics.Pen(BackColor));

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
            term.ReDraw();
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
        /// For example, if the user types in "A cow.", then this function will return "A Cow."
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
