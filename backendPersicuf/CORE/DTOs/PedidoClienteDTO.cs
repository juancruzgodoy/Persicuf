using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CORE.DTOs
{
    public class PedidoClienteDTO
    {
        public string Talle { get; set; }
        public string Manga { get; set; }
        public string NombreEmpresa { get; set; }
        public int UsuarioID { get; set; }
        public DomicilioDTO domicilio { get; set; }
        

    }

    public class PedidoClienteDTOconID : PedidoClienteDTO
    {
        public int ID { get; set; }
    }
}
