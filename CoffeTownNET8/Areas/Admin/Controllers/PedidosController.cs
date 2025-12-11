using CoffeTownNET8.AccesoDatos.Data.Repository.IRepository;
using CoffeTownNET8.Modelos;
using CoffeTownNET8.Modelos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeTownNET8.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Area("Admin")]
    public class PedidosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public PedidosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            PedidoVM pedidoVM = new PedidoVM()
            {
                Pedido = new CoffeTownNET8.Modelos.Pedido(),
                ListaProductos = _contenedorTrabajo.Producto.GetListaProductos()
            };

            return View(pedidoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PedidoVM pedidoVM)
        {
            var productoFromDb = _contenedorTrabajo.Producto.Get(pedidoVM.Pedido.ProductoId);
            if (ModelState.IsValid)
            {
                pedidoVM.Pedido.MontoTotal = (float)(pedidoVM.Pedido.Cantidad * productoFromDb.Precio);

                pedidoVM.Pedido.FechaVenta = DateTime.Now.ToString();
                _contenedorTrabajo.Pedido.Add(pedidoVM.Pedido);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            pedidoVM.ListaProductos = _contenedorTrabajo.Producto.GetListaProductos();
            return View(pedidoVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            PedidoVM pedidoVM = new PedidoVM()
            {
                Pedido = new Modelos.Pedido(),
                ListaProductos = _contenedorTrabajo.Producto.GetListaProductos(),

            };

            if (id != null)
            {
                pedidoVM.Pedido = _contenedorTrabajo.Pedido.Get(id.GetValueOrDefault());
            }

            return View(pedidoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PedidoVM pedidoVM)
        {
            if (ModelState.IsValid)
            {
                var productoFromDb = _contenedorTrabajo.Producto.Get(pedidoVM.Pedido.ProductoId);
                if (ModelState.IsValid)
                {
                    pedidoVM.Pedido.MontoTotal = (float)(pedidoVM.Pedido.Cantidad * productoFromDb.Precio);

                    _contenedorTrabajo.Pedido.Update(pedidoVM.Pedido);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                pedidoVM.ListaProductos = _contenedorTrabajo.Producto.GetListaProductos();
                return View(pedidoVM);
            }
            pedidoVM.ListaProductos = _contenedorTrabajo.Producto.GetListaProductos();
            return View(pedidoVM);
        }


        #region LLAMADAS A LA API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Pedido.GetAll(includeProperties: "Producto") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Pedido.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al borrar el pedido" });
            }
            _contenedorTrabajo.Pedido.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Pedido borrado con éxito" });
        }

        #endregion
    }
}
