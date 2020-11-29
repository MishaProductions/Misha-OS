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
    /// A class that represents a basic box.
    /// </summary>
    public class Control
    {
        /// <summary>
        /// The Tag of the control. Can be used to store information about this control.
        /// </summary>
        public object Tag { get; set; }

        public Color BackgroundColor = Color.White;
        public Color ForeColor = Color.Black;
        private System.Drawing.Point _Loc;
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
                _Loc = new System.Drawing.Point(newX,newY);
            }
        }
        public Size Size = new Size(10, 10);

        public virtual bool DrawDefaultSquare { get; set; } = true;

        public bool Enabled = true;

        public bool HasFocus = true;

        /// <summary>
        /// For internal use only.
        /// </summary>
        public Window _ParrentWindow;
        public Window ParrentWindow { get { return _ParrentWindow; } }
        private bool vis = true;
        /// <summary>
        /// Can the user see the control?
        /// </summary>
        public bool Visible
        {
            get { return vis; }
            set { vis = value;this.Draw(); }
        }

        public List<Control> Controls = new List<Control>();
        public Control()
        {
            Draw();
        }


        /// <summary>
        /// Draw function
        /// </summary>
        public virtual void Draw()
        {
            if (!Visible) //Skip drawing if not visible
                return;
            //Draw the default square
            if (DrawDefaultSquare)
                Display.DrawRectangle(Location.X, Location.Y, Size.Width, Size.Height, BackgroundColor);

            //Draw all the controls in this control
            foreach (Control d in Controls)
            {
                d.Draw();
            }
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
                d._ParrentWindow = this.ParrentWindow;
            }
        }
    }
}
