using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        [Required(ErrorMessage = "Required field")]
        [MinLength(3, ErrorMessage = "Field must contain between 3 to 22 characters")]
        [MaxLength(22, ErrorMessage = "Field must contain between 3 to 22 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required field")]
        public DateTime DateCreate { get; set; }
        
        public virtual List<Subcategory> Subcategories { get; set; }
    }
}
