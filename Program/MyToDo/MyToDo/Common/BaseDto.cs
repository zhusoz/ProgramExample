using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Common
{
    public class BaseDto
    {

		private int id;
        private DateTime createTime;
        private DateTime updateTime;

        public int Id
		{
			get { return id; }
			set { id = value; }
		}
		
        public DateTime CreateTime
        {
			get { return createTime; }
			set { createTime = value; }
		}


		public DateTime UpdateTime
		{
			get { return updateTime; }
			set { updateTime = value; }
		}

	}
}
