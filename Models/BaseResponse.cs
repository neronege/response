using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewNewProject.Models
{
    public abstract class BaseResponse
    {
        public List<string> Errors { get; set; }

        public bool HasError { get; set; }

        public bool IsSuccessFull { get; set; }
    }
   
}
