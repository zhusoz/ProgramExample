using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    public class TaskBar : BindableBase
    {

        private string title;
        private string icon;
        private string target;
        private string color;
        private string val;

        /// <summary>
        /// 值
        /// </summary>
        public string Val
        {
            get { return val; }
            set { val = value;RaisePropertyChanged(); }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        /// <summary>
        /// 目标
        /// </summary>
        public string Target
        {
            get { return target; }
            set { target = value; }
        }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color
        {
            get { return color; }
            set { color = value; }
        }


    }
}
