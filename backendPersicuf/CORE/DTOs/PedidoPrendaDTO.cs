using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CORE.DTOs
{
    public class PedidoPrendaDTO
    {
        public int Cantidad { get; set; }
        public int PrendaID { get; set; }
        public int PedidoID { get; set; }

    }
    public class PedidoPrendaDTOconID : PedidoPrendaDTO
    {
        public int ID
{
            get; set;
        }
    }
}


