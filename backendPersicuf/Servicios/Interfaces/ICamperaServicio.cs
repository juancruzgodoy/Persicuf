using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface ICamperaServicio
    {
        Task<Confirmacion<ICollection<CamperaDTOconID>>> GetCampera();
        Task<Confirmacion<CamperaDTO>> PostCampera(CamperaDTO camperaDTO);
        Task<Confirmacion<Campera>> DeleteCampera(int ID);
        Task<Confirmacion<CamperaDTO>> PutCampera(int ID, CamperaDTO camperaDTO);
        Task<Confirmacion<ICollection<CamperaDTOconID>>> BuscarCamperas(string busqueda);
    }
}
