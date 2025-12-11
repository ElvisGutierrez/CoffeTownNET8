using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeTownNET8.Modelos.ViewModels
{
    public class PedidoVM
    {
        public Pedido Pedido { get; set; }
        public IEnumerable<SelectListItem> ListaProductos { get; set; }
    }
}
