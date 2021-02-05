using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Subcategory
    {
        [Key]
        public int IdSubcategory { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Field must contain between 6 to 32 characters")]
        [MaxLength(32, ErrorMessage = "Field must contain between 6 to 32 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required field")]
        public int IdCategory { get; set; }
        [Required(ErrorMessage = "Required field")]
        public DateTime DateCreate { get; set; }
        
        [ForeignKey("IdCategory")]
        public virtual Category Category { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
