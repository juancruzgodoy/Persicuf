using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class UsuarioDTO
    {
        public string NombreUsuario { get; set; }

        public string Contrasenia { get; set; }

        public string Correo {  get; set; }

        public int PermisoID { get; set; }

    }

    public class UsuarioDTOconID : UsuarioDTO
    { 
        public int ID { get; set; } 
    }
}
