using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class MaterialDTO
    {
        public string Descripcion { get; set; }

        public float Precio { get; set; }
    }

    public class MaterialDTOconID : MaterialDTO
    {
        public int ID { get; set; }
    }
}
