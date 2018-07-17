using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using WinUpdateProc;

using LitJson;

public class HttpRequestHandle {

    private static HttpRequestHandle Instance = null;

    public Queue<ErrorVo> queue = new Queue<ErrorVo>();
    public string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

    private Object lockObj = new Object();
    private Thread thread;
    private string lastUploadStr = null;

    private HttpRequestHandle() { }
    public static HttpRequestHandle GetInstance() {
        if (Instance == null) {
            Instance = new HttpRequestHandle();
        }
        return Instance;
    }

    public void AddError(ErrorVo vo) {
        lock (lockObj) {
            // if (lastUploadStr == null || !lastUploadStr.Equals(vo.errorTxt)) {
            //     lastUploadStr = vo.errorTxt;
            //     queue.Enqueue(vo);
            // }
            queue.Enqueue(vo);
        }
    }

    public void AddErrors(List<ErrorVo> voList) {
        lock (lockObj) {
            foreach (ErrorVo vo in voList) {
                queue.Enqueue(vo);
            }
        }
    }

    public void Start() {
        ThreadStart startDownload = new ThreadStart (Run);
    	thread = new Thread (startDownload);
        thread.IsBackground = true;
    	thread.Start ();
    }

    public void Run() {
        int time = 500;
        while (true) {
            Thread.Sleep(time);
            try {
                ErrorVo vo = null;
                lock (lockObj) {
                    if (queue.Count > 0) {
                        vo = queue.Dequeue();
                        time = 100;
                    } else {
                        time = 500;
                    }
                }
                if (vo != null && ReadLogThread.GetInstance().errorUrl != null) {
                    HttpGet(ReadLogThread.GetInstance().errorUrl, vo);
                }
            } catch (Exception e) {
                if (ReadLogThread.GetInstance().errorBox != null) {
                    ReadLogThread.GetInstance().errorBox.AppendText("上报错误日志出错：" + e.Message + "\r\n");
                }
            }
        }
    }

    public string HttpGet(string Url, ErrorVo vo) {
        string urlparam = Url + "&user=" + UrlEncode(vo.user) + "&errorExt=" + UrlEncode(vo.errorTxt);
        // if (ReadLogThread.GetInstance().errorBox != null) {
        //     ReadLogThread.GetInstance().errorBox.AppendText("上报Url：" + urlparam + "\r\n");
        // }
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlparam);
        request.Timeout = 2000;
        request.Method = "GET";
        request.ContentType = "text/html;charset=UTF-8";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream myResponseStream = response.GetResponseStream();
        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
        string retString = myStreamReader.ReadToEnd();
        myStreamReader.Close();
        myResponseStream.Close();
        if (ReadLogThread.GetInstance().errorBox != null && retString != null && !retString.Contains("succ")) {
            ReadLogThread.GetInstance().errorBox.AppendText("上报出错：" + retString + "\r\n");
        }
        return retString;
    }

    private string HttpPost(string Url, ErrorVo vo) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
        request.Timeout = 2000;
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.UserAgent = DefaultUserAgent;
        //如果需要POST数据  
        StringBuilder buffer = new StringBuilder();
        buffer.AppendFormat("{0}={1}", "errorTxt", vo.errorTxt);
        buffer.AppendFormat("&{0}={1}", "projectName", vo.projectName);
        buffer.AppendFormat("&{0}={1}", "user", vo.user);
        if (ReadLogThread.GetInstance().errorBox != null) {
            ReadLogThread.GetInstance().errorBox.AppendText("上报参数：" + buffer.ToString() + "\r\n");
        }
        byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
        using (Stream stream = request.GetRequestStream()) {
            stream.Write(data, 0, data.Length);
        }
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        // Stream myResponseStream = response.GetResponseStream();
        // StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
        // string retString = myStreamReader.ReadToEnd();
        // myStreamReader.Close();
        // myResponseStream.Close();
        return "";
    } 

    private string UrlEncode(string str) {
        if (str == null) {
            return "";
        }
        StringBuilder sb = new StringBuilder();
        byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
        for (int i = 0; i < byStr.Length; i++) {
            sb.Append(@"%" + Convert.ToString(byStr[i], 16));
        }
        return (sb.ToString());
    }
}

public class ErrorVo {
    public string errorTxt = null;
    public string projectName = null;
    public string user = null;

    public ErrorVo(string errorTxt, string projectName, string user) {
        this.errorTxt = errorTxt;
        this.projectName = projectName;
        this.user = user;
    }
}
