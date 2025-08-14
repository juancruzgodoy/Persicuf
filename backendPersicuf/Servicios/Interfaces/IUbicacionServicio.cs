using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IUbicacionServicio
    {
        Task<Confirmacion<ICollection<UbicacionDTOconID>>> GetUbicacion();
        Task<Confirmacion<UbicacionDTO>> PostUbicacion(UbicacionDTO UbicacionDTO);
        Task<Confirmacion<Ubicacion>> DeleteUbicacion(int ID);
        Task<Confirmacion<UbicacionDTO>> PutUbicacion(int ID, UbicacionDTO UbicacionDTO);
    }
}
