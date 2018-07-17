using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace WinUpdateProc {

    class ReadLogThread {

        public static ReadLogThread instance = null;

        public string gamePackageCache;
        public string gamePackageLog;
        public string gameNameCache;
        public string zipPackage;

        private Thread thread;
        public TextBox textBox { get; set; }
        public TextBox errorBox { get; set; }
        public string gameName { get; set; }
        public string user { get; set; }
        public string errorUrl { get; set; }

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
            thread.IsBackground = true;
            thread.Start ();
        }

        public void Abort () {
            if (thread != null) {
                thread.Abort();
            }
        }

        public void Run () {
            string url = GetLocalPath() + Path.DirectorySeparatorChar + "output_log.txt";
            FileInfo fileInfo = new FileInfo (url);
            if (fileInfo.Exists) {
                textBox.Paste ("exists\r\n");
                Tailf ();
            } else {
                while (true) {
                    Thread.Sleep (1000);
                    textBox.Paste ("not exists\r\n");
                    fileInfo = new FileInfo (url);
                    if (fileInfo.Exists) {
                        // textBox.Paste (url + "not exists taif \r\n");
                        Tailf ();
                        return;
                    }
                }
            }
        }

        private void Tailf () {
            string url = GetLocalPath() + Path.DirectorySeparatorChar + "output_log.txt";
            var wh = new AutoResetEvent (false);
            var fsw = new FileSystemWatcher(zipPackage + @"\" + gameName + "_Data");
            fsw.Filter = "output_log.txt";
            fsw.Changed += (s, e) => wh.Set ();

            var fs = new FileStream (url, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            bool initRead = false;
            int count = 0;
            bool deleteRn = false;
            bool isError = false;
            bool isAnimatorError = false;
            using (var sr = new StreamReader (fs)) {
                var s = "";
                Queue<string> queue = new Queue<string> ();
                string errLog = "";
                while (true) {
                    s = sr.ReadLine ();
                    if (s != null) {
                        if (textBox != null && initRead) {
                            while (queue.Count > 0) {
                                textBox.Paste (queue.Dequeue() + "\r\n");
                            }
                            if (!s.Contains ("UnityEngineDebug")) {
                                if (s.Trim ().Length > 0 || !deleteRn) {
                                    if (s.Contains ("[ERROR]")) {
                                        isError = true;
                                    } else if (s.Contains ("Animator.GotoState: State could not be found")) {
                                        s = "[动作不存在]" + s;
                                        isError = true;
                                        isAnimatorError = true;
                                    } else if (s.Contains ("[/ERROR]")) {
                                        if (errLog.Length > 0) {
                                            errLog = errLog + "<br>" + s;
                                            HttpRequestHandle.GetInstance().AddError(new ErrorVo(errLog, gameName, user));
                                            errLog = "";
                                        }
                                        isError = false;
                                    } else if (isAnimatorError) {
                                        if (errLog.Length > 0) {
                                            errLog = errLog + "<br>" + s;
                                            HttpRequestHandle.GetInstance().AddError(new ErrorVo(errLog, gameName, user));
                                            errLog = "";
                                        }
                                        isAnimatorError = false;
                                        isError = false;
                                    }
                                    if (isError) {
                                        errorBox.Paste (s + "\r\n");
                                	    errorBox.SelectionStart = errorBox.Text.Length;
                                        errLog = errLog + "<br>" + s;
                                        if (s.Contains("[/ERROR]")) {
                                            HttpRequestHandle.GetInstance().AddError(new ErrorVo(errLog, gameName, user));
                                            errLog = "";
                                            isError = false;
                                        }
                                    }
                                    textBox.Paste (s + "\r\n");
                                	count++;
                                	if (count % 5 == 0) {
                                	    textBox.SelectionStart = textBox.Text.Length;
                                	}
                                }
                                deleteRn = false;
                            } else {
                                deleteRn = true;
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

        private string GetLocalPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string pathLow = path + "Low";
            DirectoryInfo dir = new DirectoryInfo(path + Path.DirectorySeparatorChar + gamePackageLog + Path.DirectorySeparatorChar + gameNameCache);
            if (dir.Exists)
            {
                textBox.Paste(dir + "\r\n");
                return path + Path.DirectorySeparatorChar + gamePackageLog + Path.DirectorySeparatorChar + gameNameCache;
            }
            else
            {
                dir = new DirectoryInfo(pathLow + Path.DirectorySeparatorChar + gamePackageLog + Path.DirectorySeparatorChar + gameNameCache);
                if (dir.Exists)
                {
                    textBox.Paste(dir + "\r\n");
                    return pathLow + Path.DirectorySeparatorChar + gamePackageLog + Path.DirectorySeparatorChar + gameNameCache;
                }
                else
                {
                    textBox.Paste(dir + "not exist\r\n");
                    return System.IO.Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + zipPackage + @"\" + gameName + "_Data";
                }
            }
        }
    }
}
