using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class LocalidadDTO
    {
        public string Nombre { get; set; }
        public int ProvinciaID { get; set; }

    }
    public class LocalidadDTOconID : LocalidadDTO 
    { 
    public int ID { get; set; }
    }
}
