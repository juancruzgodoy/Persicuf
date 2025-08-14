using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Servicios.Interfaces
{
    public interface IJWT
    {
        public string GenerarToken(Usuario user);
        //Task<RespuestaPrivada<dynamic>> validarToken(ClaimsIdentity identity);
    }
}
