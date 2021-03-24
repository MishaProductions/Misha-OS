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
    public class Window : IDisposable
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
                this.DrawAll();
            }
        }

        private bool _ShouldDrawTitleBar = true;
        private bool _Enabled = true;
        public bool ShouldDrawTitleBar
        {
            get
            {
                return _ShouldDrawTitleBar;
            }
            set
            {
                _ShouldDrawTitleBar = value;
                this.DrawAll();
            }
        }
        public int State;
        /// <summary>
        /// The window title
        /// </summary>
        public string Text { get; set; } = "MishaOS Window";
        /// <summary>
        /// The titlebar color
        /// </summary>
        public Color TitlebarColor = Color.ForestGreen;

        public Color BackgroundColor { get; set; }
        public Color ForeColor { get; set; }

        private bool _IsOpen = false;
        /// <summary>
        /// Checks if window is open.
        /// </summary>
        public bool IsOpen { get { return _IsOpen; } }

        private System.Drawing.Point loc = new System.Drawing.Point(0, 0);
        public System.Drawing.Point Location { get { return loc; } set { loc = value; } }
        /// <summary>
        /// Gets the location where the user can draw to. This will be (0,TitlebarHeight) + WindowLocation if title bar disabled. If the title bar is enabled, this will be (0,0) + Window Location
        /// </summary>
        public System.Drawing.Point ClientLocation
        {
            get
            {
                if (ShouldDrawTitleBar)
                {
                    return new System.Drawing.Point(0 + this.Location.X, TitlebarHeight + this.Location.Y);
                }
                else
                {
                    return new System.Drawing.Point(0 + this.Location.X, 0 + this.Location.Y);
                }
            }
        }
        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                _Enabled = value;

                foreach (Control c in this.Controls)
                {
                    c.Enabled = value;
                }
            }
        }

        public Size Size
        {
            get; set;
        }

        public List<Control> Controls = new List<Control>();
        public Window()
        {
            Size = new Size(220, 200);
        }
        /// <summary>
        /// Opens the window.
        /// </summary>
        public virtual void Open()
        {
            _IsOpen = true;
            Enabled = true;

            this.DrawAll();
        }
        int CloseWidth = 10;
        int CloseHeight = TitlebarHeight;

        public static int TitlebarHeight = 10;
        /// <summary>
        /// Draws everything on the window: (titlebar, all controls, etc).
        /// </summary>
        public void DrawAll()
        {
            if (_IsOpen == false)
                return;
            //Draw the window background.
            Display.DrawRectangle(Location.X, Location.Y, Size.Width, Size.Height, BackgroundColor);

            //Draw all the controls in this control
            foreach (Control d in Controls)
            {
                if (d.ParrentWindow == null)
                    d._ParrentWindow = this;
                d.DrawAll();
            }
            //Draw title bar here
            if (_ShouldDrawTitleBar)
            {
                Display.DrawRectangle(this.Location.X, this.Location.Y, this.Size.Width, TitlebarHeight, this.TitlebarColor);
                Display.DrawRectangle(this.Location.X + 2, this.Location.Y, this.Size.Width - 2, TitlebarHeight, Color.Green);
                Display.DrawString(Text, Pens.White, this.Location.X, this.Location.Y);

                //Draw Close button
                if (ShouldDrawCloseButton)
                {
                    Display.DrawRectangle(
                        this.Location.X + this.Size.Width - CloseWidth,
                        this.Location.Y,
                        CloseWidth,
                        CloseHeight,
                        Color.Red);

                    Display.DrawString("X", Pens.White, this.Location.X + this.Size.Width - CloseWidth + 2, this.Location.Y);
                }
            }
            //Call custom draw function
#pragma warning disable CS0618
            Draw();
#pragma warning restore CS0618
        }
        [Obsolete("Please call DrawAll() insted.")]
        /// <summary>
        /// Custom draw method. Do not call dirrectly.
        /// </summary>
        public virtual void Draw()
        {

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
                if (MouseManager.MouseState == MouseState.Left && this.IsOpen && this.Enabled)
                {
                    //Check if Close button is clicked
                    if (UiMouse.MouseY >= this.Location.Y && UiMouse.MouseY <= this.Location.Y + TitlebarHeight)
                    {
                        if (UiMouse.MouseX >= (this.Location.X + this.Size.Width - CloseWidth) && UiMouse.MouseX <= (this.Location.X + this.Size.Width - CloseWidth) + CloseWidth)
                        {
                            DesktopManager.CloseWindow(this);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Main window update function.
        /// Called every ms.
        /// </summary>
        public virtual void Update()
        {
            //Update all controls
            foreach (Control d in Controls)
            {
                if (d.ParrentWindow == null)
                    d._ParrentWindow = this;
                d.Update();
            }
        }

        public void Dispose()
        {
            this.Close();
            this.Controls = null;
        }
    }
}
