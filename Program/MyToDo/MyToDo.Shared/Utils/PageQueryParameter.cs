﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Utils
{
    public class PageQueryParameter
    {
        public int PageIndex { get; set; }
        
        public int PageSize { get; set; }
        
        public string? Keyword { get; set; }

    }
}
