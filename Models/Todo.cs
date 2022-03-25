using System.ComponentModel.DataAnnotations;

namespace NewNewProject.Models
{
    public class Todo : BaseEnity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
