using System;
using System.Collections.Generic;
using System.Text;

namespace MishaOS
{

    class FPSCounter
    {
        private static DateTime lastTime;
        private static int fps = 0;
        private static int frames = 0;
        /// <summary>
        /// Should the kernel draw the fps counter?
        /// </summary>
        public static bool ShouldRender { get; set; } = false;

        public static int FPS
        {
            get
            {
                Update();
                return fps;
            }
        }


        private static void Update()
        {
            if ((DateTime.Now - lastTime).TotalMilliseconds >= 500)
            {
                lastTime = DateTime.Now;
                fps = frames * 2;
                frames = 0;
            }
            frames++;
        }
    }
}
