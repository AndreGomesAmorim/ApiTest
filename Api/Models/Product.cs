using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }
        [Required(ErrorMessage = "Required field")]
        [MaxLength(100, ErrorMessage = "Field must contain a maximum of 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required field")]
        public double Width { get; set; }
        [Required(ErrorMessage = "Required field")]
        public double Height { get; set; }
        [Required(ErrorMessage = "Required field")]
        public double Length { get; set; }
        [Required(ErrorMessage = "Required field")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Required field")]
        public decimal Discount { get; set; }
        [Required(ErrorMessage = "Required field")]
        public int IdSubcategory { get; set; }
        [Required(ErrorMessage = "Required field")]
        public DateTime DateCreate { get; set; }

        [ForeignKey("IdSubcategory")]
        public virtual Subcategory Subcategory { get; set; }
    }
}
