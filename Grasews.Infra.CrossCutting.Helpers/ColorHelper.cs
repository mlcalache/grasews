using System;
using System.Drawing;

namespace Grasews.Infra.CrossCutting.Helpers
{
    /// <summary>
    ///
    /// </summary>
    public static class ColorHelper
    {
        /// <summary>
        /// This returns a completly random color
        /// </summary>
        /// <returns></returns>
        public static Color GetRandomColor()
        {
            var rnd = new Random();

            var randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

            return randomColor;
        }

        /// <summary>
        /// This only returns green
        /// </summary>
        /// <returns></returns>
        public static Color GetRandomGreenColor()
        {
            var r = new Random();

            var randomGreen = Color.FromArgb(0, r.Next(0, 256), 0);

            return randomGreen;
        }

        /// <summary>
        /// This only returns red
        /// </summary>
        /// <returns></returns>
        public static Color GetRandomRedColor()
        {
            var r = new Random();

            var randomRed = Color.FromArgb(r.Next(0, 256), 0, 0);

            return randomRed;
        }

        /// <summary>
        /// This only returns blue
        /// </summary>
        /// <returns></returns>
        public static Color GetRandomBlueColor()
        {
            var r = new Random();

            var randomBlue = Color.FromArgb(0, 0, r.Next(0, 256));

            return randomBlue;
        }

        public static string ToHex(Color color)
        {
            return $"#{color.R.ToString("X2")}{color.G.ToString("X2")}{color.B.ToString("X2")}";
        }

        public static string ToRGB(Color color)
        {
            return $"RGB({color.R},{color.G},{color.B})";
        }
    }
}