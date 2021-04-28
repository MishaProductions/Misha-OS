using MishaOS.Drivers;

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
                this.DrawAll();
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
            
        }
        public override void Draw()
        {
            if (this.ParrentWindow !=null && this.Visible)
            {
                Display.DrawString(
                Text,
                this.ForeColor,
                this.Location.X+this.ParrentWindow.ClientLocation.X, this.Location.Y + this.ParrentWindow.ClientLocation.Y);
            }
        }
    }
}
