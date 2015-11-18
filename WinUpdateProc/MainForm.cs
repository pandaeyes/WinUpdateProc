using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinUpdateProc {
    public partial class MainForm : Form {

        private Point mouseOff;
        private bool leftFlag = false;
        private Thread downloadThread;
        public bool running = false;

        public MainForm () {
            InitializeComponent ();
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;

            ThreadStart startDownload = new ThreadStart (Run);
            downloadThread = new Thread (startDownload);
            downloadThread.Start ();
        }

        private void Run () {
            DownLoadHandle load = new DownLoadHandle (this.OutputTextBox);
            try {
                running = true;
                load.Handle ();
                running = false;
            } catch (Exception e) {
                this.OutputTextBox.Paste ("出错了:" + e.Message);
            }

        }

        private void CloseButton_Click (object sender, EventArgs e) {
            // if (running) {
            //     DialogResult result = MessageBox.Show ("程序正在运行， 是否继续关闭？", "提示", MessageBoxButtons.YesNo);
            //     if (result == System.Windows.Forms.DialogResult.Yes) {
            //         downloadThread.Abort ();
            // 		Application.Exit ();
            //     }
            // } else {
            downloadThread.Abort ();
            ReadLogThread.GetInstance ().Abort ();
            System.Environment.Exit (0);
            // }
        }

        private void BackGroupImage_Down (object sender, MouseEventArgs e) {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) {
                Point local = Location;
                mouseOff = new Point (-(e.X + 389), -(e.Y - 10));
                leftFlag = true;
            }
        }

        private void BackGroupImage_Move (object sender, MouseEventArgs  e) {
            if (leftFlag) {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset (mouseOff.X, mouseOff.Y);
                Location = mouseSet; 
            }
        }

        private void BackGroupImage_Up (object sender, MouseEventArgs e) {
            if (leftFlag)
                leftFlag = false;
        }
    }
}
