using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dto
{
    public class BaseEntityDto : BaseDto
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private DateTime createTime = DateTime.Now;
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; OnPropertyChanged(); }
        }
    }
}
