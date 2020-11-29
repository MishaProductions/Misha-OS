using Cosmos.Core;
using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Commands;
using MishaOS.Drivers;
using MishaOS.Gui;
using MishaOS.Gui.Windows;
using MishaOS.Gui.Windows.Controls;
using MishaOS.TextUI.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS
{
    /// <summary>
    /// The terminal App.
    /// </summary>
    public class Terminal : Window
    {
        public Cosmos.System.Graphics.Point windowP;
        public IGuiConsole console;
        public int MaxCols = 0;
        public string typingcommand = "";

        public int StringindexX = 0;
        public int StringindexY = 20;
        public Terminal()
        {
            this.Size = new Size(Display.ScreenWidth, Display.ScreenHeight);
            this.Text = "Terminal"; //Set title 
            this.BackgroundColor = Color.Black;
            this.ForeColor = Color.White;
            //Get the max cols
            for (int i = 0; i < this.Size.Height; i++)
            {
                Drawstring(i.ToString(), new Pen(Color.Black), new Pen(Color.DodgerBlue),true,true,false);
                if (StringindexY >= this.Size.Height)
                {
                    MaxCols = i;
                    //Init the console
                    console = new GuiConsole(this, MaxCols) { _term = this };
                    
                    return;
                }
            }
        }
        public override void Open()
        {
            base.Open();
            StringindexX = 0;
            StringindexY = 20;

            //Write Terminal info
            console.WriteLine("Misha OS Terminal");
            console.WriteLine("Version: "+Kernel.KernelVersion);
            console.WriteLine("Running on a " + CPU.GetCPUVendorName());
            console.Write(console.CurrentDIR + " ");
        }
        public override void Update()
        {
            base.Update();
            try
            {
                if (!Enabled)
                    return;

                KeyEvent k;
                bool IsKeyPressed = KeyboardManager.TryReadKey(out k);
                if (!IsKeyPressed) //if the user did not press a key return 
                    return;
                //Check the keys and peform action.
                if (k.Key == ConsoleKeyEx.Enter)
                {
                    string Command = typingcommand;
                    typingcommand = "";
                    console.WriteLine();//Move down the cursor.
                    CommandParaser.ProcessCommand(console, Command); //Process the command
                    console.Write(console.CurrentDIR + " ");
                }
                else if (k.Key == ConsoleKeyEx.Spacebar)
                {
                    typingcommand += " ";
                    console.Write(" ");
                }
                else if (k.Key == ConsoleKeyEx.Backspace)
                {
                    if (StringindexX != 0)
                    {
                        StringindexX -= 10;
                        typingcommand = typingcommand.Remove(typingcommand.Length - 1, 1);
                        console.Write(" ");
                        StringindexX -= 10;
                    }
                }
                else
                {
                    typingcommand += k.KeyChar;
                    console.Write(k.KeyChar.ToString());
                }
            }
            catch { }
        }
        /// <summary>
        /// Draws a string.
        /// For internal use only.
        /// </summary>
        /// <param name="thestring">The String</param>
        /// <param name="black">Black or White?</param>
        /// <param name="newline">Add a new line?</param>
        /// <param name="a">Add right/left padding</param>
        public void Drawstring(string thestring, Pen forecolor, Pen backcolor, bool newline = true, bool a = true,bool doDraw=true)
        {
            if (!Enabled)
                return;
            //Set Char X back to 0.
            if (newline)
                StringindexX = 0;
            //Clear the area of the text first, than draw the string
            if (newline)
            {
                Control c = new Control() { Location = new System.Drawing.Point(StringindexX, StringindexY), Size = new Size(windowP.X, 15), BackgroundColor = backcolor.Color };
                this.Controls.Add(c);
            }
            else
            {
                Control c = new Control() { Location = new System.Drawing.Point(StringindexX, StringindexY), Size = new Size(10, 15), BackgroundColor = backcolor.Color };
                this.Controls.Add(c);
            }
            //Now draw the text
            if (doDraw)
            {
                Label lbl = new Label();
                lbl.Location = new System.Drawing.Point(StringindexX, StringindexY);
                lbl.Text = thestring;
                lbl.ForeColor = forecolor.Color;
                this.Controls.Add(lbl);
            }
            if (newline)
            {
                StringindexY += 20;
            }
            else
            {
                if (a)
                    StringindexX += 10;
            }
        }
    }
}
