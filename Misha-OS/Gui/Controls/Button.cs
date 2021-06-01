using Cosmos.System;
using MishaOS.Drivers;
using System;

namespace MishaOS.Gui.Windows.Controls
{
    /// <summary>
    /// A control that is a button.
    /// </summary>
    public class Button : Control
    {
        private EventHandler OnClickEventHandler;
        private string _Text = "Button";
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
        public override void Draw()
        {
            base.Draw();
            int x = this.Location.X;
            int y = this.Location.Y;
            Display.DrawString(
                Text,
                this.ForeColor,
                this.ParrentWindow.ClientLocation.X + x, this.ParrentWindow.ClientLocation.Y + y);
        }

        public override void Update()
        {
            int width = this.Size.Width;
            int height = this.Size.Height;
            int ProperX = this.Location.X + this.ParrentWindow.ClientLocation.X;
            int ProperY = this.Location.Y + this.ParrentWindow.ClientLocation.Y;


            if (MouseManager.MouseState == MouseState.Left && Enabled && Visible)
            {
                if (Utils.DoesMouseCollideWithArea(ProperX, ProperY, width, height))
                {
                    if (OnClickEventHandler != null)
                        OnClickEventHandler.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
