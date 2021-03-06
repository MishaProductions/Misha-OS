using Cosmos.Core;
using Cosmos.System;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using MishaOS.Drivers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MishaOS.Gui.Windows.Controls
{
    /// <summary>
    /// A control that is a button.
    /// </summary>
    [Obsolete]
    public class Button : Control
    {
        [Obsolete]
        private EventHandler OnClickEventHandler;
        [Obsolete]
        /// <summary>
        /// Invoked when button is clicked.
        /// </summary>
        public event EventHandler OnClick
        {

            add
            {
                OnClickEventHandler = value;
            }

            remove
            {
                OnClickEventHandler -= value;
            }

        }
        [Obsolete]
        private string _Text = "Button";
        [Obsolete]
        /// <summary>
        /// The text of the button
        /// </summary>
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
                this.DrawAll();
            }
        }
        [Obsolete]
        public override void Draw()
        {
            if (!Visible | this.ParrentWindow == null)
                return;
            base.Draw();
            int x = this.Location.X;
            int y = this.Location.Y;
            Display.DrawString(
                Text,
                new Cosmos.System.Graphics.Pen(this.ForeColor),
                this.ParrentWindow.ClientLocation.X + x, this.ParrentWindow.ClientLocation.Y + y);
        }
        [Obsolete]
        public override void Update()
        {
            int width = this.Size.Width;
            int height = this.Size.Height;
            int ProperX = this.Location.X + this.ParrentWindow.ClientLocation.X;
            int ProperY = this.Location.Y + this.ParrentWindow.ClientLocation.Y;


            if (MouseManager.MouseState == MouseState.Left && Enabled && Visible)
            {
                if (UiMouse.MouseY >= ProperY && UiMouse.MouseY <= ProperY + height)
                {
                    if (UiMouse.MouseX >= ProperX && UiMouse.MouseX <= ProperX + width)
                    {
                        if (OnClickEventHandler != null)
                            OnClickEventHandler.Invoke(this, new EventArgs());
                    }
                }
            }
        }
    }
}
