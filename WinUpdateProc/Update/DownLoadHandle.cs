using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.Diagnostics;
using LitJson;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;

namespace WinUpdateProc {

    class DownLoadHandle {

        private WebClient client = new WebClient ();
        private string fileNameNoExt;
        private string url;
        private string version;
        private string txtVersion;
        private string zipFile;
        private string runFile;
        private TextBox textBox;
        private TextBox errorBox;

        public DownLoadHandle (System.Windows.Forms.TextBox textBox, System.Windows.Forms.TextBox errorBox) {
            this.textBox = textBox;
            this.errorBox = errorBox;
        }

        public void Handle () {
            JsonData jsonData = ReadConfig ();
            fileNameNoExt = jsonData["FileNameNoExt"].ToString ();
            url = @jsonData["Url"].ToString ();
            version = @jsonData["Md5"].ToString ();
            txtVersion = @jsonData["VersionTxt"].ToString ();
            zipFile = fileNameNoExt + ".zip";
            runFile = fileNameNoExt + Path.DirectorySeparatorChar + @jsonData["RunFile"].ToString ();

            if (!CheckVersion ()) {
            // if (false) {
                bool succ = true;
                try {
                    textBox.Paste ("发现新版本\r\n");
                    textBox.Paste ("删除本地文件...");
                    DeleteOldFile ();
                    textBox.Paste ("  OK\r\n");
                    textBox.Paste ("删除缓存...");
                    DeleteCache ();
                    textBox.Paste ("OK\r\n");
                    textBox.Paste ("更新文件...");
                    Random rd = new Random ();
                    string url2 = url + "?random=" + rd.Next (100);
                    client.DownloadFile (url2, zipFile);
                    textBox.Paste ("  OK\r\n");
                    textBox.Paste ("正在解压...");
                    string msg = "";
                    succ = UnZipFile (zipFile, "", out msg);
                    if (succ) {
                        textBox.Paste ("OK\r\n");
                    } else {
                        throw new Exception ("解包出错！" + msg);
                    }
                    textBox.Paste ("正在打开程序。\r\n");
                } catch (Exception e) {
                    succ = false;
                    textBox.Paste ("更新失败：" + e.Message + "\r\n");
                }
                if (succ) {
                    Process p = new Process ();
                    p.StartInfo.FileName = System.IO.Directory.GetCurrentDirectory () + Path.DirectorySeparatorChar + runFile;
                    p.Start ();
                    p.Dispose ();
                    // Application.Exit ();
                    // System.Environment.Exit (0);
                }
            } else {
                textBox.Paste ("已经是最新版本了...\r\n");
                // if (!CheckTxtVersion ()) {
                //     textBox.Paste ("删除缓存...\r\n");
                //     DeleteCache ();
                //     textBox.Paste ("OK...\r\n");
                // }
                Process p = new Process ();
                p.StartInfo.FileName = System.IO.Directory.GetCurrentDirectory () + Path.DirectorySeparatorChar + runFile;
                p.Start ();
                p.Dispose ();
                // System.Environment.Exit (0);
            }
            string fileName = @jsonData["RunFile"].ToString ();
            fileName = fileName.Split ('.')[0];
            textBox.Paste ("fileName:" + fileName + "\r\n");
            ReadLogThread.GetInstance ().textBox = textBox;
            ReadLogThread.GetInstance ().errorBox = errorBox;
            ReadLogThread.GetInstance ().dir = fileNameNoExt;
            ReadLogThread.GetInstance ().gameName = fileName;
            ReadLogThread.GetInstance ().Start ();
        }

        private JsonData ReadConfig () {
            StreamReader sr = new StreamReader ("config.json", Encoding.Default);
            string line = null;
            string content = "";
            while ((line = sr.ReadLine ()) != null) {
                content += line;
            }
            sr.Close ();
            JsonData jsonData = JsonMapper.ToObject (content);
            return jsonData;
        }

        private bool CheckVersion () {
            textBox.Paste ("检查版本...\r\n");
            Random rd = new Random ();
            string cli = version + "?random=" + rd.Next (100);
            string remotVersion = client.DownloadString (cli);
            if (File.Exists (zipFile)) {
                string localVersion = GetMD5HashFromFile (zipFile);
                if (localVersion.Equals (remotVersion)) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        private bool CheckTxtVersion () {
            Random rd = new Random ();
            string verMd5 = txtVersion + "?random=" + rd.Next (100);
            string remotVerMd5 = client.DownloadString (verMd5);
            string localPath = GetLocalPath ();
            if (localPath != null) {
                string path = localPath + Path.DirectorySeparatorChar + "version";
                if (File.Exists (path)) {
                    string localVersion = GetMD5HashFromFile (path);
                    if (remotVerMd5 == null || "00000".Equals (remotVerMd5)) {
                        return false;
                    }
                	if (!remotVerMd5.Equals (localVersion)) {
                        return false;
                    } else {
                        return true;
                    }
                }
                return true;
            } else {
                return true;
            }
        }

        public void DeleteOldFile () {
            if (File.Exists (zipFile)) {
                File.Delete (zipFile);
            }
            DirectoryInfo dir = new DirectoryInfo (fileNameNoExt);
            if (dir.Exists) {
                FileInfo[] fileInfo = dir.GetFiles ();
                foreach (FileInfo file in fileInfo) {
                    file.Delete ();
                }

                DirectoryInfo[] dirInfos = dir.GetDirectories ();
                foreach (DirectoryInfo directory in dirInfos) {
                    directory.Delete (true);
                }
            }
        }

        private void DeleteCache () {
            string path = GetLocalPath ();
            if (path != null) {
                DirectoryInfo dir = new DirectoryInfo (path);
                if (dir.Exists) {
                    FileInfo[] fileInfo = dir.GetFiles ();
                    foreach (FileInfo file in fileInfo) {
                        file.Delete ();
                    }

                    DirectoryInfo[] dirInfos = dir.GetDirectories ();
                    foreach (DirectoryInfo directory in dirInfos) {
                        directory.Delete (true);
                    }
                }
            }
        }

        private string GetLocalPath () {
            string path = Environment.GetFolderPath (Environment.SpecialFolder.LocalApplicationData);
            string pathLow = path + "Low";
            DirectoryInfo dir = new DirectoryInfo(path + Path.DirectorySeparatorChar + "fssj_shiyuegame_com" + Path.DirectorySeparatorChar + "风色世界");
            if (dir.Exists) {
                return path + Path.DirectorySeparatorChar + "fssj_shiyuegame_com" + Path.DirectorySeparatorChar + "风色世界";
            } else {
                dir = new DirectoryInfo(pathLow + Path.DirectorySeparatorChar + "fssj_shiyuegame_com" + Path.DirectorySeparatorChar + "风色世界");
                if (dir.Exists) {
                    return pathLow + Path.DirectorySeparatorChar + "fssj_shiyuegame_com" + Path.DirectorySeparatorChar + "风色世界";
                } else {
                    return null;
                }
            }
        }

        private static string GetMD5HashFromFile (string fileName) {
            try {
                FileStream file = new FileStream (fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider ();
                byte[] retVal = md5.ComputeHash (file);
                file.Close ();
                StringBuilder sb = new StringBuilder ();
                for (int i = 0; i < retVal.Length; i++) {
                    sb.Append (retVal[i].ToString ("x2"));
                }
                return sb.ToString ();
            } catch (Exception ex) {
                throw new Exception ("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        /// <summary>
        /// 功能：解压zip格式的文件。
        /// </summary>
        /// <param name="zipFilePath">压缩文件路径</param>
        /// <param name="unZipDir">解压文件存放路径,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹</param>
        /// <param name="err">出错信息</param>
        /// <returns>解压是否成功</returns>
        public static bool UnZipFile (string zipFilePath, string unZipDir, out string err) {
            err = "";
            if (zipFilePath == string.Empty) {
                err = "压缩文件不能为空！";
                return false;
            }
            if (!File.Exists (zipFilePath)) {
                err = "压缩文件不存在！";
                return false;
            }
            //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹
            if (unZipDir == string.Empty)
                unZipDir = zipFilePath.Replace (Path.GetFileName (zipFilePath), Path.GetFileNameWithoutExtension (zipFilePath));
            if (!unZipDir.EndsWith ("//"))
                unZipDir += "//";
            if (!Directory.Exists (unZipDir))
                Directory.CreateDirectory (unZipDir);

            try {
                using (ZipInputStream s = new ZipInputStream (File.OpenRead (zipFilePath))) {

                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry ()) != null) {
                        string directoryName = Path.GetDirectoryName (theEntry.Name);
                        string fileName = Path.GetFileName (theEntry.Name);
                        if (directoryName.Length > 0) {
                            Directory.CreateDirectory (unZipDir + directoryName);
                        }
                        if (!directoryName.EndsWith ("//"))
                            directoryName += "//";
                        if (fileName != String.Empty) {
                            using (FileStream streamWriter = File.Create (unZipDir + theEntry.Name)) {

                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true) {
                                    size = s.Read (data, 0, data.Length);
                                    if (size > 0) {
                                        streamWriter.Write (data, 0, size);
                                    } else {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                err = ex.Message;
                return false;
            }
            return true;
        }
    }
}