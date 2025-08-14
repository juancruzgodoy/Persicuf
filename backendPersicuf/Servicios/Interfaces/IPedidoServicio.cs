using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IPedidoServicio
    {
        Task<Confirmacion<ICollection<PedidoDTOconID>>> GetPedido();
        Task<Confirmacion<PedidoDTO>> PostPedido(PedidoDTO PedidoDTO);
        Task<Confirmacion<Pedido>> DeletePedido(int ID);
        Task<Confirmacion<PedidoDTO>> PutPedido(int ID, PedidoDTO PedidoDTO);
        Task<Confirmacion<ICollection<PedidoDTOconID>>> GetPedidoUsuario(int ID);
    }
}
