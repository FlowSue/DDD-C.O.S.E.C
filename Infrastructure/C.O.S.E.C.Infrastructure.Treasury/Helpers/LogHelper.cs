﻿using C.O.S.E.C.Infrastructure.Treasury.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace C.O.S.E.C.Treasury.Helpers
{
    public static class LogHelper
    {
        private const string Htmltext = "<!DOCTYPE html PUBLIC\"-//W3C//DTD XHTML 1.0 Transitional//EN\"\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\"content=\"text/html; charset=utf-8\"/><title>日志信息</title><link rel=\"stylesheet\" href=\"../css.css\"/></head><body>[Log]</body></html>";
        private static readonly string FileName = DateTime.Now.ToString("yyyy-MM-dd") + ".html";

        /// <summary>
        /// 写入日志
        /// </summary>       
        /// <param name="type">日志类型</param>
        /// <param name="msg">日志信息</param>
        public static void SetLog(string logLevel, string msg, string WebRootPath)
        {
            try
            {
                string filepath = "//Log//" + PathStr(logLevel) + "//";
                string allfilepath = WebRootPath + filepath + FileName;
                var LogHtml = string.Empty;
                if (!FileHelper.FileExists(allfilepath))
                {
                    LogHtml = Htmltext;
                }
                else
                {
                    LogHtml = System.IO.File.ReadAllText(allfilepath);
                }

                var htmltxt = LogHtml.Replace("[Log]", LogStr(logLevel, msg));
                FileHelper.SaveFile(htmltxt, allfilepath, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 写入日志
        /// </summary>       
        /// <param name="type">日志类型</param>
        /// <param name="ex">Exception</param>
        public static void SetLog(string type, Exception ex, string WebRootPath)
        {
            try
            {
                var filepath = "//Log//" + PathStr(type) + "//";
                var allfilepath = WebRootPath + filepath + FileName;
                var LogHtml = string.Empty;
                if (!FileHelper.FileExists(allfilepath))
                {
                    LogHtml = Htmltext;
                }
                else
                {
                    LogHtml = File.ReadAllText(allfilepath);
                }
                var htmltxt = LogHtml.Replace("[Log]", LogStr(type, ex));
                FileHelper.SaveFile(htmltxt, allfilepath, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static string LogStr(string type, string msg)
        {
            var loghtml = "[Log]";
            loghtml += "<p>记录时间：" + DateTime.Now + "</p>";
            loghtml += "<p>日志类型：" + type + "信息</p>";
            loghtml += "<p>日志描述：" + msg + "</p>";
            loghtml += "<hr />";
            return loghtml;
        }
        private static string LogStr(string type, Exception e)
        {
            var loghtml = "[Log]";
            loghtml += "<p>记录时间：" + DateTime.Now + "</p>";
            loghtml += "<p>日志类型：" + type + "信息</p>";
            loghtml += "<p>日志描述：" + e.Message + "</p>";
            loghtml += "<p>异常根源：" + e.Source + "</p>";
            var stackTrace = e.StackTrace.Replace("at ", "</br>at ");
            loghtml += "<p>堆栈轨迹：" + stackTrace + "</p>";
            loghtml += "<hr />";
            return loghtml;
        }
        private static string PathStr(string type)
        {
            var info = string.Empty;
            switch (type)
            {
                case "错误":
                    info = "Error";
                    break;
                case "操作":
                    info = "Info";
                    break;
                case "调试":
                    info = "Debug";
                    break;
            }
            return info;
        }
        public static LogModel GetLog(string WebRootPath)
        {
            var m = new LogModel();
            var PathDebug = new DirectoryInfo(WebRootPath + "//Log//Debug");
            var Debug = new List<LogItem>();
            foreach (FileInfo file in PathDebug.GetFiles("*.html"))
            {
                var a = new LogItem
                {
                    FileName = file.FullName.Replace(WebRootPath, string.Empty)
                };
                Debug.Add(a);
            }
            var PathError = new DirectoryInfo(WebRootPath + "//Log//Error");
            var Error = new List<LogItem>();
            foreach (FileInfo file in PathError.GetFiles("*.html"))
            {
                var a = new LogItem
                {
                    FileName = file.FullName.Replace(WebRootPath, string.Empty)
                };
                Error.Add(a);
            }
            var PathInfo = new DirectoryInfo(WebRootPath + "//Log//Info");
            var Info = new List<LogItem>();
            foreach (FileInfo file in PathInfo.GetFiles("*.html"))
            {
                var a = new LogItem
                {
                    FileName = file.FullName.Replace(WebRootPath, string.Empty)
                };
                Info.Add(a);
            }
            m.Debug = Debug;
            m.Error = Error;
            m.Info = Info;
            return m;
        }
    }

    public class LogModel
    {
        public List<LogItem> Debug { get; set; }
        public List<LogItem> Error { get; set; }
        public List<LogItem> Info { get; set; }
    }
    public class LogItem
    {
        public string FileName { get; set; }
    }

    public static class LogLevel
    {
        public const string Debug = "调试";
        public const string Error = "错误";
        public const string Info = "操作";
    }
}
