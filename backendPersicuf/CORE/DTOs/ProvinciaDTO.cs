using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class ProvinciaDTO
    {
        public string Nombre { get; set; }
    }

    public class ProvinciaDTOconID : ProvinciaDTO 
    { 
    public int ID { get; set; }
    }
}
