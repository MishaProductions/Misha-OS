using Cosmos.System;
using MishaOS.Drivers;
using MishaOS.Gui.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MishaOS.Gui.Windows
{
    /// <summary>
    /// A class for displaying windows.
    /// </summary>
    public class Window : IDisposable
    {
        private bool _ShouldDrawCloseButton = true;
        private bool _ShouldDrawTitleBar = true;
        private bool _IsOpen = false;
        private System.Drawing.Point loc = new System.Drawing.Point(0, 0);
        private Point OldMouseLoc;
        private int CloseWidth = 10;
        private int CloseHeight = TitlebarHeight;
        public static int TitlebarHeight = 10;
        private bool WindowMoving;

        /// <summary>
        /// Can the close button be drawn?
        /// </summary>
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
        /// <summary>
        /// Can the window be moved?
        /// </summary>
        public bool WindowMoveable { get; set; } = true;
        /// <summary>
        /// Should the window draw a title bar?
        /// </summary>
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
        /// <summary>
        /// Checks if window is open.
        /// </summary>
        public bool IsOpen { get { return _IsOpen; } }
        /// <summary>
        /// The location of the window.
        /// </summary>
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
        /// <summary>
        /// The size of the window
        /// </summary>
        public Size Size
        {
            get; set;
        }
        /// <summary>
        /// A list of controls.
        /// </summary>
        public List<Control> Controls = new List<Control>();
        /// <summary>
        /// Window Constructor
        /// </summary>
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

            this.DrawAll();
        }

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
                Display.DrawString(Text, Color.White, this.Location.X, this.Location.Y);

                //Draw Close button
                if (ShouldDrawCloseButton)
                {
                    Display.DrawRectangle(
                        this.Location.X + this.Size.Width - CloseWidth,
                        this.Location.Y,
                        CloseWidth,
                        CloseHeight,
                        Color.Red);

                    Display.DrawString("X", Color.White, this.Location.X + this.Size.Width - CloseWidth + 2, this.Location.Y);
                }
            }
            //Call custom draw function
#pragma warning disable CS0618
            Draw();
#pragma warning restore CS0618
        }
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
        }
        /// <summary>
        /// Updates everything on the window. Ex: checks if close button clicked.
        /// </summary>
        public void UpdateAll()
        {
            if (this.IsOpen)
            {
                Update();
                if (MouseManager.MouseState == MouseState.Left)
                {
                    //Check if Close button is clicked
                    if (UiMouse.MouseY >= this.Location.Y && UiMouse.MouseY <= this.Location.Y + TitlebarHeight && ShouldDrawCloseButton)
                    {
                        if (UiMouse.MouseX >= (this.Location.X + this.Size.Width - CloseWidth) && UiMouse.MouseX <= (this.Location.X + this.Size.Width - CloseWidth) + CloseWidth)
                        {
                            DesktopManager.CloseWindow(this);
                            return;
                        }
                    }
                    if (Utils.DoesMouseCollideWithArea(0, 0, this.Size.Width, TitlebarHeight) && ShouldDrawTitleBar && WindowMoveable)
                    {
                        WindowMoving = true;
                        OldMouseLoc = new Point(UiMouse.MouseX, UiMouse.MouseY);
                    }
                }
                else
                {
                    WindowMoving = false;
                    OldMouseLoc = new Point();
                }
                if (WindowMoving)
                {
                    this.Location = new Point(UiMouse.MouseX - OldMouseLoc.X, UiMouse.MouseY - OldMouseLoc.Y);
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
        /// <summary>
        /// Closes and disposes the window.
        /// </summary>
        public void Dispose()
        {
            this.Close();
            this.Controls = null;
        }
    }
}
