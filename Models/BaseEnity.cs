using System;

namespace NewNewProject.Models
{
    public abstract class BaseEnity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
