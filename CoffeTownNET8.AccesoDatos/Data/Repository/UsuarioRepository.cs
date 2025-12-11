using CoffeTownNET8.AccesoDatos.Data.Repository.IRepository;
using CoffeTownNET8.Data;
using CoffeTownNET8.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeTownNET8.AccesoDatos.Data.Repository
{
    public class UsuarioRepository : Repository<AplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeBd = _db.AplicationUser.FirstOrDefault(u => u.Id ==  IdUsuario);
            usuarioDesdeBd.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeBd = _db.AplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeBd.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
