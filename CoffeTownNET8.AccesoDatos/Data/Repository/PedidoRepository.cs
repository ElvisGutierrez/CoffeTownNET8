using CoffeTownNET8.AccesoDatos.Data.Repository.IRepository;
using CoffeTownNET8.Data;
using CoffeTownNET8.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeTownNET8.AccesoDatos.Data.Repository
{
    internal class PedidoRepository : Repository<Pedido>,  IPedidoRepository
    {
        private readonly ApplicationDbContext _db;

        public PedidoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaPedidos()
        {
            return _db.Pedido.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            }

            );
        }

        public void Update(Pedido pedido)
        {
            var objDesdeDb = _db.Pedido.FirstOrDefault(s => s.Id == pedido.Id);
            objDesdeDb.Nombre = pedido.Nombre;
            objDesdeDb.ProductoId = pedido.ProductoId;
            objDesdeDb.Cantidad = pedido.Cantidad;
            objDesdeDb.MontoTotal = pedido.MontoTotal;

            _db.SaveChanges();
        }
    }
}
