using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeTownNET8.Modelos
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }

        public string? FechaVenta { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
        [Display(Name = "Nombre del cliente")]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [Display(Name = "Nombre del producto")]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "La cantidad del producto es obligatoria")]
        [Display(Name = "Cantidad del producto")]
        [Range(1, 30, ErrorMessage = "La cantidad maxíma es de 30")]
        public int Cantidad { get; set; }

        [Display(Name = "Monto total")]
        public float MontoTotal { get; set; }
    }
}
