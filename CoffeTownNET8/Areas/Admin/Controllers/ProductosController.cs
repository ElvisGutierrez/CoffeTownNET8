using CoffeTownNET8.AccesoDatos.Data.Repository.IRepository;
using CoffeTownNET8.Modelos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeTownNET8.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]

    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new CoffeTownNET8.Modelos.Producto(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (productoVM.Producto.Id == 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"img\productos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    productoVM.Producto.UrlImagen = @"\img\productos\" + nombreArchivo + extension;
                    _contenedorTrabajo.Producto.Add(productoVM.Producto);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
            }
            productoVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();
            return View(productoVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ProductoVM produVM = new ProductoVM()
            {
                Producto = new Modelos.Producto(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias(),

            };

            if (id != null)
            {
                produVM.Producto = _contenedorTrabajo.Producto.Get(id.GetValueOrDefault());
            }
            return View(produVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductoVM produVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var productoDB = _contenedorTrabajo.Producto.Get(produVM.Producto.Id);

                if (archivos.Count() > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"img\productos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, productoDB.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }


                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    produVM.Producto.UrlImagen = @"\img\productos\" + nombreArchivo + extension;

                    _contenedorTrabajo.Producto.Update(produVM.Producto);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    produVM.Producto.UrlImagen = productoDB.UrlImagen;
                }
                _contenedorTrabajo.Producto.Update(produVM.Producto);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            produVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();

            return View(produVM);
        }



        #region LLAMADAS A LA API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Producto.GetAll(includeProperties: "Categoria") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productoDesdeBd = _contenedorTrabajo.Producto.Get(id);
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, productoDesdeBd.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }


            if (productoDesdeBd == null)
            {
                return Json(new { success = false, message = "Error borrando producto" });
            }

            _contenedorTrabajo.Producto.Remove(productoDesdeBd);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Producto Borrado Correctamente" });
        }

        #endregion

    }
}
