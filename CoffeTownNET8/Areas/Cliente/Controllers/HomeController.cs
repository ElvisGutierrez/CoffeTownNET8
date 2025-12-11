using CoffeTownNET8.AccesoDatos.Data.Repository.IRepository;
using CoffeTownNET8.Modelos.ViewModels;
using CoffeTownNET8.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Diagnostics;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using CoffeTownNET8.Modelos;

namespace CoffeTownNET8.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly UserManager<AplicationUser> _userManager;

        public HomeController(IContenedorTrabajo contenedorTrabajo, UserManager<AplicationUser> userManager)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _userManager = userManager;
        }

        public IActionResult Index()

        {
            HomeVM homeVm = new HomeVM()
            {
                Sliders = _contenedorTrabajo.Slider.GetAll(),
                ListProductos = _contenedorTrabajo.Producto.GetAll()
            };

            ViewBag.IsHome = true;

            return View(homeVm);
        }

        [HttpGet]
        public IActionResult Detalle(int id)
        {
            var productoDesdeBd = _contenedorTrabajo.Producto.Get(id);
            return View(productoDesdeBd);
        }

        [HttpGet]
        public IActionResult Pedido()
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
        public IActionResult Pedido(PedidoVM pedidoVM)
        {
            var productoFromDb = _contenedorTrabajo.Producto.Get(pedidoVM.Pedido.ProductoId);
            if (ModelState.IsValid)
            {

                var user = _userManager.GetUserAsync(User).Result;
                if (user == null)
                {
                    return Unauthorized();
                }
                var productoDB = _contenedorTrabajo.Producto.Get(pedidoVM.Pedido.ProductoId);
                pedidoVM.Pedido.MontoTotal = (float)(pedidoVM.Pedido.Cantidad * productoFromDb.Precio);
                pedidoVM.Pedido.FechaVenta = DateTime.Now.ToString();
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("CoffeTown", "coffetownsv@outlook.com"));
                message.To.Add(new MailboxAddress(user.Nombre, user.Email));
                message.Subject = "Factura de su Pedido - CoffeTown";

                // Construir el cuerpo del mensaje con los detalles del pedido
                string emailBody = $@"
                    <h1>Factura de su Pedido</h1>
                    <p>Estimado/a {pedidoVM.Pedido.Nombre},</p>
                    <p>Gracias por su compra. A continuación, encontrará los detalles de su pedido:</p>
                    <table border='1' cellpadding='10'>
                        <tr>
                            <th>Producto</th>
                            <th>Cantidad</th>
                            <th>Precio Unitario</th>
                            <th>Total</th>
                        </tr>
                        <tr>
                            <td>{productoDB.Nombre}</td>
                            <td>{pedidoVM.Pedido.Cantidad}</td>
                            <td>{productoDB.Precio}</td>
                            <td>{pedidoVM.Pedido.MontoTotal}</td>
                        </tr>
                    </table>
                    <p>Fecha del Pedido: {pedidoVM.Pedido.FechaVenta}</p>
                    <p>Total a Pagar: {pedidoVM.Pedido.MontoTotal}</p>
                    <p>Saludos,</p>
                    <p>El equipo de CoffeTown</p>";

                message.Body = new TextPart("html")
                {
                    Text = emailBody
                };

                using (var client = new SmtpClient())
                {
                    // Conectar al servidor SMTP de Outlook
                    client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    // Autenticarse con la contraseña específica de la aplicación
                    client.Authenticate("coffetownsv@outlook.com", "dbqajhjrfigmmumz");

                    // Enviar el mensaje
                    client.Send(message);
                    // Desconectarse del servidor
                    client.Disconnect(true);
                }

                
                _contenedorTrabajo.Pedido.Add(pedidoVM.Pedido);
                _contenedorTrabajo.Save();

                TempData["PedidoCreado"] = "Su pedido estara listo en unos momentos";
                return RedirectToAction(nameof(Index));
            }
            pedidoVM.ListaProductos = _contenedorTrabajo.Producto.GetListaProductos();
            return View(pedidoVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}