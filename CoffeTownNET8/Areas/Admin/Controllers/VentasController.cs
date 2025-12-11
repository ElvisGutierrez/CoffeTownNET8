using CoffeTownNET8.AccesoDatos.Data.Repository.IRepository;
using CoffeTownNET8.Modelos;
using CoffeTownNET8.Modelos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CoffeTownNET8.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Area("Admin")]
    public class VentasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly UserManager<AplicationUser> _userManager;

        public VentasController(IContenedorTrabajo contenedorTrabajo, UserManager<AplicationUser> userManager)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(int? id)
        {
            PedidoVM pedidoVM = new PedidoVM()
            {
                Pedido = new CoffeTownNET8.Modelos.Pedido(),
                ListaProductos = _contenedorTrabajo.Producto.GetListaProductos()
            };

            if (id != null)
            {
                pedidoVM.Pedido = _contenedorTrabajo.Pedido.Get(id.GetValueOrDefault());
            }

            return View(pedidoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PedidoVM pedidoVM)
        {
            if (ModelState.IsValid)
            {

                // Mapear los datos de PedidoVM a Venta
                Venta venta = new Venta
                {
                    FechaVenta = pedidoVM.Pedido.FechaVenta,
                    Nombre = pedidoVM.Pedido.Nombre,
                    ProductoId = pedidoVM.Pedido.ProductoId,
                    Cantidad = pedidoVM.Pedido.Cantidad,
                    MontoTotal = pedidoVM.Pedido.MontoTotal
                };

                _contenedorTrabajo.Venta.Add(venta);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            pedidoVM.ListaProductos = _contenedorTrabajo.Producto.GetListaProductos();
            return View(pedidoVM);
        }

        #region LLAMADAS A LA API

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Venta.GetAll(includeProperties: "Producto") });
        }

        #endregion
    }
}
