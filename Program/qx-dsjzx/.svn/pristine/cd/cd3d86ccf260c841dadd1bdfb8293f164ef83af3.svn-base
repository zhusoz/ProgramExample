using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ProgramsNetCore.Common
{
    /// <summary>
    /// 日志
    /// </summary>
    public abstract class LogHelper
    {
        #region 错误日志（本地）

        /// <summary>
        ///  写入到本地
        /// </summary>
        public static void WriteToLocal(string msg)
        {
            string path = AppContext.BaseDirectory;
            string logPath = Path.Combine(path, "Log");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            string filePath = Path.Combine(logPath, $"{DateTime.Today.ToString("yyyyMMdd")}.txt");
            FileStream fs = new FileStream(filePath, FileMode.Append);
            msg = $"[{DateTime.Now}] {msg}\r\n\r\n";
            byte[] bytes = Encoding.Default.GetBytes(msg);
            fs.Write(bytes, 0, bytes.Length);
            fs.Flush();
            fs.Close();
        }

        #endregion

        #region Quartz日志
        public static void QuartzTaskWriteToLocal(string msg)
        {
            string path = AppContext.BaseDirectory;
            string logPath = Path.Combine(path, "QuartzLog");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            string filePath = Path.Combine(logPath, $"{DateTime.Today.ToString("yyyyMMdd")}.txt");
            FileStream fs = new FileStream(filePath, FileMode.Append);
            msg = $"[{DateTime.Now}] {msg}\r\n\r\n";
            byte[] bytes = Encoding.Default.GetBytes(msg);
            fs.Write(bytes, 0, bytes.Length);
            fs.Flush();
            fs.Close();
        }
        #endregion
    }
}
