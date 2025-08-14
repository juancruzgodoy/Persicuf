using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IPedidoPrendaServicio
    {
        Task<Confirmacion<ICollection<PedidoPrendaDTOconID>>> GetPedidoPrenda();
        Task<Confirmacion<PedidoPrendaDTO>> PostPedidoPrenda(PedidoPrendaDTO PedidoPrendaDTO);
        Task<Confirmacion<PedidoPrenda>> DeletePedidoPrenda(int ID);
        Task<Confirmacion<PedidoPrendaDTO>> PutPedidoPrenda(int ID, PedidoPrendaDTO PedidoPrendaDTO);
        Task<Confirmacion<string>> PostPedidoCliente(PedidoClienteDTO pedidoClienteDTO);
    }
}
