using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class DomicilioDTO
    {
        public string Calle {  get; set; }
        public int Numero { get; set; }
        public int Piso { get; set; }
        public string Depto { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioID { get; set; }
        public int LocalidadID { get; set; }

    }

    public class DomicilioDTOconID : DomicilioDTO
    { 
    public int ID { get; set; }
    }
}
