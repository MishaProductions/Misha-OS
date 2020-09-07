using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MishaOS.Gui.Windows.Controls
{
    public class Control
    {
      //  public event EventHandler OnClick;
       // public Point Location = new Point(0,0);
      //  public System.Drawing.Size Size = new System.Drawing.Size(100,10);
        //public bool Enabled = true;
        public Control()
        {
           // Draw();
        }
        public void Click()
        {
          //  OnClick.Invoke(this,new EventArgs());
        }
        public virtual void Draw() { }
        /// <summary>
        /// Main control loop function
        /// </summary>
        public virtual void Update()
        {
            //if (!Enabled) return;
            //if (MouseManager.MouseState == MouseState.Left)
            //{
            //    if (UiMouse.MouseY >= Location.Y && UiMouse.MouseY <= Location.Y + Size.Height)
            //    {
            //        if (UiMouse.MouseX >= Location.X && UiMouse.MouseX <= Location.X + Size.Width)
            //        {
            //            Click();
            //        }
            //    }
            //}
        }
    }
}
