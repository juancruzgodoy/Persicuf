using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Zapato : Prenda
    {
        [Required]
        public bool PuntaMetalica { get; set; }

        [Required]
        public int TNID { get; set; }

        [ForeignKey("TNID")]
        public virtual TalleNumerico TalleNumerico { get; set; }

    }
}
