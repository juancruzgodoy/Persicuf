using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CORE.DTOs
{
    public class TalleNumericoDTO
    {
        public string Descripcion {  get; set; }
    }
    public class TalleNumericoDTOconID : TalleNumericoDTO
    {
        public int ID { get; set; }
    }
}
