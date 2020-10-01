using Cosmos.Core;
using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Drivers;
using MishaOS.Gui;
using MishaOS.Gui.Windows;
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
        public bool Enabled = true;
        public Cosmos.System.Graphics.Point windowP;
        public GuiConsole console;
        public int width = 800;
        public int height = 600;
        public string CurrentDir = @"0:\";
        public int MaxCols = 0;
        public string typingcommand = "";
        public Cosmos.System.Graphics.Point WindowCLoseP;
        public int StringindexX = 0;
        public int StringindexY = 20;
        public Terminal()
        {
            this.Text = "Terminal";
            windowP = new Cosmos.System.Graphics.Point(0, 0);
            WindowCLoseP = new Cosmos.System.Graphics.Point(width - 20, 0);
            //Get the max cols
            for (int i = 0; i < height; i++)
            {
                Drawstring(i.ToString(), new Pen(Color.Black), new Pen(Color.DodgerBlue),true,true,false);
                if (StringindexY >= height)
                {
                    MaxCols = i;
                    //Init the console
                    console = new GuiConsole(this, MaxCols);
                    return;
                }
            }
        }
        /// <summary>
        /// Redraws the terminal.
        /// </summary>
        public void ReDraw()
        {
            
            Cosmos.System.Graphics.Point titleP = new Cosmos.System.Graphics.Point(windowP.X, windowP.Y + 20);
            int WindowTittleBarHeight = 20;
            Display.disp.DrawFilledRectangle(new Pen(TitlebarColor), new Cosmos.System.Graphics.Point(), width, WindowTittleBarHeight);//Top Bar
            Display.disp.DrawFilledRectangle(new Pen(Color.Black), titleP, width, height);//Contents


            //Draw Close Button
            Display.disp.DrawFilledRectangle(new Pen(Color.Red), WindowCLoseP, 25, WindowTittleBarHeight);
            Display.disp.DrawString("X", PCScreenFont.Default, new Pen(Color.White), new Cosmos.System.Graphics.Point(width - 20, 0));
            //End

            console.Title = "Misha OS Terminal - Active"; //Set title 
            StringindexX = 0;
            StringindexY = 20;
        }
        public override void Open()
        {
            base.Open();
            this.Enabled = true;


            ReDraw();
            //Write Terminal info
            console.WriteLine("Misha OS Terminal");
            console.WriteLine("Version: "+Kernel.KernelVersion);
            console.WriteLine("Running on a " + CPU.GetCPUVendorName());
            console.Write(CurrentDir + " ");
        }
        public override void Close()
        {
            base.Close();
            Enabled = false; 
        }
        public override void Update()
        {
            base.Update();
            try
            {
                if (!Enabled)
                    return;
                //Check if close button is clicked
                if (IsOpen)
                {
                    if (MouseManager.MouseState == MouseState.Left)
                    {
                        //20 is text size.
                        if (UiMouse.MouseY >= WindowCLoseP.Y && UiMouse.MouseY <= WindowCLoseP.Y + 20)
                        {
                            this.Close();
                            DesktopManager.CloseWindow(this);
                            DesktopManager.OpenWindow(new Desktop());
                        }
                    }
                }

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
                    console.Write(CurrentDir + " ");
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
        /// <param name="a">Add right/left peadding</param>
        public void Drawstring(string thestring, Pen forecolor, Pen backcolor, bool newline = true, bool a = true,bool doDraw=true)
        {
            if (!Enabled)
                return;
            //Set Char X back to 0.
            if (newline)
                StringindexX = 0;
            //Clear the area of the text first, than draw the string
            if (newline)
                Display.DrawRectangle(StringindexX, StringindexY, windowP.X, 15, backcolor.Color);
            else
                Display.DrawRectangle(StringindexX, StringindexY, 10, 15, backcolor.Color);
            //Now draw the text
            if (doDraw)
                Display.disp.DrawString(thestring, PCScreenFont.Default, forecolor, new Cosmos.System.Graphics.Point(StringindexX, StringindexY));
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
