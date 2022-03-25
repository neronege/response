using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewNewProject.Models
{
    public class Category : BaseEnity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get;  set; }
        public string Description { get; set; }

       
        public List<Todo> Todos { get; set; }
    }
}
