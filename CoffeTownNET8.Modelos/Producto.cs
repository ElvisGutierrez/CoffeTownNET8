using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeTownNET8.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [Display(Name = "Nombre del producto")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del producto es obligatoria")]
        [Display(Name = "Descripción del producto")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio del producto es obligatorio")]
        [Display(Name = "Precio del producto")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        
        [Display(Name = "Imagen del producto")]
        [DataType(DataType.ImageUrl)]
        public string UrlImagen { get; set; }

        [Required(ErrorMessage = "La categoría del producto es obligatoria")]
        [Display(Name = "Categoría del producto")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }
}
