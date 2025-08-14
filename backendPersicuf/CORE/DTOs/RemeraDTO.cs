using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class RemeraDTO : PrendaDTO
    {
        public int TalleAlfabeticoID { get; set; }
        public int CorteCuelloID { get; set; }
        public int MangaID { get; set; }

    }
    public class RemeraDTOconID : RemeraDTO
    {
        public int ID { get; set; }
    }
}
