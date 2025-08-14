using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Domicilio
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DomicilioID { get; set; }

        [Required]
        public string Calle { get; set; }

        [Required]
        public int Numero { get; set; }

        public int Piso { get; set; }

        public string Depto { get; set; }

        public string Descripcion { get; set; }

        [Required]
        public int UsuarioID { get; set; }

        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }

        [Required]
        public int LocalidadID { get; set; }

        [ForeignKey("LocalidadID")]
        public virtual Localidad Localidad { get; set; }
    }
}
