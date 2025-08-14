using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IProvinciaServicio
    {
        Task<Confirmacion<ICollection<ProvinciaDTOconID>>> GetProvincia();
        Task<Confirmacion<ProvinciaDTO>> PostProvincia(ProvinciaDTO ProvinciaDTO);
        Task<Confirmacion<Provincia>> DeleteProvincia(int ID);
        Task<Confirmacion<ProvinciaDTO>> PutProvincia(int ID, ProvinciaDTO ProvinciaDTO);
    }
}
