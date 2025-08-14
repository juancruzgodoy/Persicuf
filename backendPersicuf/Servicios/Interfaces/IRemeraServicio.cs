using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IRemeraServicio
    {
        Task<Confirmacion<ICollection<RemeraDTOconID>>> GetRemera();
        Task<Confirmacion<RemeraDTO>> PostRemera(RemeraDTO remeraDTO);
        Task<Confirmacion<Remera>> DeleteRemera(int ID);
        Task<Confirmacion<RemeraDTO>> PutRemera(int ID, RemeraDTO remeraDTO);
        Task<Confirmacion<ICollection<RemeraDTOconID>>> BuscarRemeras(string busqueda);
    }
}
