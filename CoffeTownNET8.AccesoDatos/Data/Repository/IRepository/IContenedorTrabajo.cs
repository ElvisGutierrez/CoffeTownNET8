using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeTownNET8.AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        ICategoriaRepository Categoria { get; }

        IProductoRepository Producto { get; }
        ISliderRepository Slider { get; }

        IPedidoRepository Pedido { get; }

        IVentaRepository Venta { get; }

        IUsuarioRepository Usuario { get; }

        void Save();
    }
}
