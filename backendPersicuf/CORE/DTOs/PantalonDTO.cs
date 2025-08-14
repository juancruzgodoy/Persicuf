using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class PantalonDTO : PrendaDTO
    {
        public int TalleAlfabeticoID { get; set; }
        public int LargoID { get; set; }

    }
    public class PantalonDTOconID : PantalonDTO
    {
        public int ID { get; set; }
    }
}
