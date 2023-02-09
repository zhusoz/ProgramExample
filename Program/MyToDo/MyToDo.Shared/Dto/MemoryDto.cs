﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dto
{
    public class MemoryDto : BaseEntityDto
    {

        private string title;
        private string content;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }


    }
}
