using Cosmos.System.Graphics.Fonts;
using MishaOS.Drivers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MishaOS.Gui.Windows.Controls
{
    public class Label : Control
    {
        private string _Text = "Label";
        private System.Drawing.Point _Loc = new System.Drawing.Point(0,0);
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

        public new System.Drawing.Point Location
        {
            get
            {
                return _Loc;
            }
            set
            {
                _Loc = value;
                if (this._ParrentWindow !=null)
                {
                    this.Draw();
                }
            }
        }

        public Label()
        {
            this.DrawDefaultSquare = false;
        }
        public override void Draw()
        {
            if (this.ParrentWindow !=null)
            {
                base.Draw();
                Display.DrawString(
                Text,
                new Cosmos.System.Graphics.Pen(this.ForeColor),
                this.Location.X, this.Location.Y);
            }
        }
    }
}
