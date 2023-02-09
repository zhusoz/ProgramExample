using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ProgramsNetCore.Common
{
    /// <summary>
    /// 磁盘信息
    /// </summary>
    public class DiskInfo
    {
        /// <summary>
        /// 总容量[单位:TB]
        /// </summary>
        public double TotalSize { get; set; }

        /// <summary>
        /// 使用量[单位:TB]
        /// </summary>
        public double UsedSize { get; set; }

        /// <summary>
        /// 剩余容量[单位:TB]
        /// </summary>
        public double AvailableSize { get; set; }

        /// <summary>
        /// 使用率
        /// </summary>
        public string Use { get; set; }
    }

    public class DiskInfoHelper
    {
        DiskInfoHelper() { }

        /// <summary>
        /// 获取磁盘剩余空间 [Windows:'C盘',Linux:'/'根路径]
        /// </summary>
        /// <returns></returns>
        public static double GetTotalSpace()
        {
            double total = 0;
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                total = GetHardDiskFreeSpace("C");
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                total = LinuxDisk("/").TotalSize;
            }

            return Math.Round(total, 2);
        }

        private static DiskInfo LinuxDisk(string path)
        {
            DiskInfo disk = new DiskInfo();
            if (string.IsNullOrEmpty(path))
            {
                return disk;
            }
            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }
            string shellPathLine = string.Format("cd {0}", path);
            string printLine = " awk '{print $2,$3,$4,$5}'";
            string shellLine = string.Format("df -k {0} |", path) + printLine;
            Process p = new Process();
            p.StartInfo.FileName = "sh";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(shellPathLine);
            p.StandardInput.WriteLine(shellLine);
            p.StandardInput.WriteLine("exit");
            string strResult = p.StandardOutput.ReadToEnd();
            string[] arr = strResult.Split('\n');
            if (arr.Length == 0)
            {
                return disk;
            }
            string[] resultArray = arr[1].TrimStart().TrimEnd().Split(' ');
            if (resultArray == null || resultArray.Length == 0)
            {
                return disk;
            }
            disk.TotalSize = Math.Round(Convert.ToDouble(resultArray[0])/(1024*1024*1024), 2);
            disk.UsedSize = Math.Round(Convert.ToDouble(resultArray[1])/(1024*1024*1024), 2);
            disk.AvailableSize = Math.Round(Convert.ToDouble(resultArray[2])/(1024*1024*1024), 2);
            disk.Use = resultArray[3];

            Console.WriteLine(string.Format("Linux获取目录：{0},总大小:{1},已用:{2},未用:{3},使用率:{4}", path, disk.TotalSize, disk.UsedSize, disk.AvailableSize, disk.Use));
            //logger.Info(string.Format("Linux获取目录：{0},总大小:{1},已用:{2},未用:{3},使用率:{4}", path, disk.TotalSize, disk.UsedSize, disk.AvailableSize, disk.Use));
            return disk;
        }

        ///  获取指定驱动器的空间总大小(单位为TB);只需输入代表驱动器的字母即可 （大写） 
        private static double GetHardDiskSpace(string str_HardDiskName)
        {
            double totalSize = new double();
            str_HardDiskName=str_HardDiskName +@":\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    totalSize = drive.TotalSize / (1024 * 1024 * 1024);
                }
            }
            return totalSize/1024;
        }

        ///  获取指定驱动器的剩余空间总大小(单位为TB) 只需输入代表驱动器的字母即可  
        private static double GetHardDiskFreeSpace(string str_HardDiskName)
        {
            double freeSpace = new double();
            str_HardDiskName = str_HardDiskName + @":\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    freeSpace = drive.TotalSize / (1024 * 1024 * 1024);

                }
            }
            return freeSpace/1024;
        }

    }
}
