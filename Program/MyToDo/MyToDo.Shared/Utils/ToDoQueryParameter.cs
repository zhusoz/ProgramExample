using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Utils
{
    public class ToDoQueryParameter : PageQueryParameter
    {
        public int? Status { get; set; }
    }
}
