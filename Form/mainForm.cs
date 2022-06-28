using InternetSwitcher.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetSwitcher {

    public partial class mainForm : Form {

        protected override CreateParams CreateParams {

            get {
                const int CS_DROPSHADOW = 0x00030000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        public mainForm() {

            InitializeComponent();


            var pref = EdgeSupport.DWMWCP.DWMWCP_ROUND;
            EdgeSupport.DwmSetWindowAttribute(Handle, EdgeSupport.DWMWA.DWMWA_WINDOW_CORNER_PREFERENCE, ref pref, sizeof(uint));

            int dpi = ScreenResolution.scaleOfScreen; if (dpi != 100)
                Font = new Font("Microsoft YaHei", (dpi > 100 && dpi <= 125) ? 8f : (dpi > 125 && dpi <= 150) ? 7f : 6f, FontStyle.Regular, GraphicsUnit.Point, 0);

            Location = new Point(ScreenResolution.wWidth - 410, ScreenResolution.wHeight - 145);


            new List<Control> {

                this,
                label1,
                label3,
                pictureBox1

            }.ForEach(x => x.Click += (s, e) => Exit());


            if (Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Nls\Language").GetValue("Default").ToString() != "0419")
                lang = "en";

        }

        public static string lang = "ru";

        async void Form1_Load(object sender, EventArgs e) {

            if (Process.GetProcesses().Count(x => x.ProcessName == Path.GetFileName(Application.ExecutablePath).Replace(".exe", "")) == 1) {

                bool themeIsLight = true;

                async void cmdline(string line) {

                    try {

                        Process.Start(new ProcessStartInfo() {
                            FileName = "cmd",
                            Arguments = $@"/c {line} & exit",
                            WindowStyle = ProcessWindowStyle.Hidden
                        });
                    } catch {

                        label1.Text = dict.local["catch_"+lang];

                        pictureBox1.Image = themeIsLight ? Resources.LightWarning : Resources.DarkWarning;

                        await Task.Delay(4000);
                        Exit();
                    }
                }

                async void Notification() {

                    new SoundPlayer(Resources.Notify).Play();
                    await Task.Delay(3000);
                    Exit();
                }

                using (var Key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")) {

                    if (Key?.GetValue("AppsUseLightTheme")?.ToString() != "1") {

                        themeIsLight = false;

                        BackColor = Color.FromArgb(35, 40, 45);

                        foreach (Label lab in Controls.OfType<Label>())
                            lab.ForeColor = Color.Yellow;

                        label1.ForeColor = Color.White;
                    }
                }

                try {

                    if (new Ping().Send("google.com").Status.ToString().Contains("Success")) {

                        cmdline("netsh interface set interface Ethernet disable");

                        label1.Text = dict.local["iOff_" + lang];

                        pictureBox1.Image = themeIsLight ? Resources.DarkError : Resources.LightError;

                        Notification();
                    }
                } catch {

                    cmdline("netsh interface set interface Ethernet enable");

                    label1.Text = dict.local["iOn_" + lang];

                    pictureBox1.Image = themeIsLight ? Resources.Dark : Resources.Light;

                    Notification();
                }

                await Task.Delay(100);

                for (double d = 0D; d <= 0.95; d += 0.2, await Task.Delay(20))
                    Opacity = d;

                Opacity = 0.95D;
            }
            else
                Close();
        }

        async void Exit() {


            while (Opacity != 0) { Opacity -= 0.2; await Task.Delay(20); }

            Opacity = 0;

            await Task.Delay(70);
            Close();
        }
    }
}
