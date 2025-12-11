using CoffeTownNET8.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeTownNET8.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categoria { get; set; }

        public DbSet<Producto> Producto { get; set; }

        public DbSet<Slider> Slider { get; set; }

        public DbSet<AplicationUser> AplicationUser { get; set; }

        public DbSet<Pedido> Pedido { get; set; }

        public DbSet<Venta> Venta { get; set; }
    }
}
