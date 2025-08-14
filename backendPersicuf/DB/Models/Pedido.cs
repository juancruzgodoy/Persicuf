using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Pedido
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PedidoID { get; set; }

        [Required]
        public float PrecioTotal { get; set; }

        [Required]
        public int NroSeguimiento { get; set; }

        [Required]
        public int DomicilioID { get; set; }

        [ForeignKey("DomicilioID")]
        public virtual Domicilio Domicilio { get; set; }

        [Required]
        public int UsuarioID { get; set; }

        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }

    }
}
