using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeTownNET8.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es obligatorio!")]
        [RegularExpression(@"^[A-Za-záéíóúÁÉÍÓÚñÑ\s]*$", ErrorMessage = "Solo se permiten letras y espacios.")]
        [Display(Name ="Tipo de café")]
        public string Nombre { get; set; }
        [Display(Name = "Orden de visualización")]
        public int? Orden { get; set; }
    }
}
