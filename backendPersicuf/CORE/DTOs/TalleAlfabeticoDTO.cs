using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class TalleAlfabeticoDTO
    {
        public string Descripcion { get; set; }
    }
    public class TalleAlfabeticoDTOconID : TalleAlfabeticoDTO { 
    public int ID { get; set; }
    }
}
