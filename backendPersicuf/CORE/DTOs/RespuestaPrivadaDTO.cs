using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTOs
{
    public class RespuestaPrivada<T>
    {
        public T Datos { get; set; }
        public bool Exito { get; set; } = false;
        public string Mensaje { get; set; } = "";
    }
}
