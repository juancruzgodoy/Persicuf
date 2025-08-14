using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface ILocalidadServicio
    {
        Task<Confirmacion<ICollection<LocalidadDTOconID>>> GetLocalidad();
        Task<Confirmacion<LocalidadDTO>> PostLocalidad(LocalidadDTO LocalidadDTO);
        Task<Confirmacion<Localidad>> DeleteLocalidad(int ID);
        Task<Confirmacion<LocalidadDTO>> PutLocalidad(int ID, LocalidadDTO LocalidadDTO);
    }
}
