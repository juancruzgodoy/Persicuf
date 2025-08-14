using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Localidad
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocalidadID { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int ProvinciaID { get; set; }

        [ForeignKey("ProvinciaID")]
        public virtual Provincia Provincia { get; set; }
    }
}
