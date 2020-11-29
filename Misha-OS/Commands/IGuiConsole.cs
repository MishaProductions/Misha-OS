using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MishaOS.Commands
{
    public interface IGuiConsole
    {
        /// <summary>
        /// Gets the host.
        /// </summary>
        object term { get; set; }
        string CurrentDIR { get; set; }
        /// <summary>
        /// Gets the maxium colums that the display device can handle.
        /// </summary>
        int MaxCols { get; set; }
        /// <summary>
        /// Gets the title of the window
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// Gets or sets the text color of the console
        /// </summary>
        Color foreColor { get; set; }

        /// <summary>
        /// Gets or sets the background color of the console
        /// </summary>
        Color backColor { get; set; }
        /// <summary>
        /// Write text to the console.
        /// </summary>
        void Write(string text);
        /// <summary>
        /// Write text to the console then adds a new line.
        /// </summary>
        void WriteLine(string text);
        /// <summary>
        /// Adds a new line to the screen.
        /// </summary>
        void WriteLine();
        /// <summary>
        /// Clear the screen.
        /// </summary>
        void Clear();
        /// <summary>
        /// Shows a message and waits for the user to press any key.
        /// </summary>
        void Pause();
        /// <summary>
        /// Reads the key then returns it as a char.
        /// </summary>
        char ReadKey();
        /// <summary>
        /// Reads the keys before the user presses enter. Once the user presses enter, the text will be returned
        /// </summary>
        /// <returns></returns>
        string ReadLine();
    }
}
