using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class ZapatoDTO : PrendaDTO
    {
        public bool PuntaMetal { get; set; }
        public int TalleNumericoID { get; set; }
    }
    public class ZapatoDTOconID : ZapatoDTO
    {
        public int ID { get; set; }
    }
}
