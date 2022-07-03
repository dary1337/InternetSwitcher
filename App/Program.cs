using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace InternetSwitcher {

    internal static class Program {

        [STAThread]
        static void Main() {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Exception);
            Application.Run(new mainForm());
        }

        static void Exception(object sender, ThreadExceptionEventArgs e) {

            if (MessageBox.Show(dict.local["exp1_" + mainForm.lang] + e.Exception.ToString() + dict.local["exp2_" + mainForm.lang],
                "Exception", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Process.Start("https://github.com/dary1337/InternetSwitcher/issues");

            Application.Exit();
        }

    }
}
