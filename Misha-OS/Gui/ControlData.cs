using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Gui
{
    /// <summary>
    /// Contains events and stuff.
    /// </summary>
    public class ControlData
    {
        /// <summary>
        /// The forecolor of the control. Default is black.
        /// </summary>
        public Color ForeColor { get; set; } = Color.Black;
        /// <summary>
        /// The BackColor of the control. Default is White.
        /// </summary>
        public Color BackColor { get; set; } = Color.White;
    }
}
