using CoffeTownNET8.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeTownNET8.AccesoDatos.Data.Repository.IRepository
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        IEnumerable<SelectListItem> GetListaPedidos();

        void Update(Pedido pedido);
    }
}
