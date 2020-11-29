using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Drivers;
using MishaOS.Gui.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Gui.Windows
{
    /// <summary>
    /// A class for displaying windows.
    /// </summary>
    public class Window : Control
    {
        private bool _ShouldDrawCloseButton = true;
        public bool ShouldDrawCloseButton
        {
            get
            {
                return _ShouldDrawCloseButton;
            }
            set
            {
                _ShouldDrawCloseButton = value;
                this.Draw();
            }
        }
        public int State;
        public string Text { get; set; } = "Window1";
        public Color TitlebarColor = Color.ForestGreen;
        private bool _IsOpen = false;
        private System.Drawing.Point loc = new System.Drawing.Point(0, 20);
        int tmp_CtlCount = 0;
        //20 is titlebar height.
        public new System.Drawing.Point Location { get { return loc; } set { loc = new System.Drawing.Point(value.X, value.Y + 20); } }
        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                foreach (Control c in this.Controls)
                {
                    c.Enabled = value;
                }
            }
        }

        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }
        public Window()
        {
            Size = new Size(500, 200);
        }
        /// <summary>
        /// Checks if window is open.
        /// </summary>
        public bool IsOpen { get { return _IsOpen; } }
        /// <summary>
        /// Opens the window.
        /// </summary>
        public virtual void Open()
        {
            _IsOpen = true;
            this.Draw();
        }
        int CloseWidth = 20;
        int CloseHeight = 20;
        public override void Draw()
        {
            if (_IsOpen == false)
                return;
            //Draw the default square
            if (DrawDefaultSquare)
                Display.DrawRectangle(Location.X, Location.Y, Size.Width, Size.Height, BackgroundColor);

            //Draw all the controls in this control
            foreach (Control d in Controls)
            {
                d._ParrentWindow = this;
                d.Draw();
            }
            tmp_CtlCount = Controls.Count;
            //Draw title bar here
            Display.DrawRectangle(this.Location.X, this.Location.Y - 20, this.Size.Width, 20, this.TitlebarColor);
            Display.DrawString(Text, new Pen(Color.White), this.Location.X, this.Location.Y - 20);
            //Draw Close button
            if (ShouldDrawCloseButton)
            {
                Display.DrawRectangle(this.Location.X + this.Size.Width - CloseWidth, this.Location.Y - 20, CloseWidth, CloseHeight, Color.Red);
                Display.DrawString("X", new Pen(Color.White), this.Location.X + this.Size.Width - CloseWidth, this.Location.Y - 20);
            }
        }
        /// <summary>
        /// Closes the window.
        /// </summary>
        public virtual void Close()
        {
            _IsOpen = false;
            Enabled = false;
        }

        public void UpdateAll()
        {
            if (_IsOpen)
            {
                Update();
                if (tmp_CtlCount != Controls.Count)
                {
                    Draw();
                }
                if (MouseManager.MouseState == MouseState.Left && this.IsOpen && this.Enabled)
                {
                    //Check if Close button is clicked
                    if (UiMouse.MouseY >= (this.Location.Y - 20) && UiMouse.MouseY <= (this.Location.Y - 20) + 20)
                    {
                        if (UiMouse.MouseX >= (this.Location.X + this.Size.Width - CloseWidth) && UiMouse.MouseX <= (this.Location.X + this.Size.Width - CloseWidth) + CloseWidth)
                        {
                            DesktopManager.CloseWindow(this);
                            DesktopManager.OpenWindow(new Desktop());
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Main window update function.
        /// You can use this function for checking mouse clicks.
        /// </summary>
        public override void Update()
        {
            base.Update();
        }
    }
}
