using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTOs
{
    public class LoginUsuarioConRolDTO
    {
        public LoginUsuarioDTO LoginUsuario { get; set; }
        public int Id { get; set; }
        public string Rol { get; set; }
        public string Token { get; set; }
    }
}
