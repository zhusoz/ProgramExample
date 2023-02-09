using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    public class SettingDto:BaseDto
    {
		/// <summary>
		/// 图标
		/// </summary>
		private string icon;

		public string Icon
		{
			get { return icon; }
			set { icon = value; }
		}

		/// <summary>
		/// 目标区域
		/// </summary>
		private string target;

		public string Target
		{
			get { return target; }
			set { target = value; }
		}


		/// <summary>
		/// 标题
		/// </summary>
		private string title;
		public string Title
		{
			get { return title; }
			set { title = value; }
		}

	}
}
