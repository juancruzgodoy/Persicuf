using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IPrendaServicio
    {
        Task<Confirmacion<ICollection<PrendaDTOconID>>> GetPrenda();
        Task<Confirmacion<PrendaDTO>> PostPrenda(PrendaDTO prendaDTO);
        Task<Confirmacion<Prenda>> DeletePrenda(int ID);
        Task<Confirmacion<PrendaDTO>> PutPrenda(int ID, PrendaDTO prendaDTO);
        Task<Confirmacion<ICollection<PrendaDTOconID>>> BuscarPrenda(string busqueda);
        Task<Confirmacion<ICollection<PrendaDTOconID>>> GetPrendaUsuario(int ID);
        Task<Confirmacion<PrendaDTOconID>> BuscarPrendaPorID(int ID);
    }
}
