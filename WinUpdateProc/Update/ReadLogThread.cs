using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace WinUpdateProc {

    class ReadLogThread {

        public static ReadLogThread instance = null;

        private Thread thread;
        public TextBox textBox { get; set; }
        public string dir { get; set; }

        private ReadLogThread () { }

        public static ReadLogThread GetInstance () {
            if (instance == null) {
                instance = new ReadLogThread();
            }
            return instance;
        }


        public void Start () {
            ThreadStart startDownload = new ThreadStart (Run);
            thread = new Thread (startDownload);
            thread.Start ();
        }

        public void Abort () {
            if (thread != null) {
                thread.Abort();
            }
        }

        public void Run () {
            string url = @"warbird-windows\fswy_Data\output_log.txt";
            var wh = new AutoResetEvent (false);
            var fsw = new FileSystemWatcher (@"warbird-windows\fswy_Data");
            fsw.Filter = "output_log.txt";
            fsw.Changed += (s, e) => wh.Set ();

            var fs = new FileStream (url, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            bool initRead = false;
            using (var sr = new StreamReader (fs)) {
                var s = "";
                Queue<string> queue = new Queue<string> ();
                while (true) {
                    s = sr.ReadLine ();
                    if (s != null) {
                        if (textBox != null && initRead) {
                            while (queue.Count > 0) {
                                textBox.Paste (queue.Dequeue() + "\r\n");
                            }
                            if (!s.Contains ("UnityEngineDebug")) {
                                textBox.Paste (s + "\r\n");
                            }
                        } else {
                            if (queue.Count > 10) {
                                queue.Dequeue ();
                            }
                            queue.Enqueue (s);
                        }
                    } else {
                        initRead = true;
                        wh.WaitOne (1000);
                    }
                }
            }
            wh.Close ();
        }
    }
}
