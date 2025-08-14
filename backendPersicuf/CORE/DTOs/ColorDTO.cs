using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class ColorDTO
    {
        public string CodigoHexa { get; set; }
        public string Nombre { get; set; }
    }

    public class ColorDTOconID : ColorDTO 
    {
        public int ID { get; set; }
    }
}
