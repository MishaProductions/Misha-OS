using Cosmos.System;
using Cosmos.System.Graphics;
using MishaOS.Drivers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Gui.Windows.Controls
{
    /// <summary>
    /// A class that represents a control such as a button or label.
    /// </summary>
    public class Control
    {
        /// <summary>
        /// The Tag of the control. Can be used to store information about this control.
        /// </summary>
        public object Tag { get; set; }
        /// <summary>
        /// The Background Color of the control
        /// </summary>

        public Color BackgroundColor = Color.White;
        /// <summary>
        /// The text color of the control
        /// </summary>
        public Color ForeColor = Color.Black;
        private System.Drawing.Point _Loc;
        /// <summary>
        /// The location of the control
        /// </summary>
        public System.Drawing.Point Location
        {
            get
            {
                return _Loc;
            }
            set
            {
                int newX = value.X;
                int newY = value.Y;

                if (value.Y <= -1)
                {
                    newY = 0;
                }
                if (value.X <= -1)
                {
                    newX = 0;
                }

                if (_ParrentWindow != null)
                {
                    if (value.Y > _ParrentWindow.Size.Height)
                    {
                        newY = _ParrentWindow.Size.Height - 1;
                    }
                    if (value.X > _ParrentWindow.Size.Width)
                    {
                        newX = _ParrentWindow.Size.Width - 1;
                    }
                }
                _Loc = new System.Drawing.Point(newX, newY);
            }
        }
        /// <summary>
        /// The size of the control
        /// </summary>
        public Size Size = new Size(10, 10);
        /// <summary>
        /// Weather or not if the control is enabled
        /// </summary>
        public bool Enabled = true;
        /// <summary>
        /// Does the control have focus?
        /// </summary>
        public bool HasFocus = true;

        /// <summary>
        /// For internal use only. Please do not modify
        /// </summary>
        internal Window _ParrentWindow;
        /// <summary>
        /// The Window that the control is attached to.
        /// </summary>
        public Window ParrentWindow { get { return _ParrentWindow; } }

        private bool vis = true;
        /// <summary>
        /// Can the user see the control?
        /// </summary>
        public bool Visible
        {
            get { return vis; }
            set { vis = value; this.DrawAll(); }
        }
        /// <summary>
        /// The controls inside this control.
        /// </summary>
        public List<Control> Controls = new List<Control>();
        public Control()
        {
            DrawAll();
        }

        public void DrawAll()
        {
            if (!Visible | this.ParrentWindow == null) //Skip drawing if not visible
                return;

            //Draw the background
            if (BackgroundColor != Color.Transparent)
                Display.DrawRectangle(Location.X + this.ParrentWindow.ClientLocation.X, Location.Y + this.ParrentWindow.ClientLocation.Y, Size.Width, Size.Height, BackgroundColor);

            //Draw all the controls in this control
            foreach (Control d in Controls)
            {
                d.DrawAll();
            }
            //Call custom draw function
            Draw();
        }
        /// <summary>
        /// Draw function. Please do not call dirrectly, insted call DrawAll()
        /// </summary>
        public virtual void Draw()
        {

        }
        /// <summary>
        /// Main control loop function
        /// </summary>
        public virtual void Update()
        {
            //Update all controls
            foreach (Control d in Controls)
            {
                d.Update();
                if (d.ParrentWindow == null)
                    d._ParrentWindow = this.ParrentWindow;
            }
        }
    }
}
