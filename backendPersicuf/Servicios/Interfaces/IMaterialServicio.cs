using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IMaterialServicio
    {
        Task<Confirmacion<ICollection<MaterialDTOconID>>> GetMaterial();
        Task<Confirmacion<MaterialDTO>> PostMaterial(MaterialDTO materialDTO);
        Task<Confirmacion<Material>> DeleteMaterial(int ID);
        Task<Confirmacion<MaterialDTO>> PutMaterial(int ID, MaterialDTO materialDTO);
        Task<Confirmacion<MaterialDTOconID>> BuscarMaterialPorID(int ID);
    }
}
