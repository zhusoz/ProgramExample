using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyToDo.ViewModels
{
    public class AboutViewModel
    {
        public AboutViewModel()
        {
            OpenInDefaultExplorerCommand=new DelegateCommand<Uri>(OpenInDefaultExplorer);
        }

        // 打开默认浏览器网址
        public ICommand OpenInDefaultExplorerCommand { get; private set; }
        private void OpenInDefaultExplorer(Uri url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow= true });
            }
        }
    }
}
