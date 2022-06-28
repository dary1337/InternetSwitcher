using System;
using System.Windows;
using System.Windows.Forms;

namespace InternetSwitcher {

    static internal class ScreenResolution {

        public static int scaleOfScreen = 100 * Screen.PrimaryScreen.Bounds.Width / rWidth;


        public static int rWidth => Convert.ToInt32(SystemParameters.PrimaryScreenWidth);

        public static int rHeight => Convert.ToInt32(SystemParameters.PrimaryScreenHeight);



        public static int wWidth => Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width);

        public static int wHeight => Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height);

    }
}