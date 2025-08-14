using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTOs
{
    public class EnvioDTO
    {
        public string descripcion { get; set; }
        public string hora { get; set; }
        public int pesoGramos { get; set; }
        public bool reserva { get; set; }
        public Direccion origen { get; set; }
        public Direccion destino { get; set; }
        public string cliente { get; set; }
    }

    public class Direccion
    {
        public string calle { get; set; }
        public int numero { get; set; }
        public int piso { get; set; }
        public string depto { get; set; }
        public string descripcion { get; set; }
        public int localidadID { get; set; }
    }
}

