using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface ILargoServicio
    {
        Task<Confirmacion<ICollection<LargoDTOconID>>> GetLargo();
        Task<Confirmacion<LargoDTO>> PostLargo(LargoDTO LargoDTO);
        Task<Confirmacion<Largo>> DeleteLargo(int ID);
        Task<Confirmacion<LargoDTO>> PutLargo(int ID, LargoDTO LargoDTO);
    }
}
