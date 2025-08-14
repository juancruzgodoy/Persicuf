using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class Confirmacion<T>
    {
        public T Datos {  get; set; }
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = "";
    }
}
