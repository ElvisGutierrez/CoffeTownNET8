using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeTownNET8.Modelos.ViewModels
{
    public class ProductoVM
    {

        public Producto Producto { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
