using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs
{
    public class PedidoDTO
    {
        public float PrecioTotal { get; set; }
        public int DomicilioID { get; set; }
        public int UsuarioID { get; set; }
        public int NroSeguimiento { get; set; }

    }

    public class PedidoDTOconID : PedidoDTO 
    {
        public int ID {  get; set; }   
    }
}
