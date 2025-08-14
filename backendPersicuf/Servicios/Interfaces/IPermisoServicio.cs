using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IPermisoServicio
    {
        Task<Confirmacion<ICollection<PermisoDTOconID>>> GetPermiso();
        Task<Confirmacion<PermisoDTO>> PostPermiso(PermisoDTO PermisoDTO);
        Task<Confirmacion<Permiso>> DeletePermiso(int ID);
        Task<Confirmacion<PermisoDTO>> PutPermiso(int ID, PermisoDTO PermisoDTO);
    }
}
