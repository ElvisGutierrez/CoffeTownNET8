using CoffeTownNET8.AccesoDatos.Data.Repository.IRepository;
using CoffeTownNET8.Modelos.ViewModels;
using CoffeTownNET8.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using CoffeTownNET8.Areas.Admin.Controllers;
using Microsoft.EntityFrameworkCore.Metadata;
using CoffeTownNET8.Modelos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoffeTownNET8.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PDFController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public PDFController(IWebHostEnvironment host, IContenedorTrabajo contenedorTrabajo)
        {
            _host = host;
            _contenedorTrabajo = contenedorTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ventaPDF()
        {
            var venta = _contenedorTrabajo.Venta.GetAll(includeProperties: "Producto"); // Obtener la lista de empleados desde el repositorio

            
            var PDF = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(30);

                    page.Header().ShowOnce().Row(row =>
                    {
                        var rutaImagen = Path.Combine(_host.WebRootPath, "img/Logo_CoffeeTown.png");
                        byte[] imageData = System.IO.File.ReadAllBytes(rutaImagen);

                        row.ConstantItem(80).Image(imageData);

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("Coffe Town").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("col. Las Mercedes").FontSize(9);
                            col.Item().AlignCenter().Text("79245635").FontSize(9);
                            col.Item().AlignCenter().Text("coffetown@gmail.com").FontSize(9);
                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Border(1).BorderColor("#895d58")
                            .AlignCenter().Text("RUC 21312312312");

                            col.Item().Border(1).Background("#895d58").BorderColor("#895d58")
                            .AlignCenter().Text("Boleta de Ventas").FontColor("#ffff");

                            col.Item().Border(1).BorderColor("#895d58")
                            .AlignCenter().Text("B001-234");
                        });
                    });

                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        col1.Item().Column(col2 =>
                        {
                            col2.Item().Text("Datos Sucursal").Underline().Bold();

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Sucursal: ").SemiBold().FontSize(10);
                                txt.Span("Ilobasco").FontSize(10);

                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Direccion: ").SemiBold().FontSize(10);
                                txt.Span("col. Las Mercedes").FontSize(10);
                            });
                        });

                        col1.Item().LineHorizontal(0.5f);

                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#895d58")
                                .Padding(2).Text("Producto").FontColor("#fff");

                                header.Cell().Background("#895d58")
                                .Padding(2).Text("Cantidad").FontColor("#fff");

                                header.Cell().Background("#895d58")
                                .Padding(2).Text("Cliente").FontColor("#fff");

                                header.Cell().Background("#895d58")
                                .Padding(2).Text("Fecha").FontColor("#fff");

                                header.Cell().Background("#895d58")
                                .Padding(2).Text("Total").FontColor("#fff");

                            });

                            foreach (var item in venta)
                            {

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(item.Producto.Nombre).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(item.Cantidad).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(item.Nombre).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(item.FechaVenta).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text($"${item.MontoTotal}").FontSize(10);

                            };
                        });
                        //var totalPrice = Model.Items.Sum(x => x.Price * x.Quantity);
                        //col1.Item().AlignRight().Text($"total: {totalPrice}").FontSize(12);

                        col1.Spacing(10);
                    });

                    page.Footer().AlignRight().Text(txt =>
                    {
                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);


                    });
                });
            }).GeneratePdf();
            
            Stream stream = new MemoryStream(PDF);
            return File(stream, "application/pdf", "reporteVenta.pdf");

            return View();
        }
    }
}
