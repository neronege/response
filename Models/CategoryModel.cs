using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewNewProject.Models
{
    public class CategoryModel
    {
        [DisplayName("Başlık"), Required(ErrorMessage ="{0} boş geçilmez!"), MaxLength(10, ErrorMessage= " En fazla {1} karakter")]
        public string Title { get; set; }
        [DisplayName("Açıklama"), Required(ErrorMessage ="{0} Boş geçilmez"), MinLength(5, ErrorMessage = " En fazla {1} karakter")]
        public string Description { get; set; }
        [Range(10,100, ErrorMessage ="En az {1} ile {2} arası rakam girilmelidir.")]
        public int Hit { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Password tekrarı password ile aynı değil")]
        public string RePassword { get; set; }
        public int Id { get; internal set; }
    }
}
