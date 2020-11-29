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
    public class Button : Control
    {
        private string _Text = "Button";
        private EventHandler OnClickEventHandler;
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
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
                this.Draw();
            }
        }


        public Button()
        {
        }
        public override void Draw()
        {
            base.Draw();
            if (!Visible)
                return;
            int width = this.Size.Width;
            int height = this.Size.Height;
            int x = this.Location.X;
            int y = this.Location.Y;
            Display.DrawString(
                Text,
                new Cosmos.System.Graphics.Pen(this.ForeColor),
                x, y);
        }

        public override void Update()
        {
            int width = this.Size.Width;
            int height = this.Size.Height;
            int x = this.Location.X;
            int y = this.Location.Y;
            if (MouseManager.MouseState == MouseState.Left && Enabled && Visible)
            {
                if (UiMouse.MouseY >= this.Location.Y && UiMouse.MouseY <= this.Location.Y + this.Size.Height)
                {
                    if (UiMouse.MouseX >= this.Location.X && UiMouse.MouseX <= this.Location.X + this.Size.Width)
                    {
                        if (OnClickEventHandler != null)
                            OnClickEventHandler.Invoke(this, new EventArgs());
                    }
                }
            }
        }
    }
}
