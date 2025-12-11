using CoffeTownNET8.AccesoDatos.Data.Repository.IRepository;
using CoffeTownNET8.Data;
using CoffeTownNET8.Modelos;
using CoffeTownNET8.Modelos.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeTownNET8.AccesoDatos.Data.Repository
{
    internal class VentaRepository : Repository<Venta>, IVentaRepository
    {
        private readonly ApplicationDbContext _db;

        public VentaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaVentas()
        {
            return _db.Venta.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            }

            );
        }

        public void Update(Venta venta)
        {
            var objDesdeDb = _db.Venta.FirstOrDefault(s => s.Id == venta.Id);
            objDesdeDb.Nombre = venta.Nombre;
            objDesdeDb.ProductoId = venta.ProductoId;
            objDesdeDb.Cantidad = venta.Cantidad;
            objDesdeDb.MontoTotal = venta.MontoTotal;

            _db.SaveChanges();
        }
    }
}
