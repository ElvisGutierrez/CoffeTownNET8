using CoffeTownNET8.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeTownNET8.AccesoDatos.Data.Repository.IRepository
{
    public interface IUsuarioRepository : IRepository<AplicationUser>
    {
        void BloquearUsuario(string IdUsuario);
        void DesbloquearUsuario(string IdUsuario);
    }
}
