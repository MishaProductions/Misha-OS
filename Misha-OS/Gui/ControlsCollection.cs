using MishaOS.Gui.Windows;
using MishaOS.Gui.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MishaOS.Gui
{
    public class ControlCollection
    {
        private Window ParrentWindow;
        private List<Control> controls = new List<Control>();
        public ControlCollection(Window ParrentWindow)
        {
            this.ParrentWindow = ParrentWindow;
        }

        public List<Control> GetControls
        {
            get
            {
                return controls;
            }
        }
        public void Add(Control c)
        {
            c._ParrentWindow = ParrentWindow;
            controls.Add(c);
        }
        public void Remove(Control c)
        {
            controls.Remove(c);
        }
    }
}
