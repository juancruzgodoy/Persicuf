using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class PedidoPrenda
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PPID { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public int PrendaID { get; set; }

        [ForeignKey("PrendaID")]
        public virtual Prenda Prenda { get; set; }


        [Required]
        public int PedidoID { get; set; }

        [ForeignKey("PedidoID")]
        public virtual Pedido Pedido { get; set; }
    }
}
