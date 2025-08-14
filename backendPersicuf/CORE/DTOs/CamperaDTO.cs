using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class CamperaDTO : PrendaDTO
    {
        public int TalleAlfabeticoID { get; set; }
    }
    public class CamperaDTOconID : CamperaDTO
    {
        public int ID { get; set; }
    }
}
