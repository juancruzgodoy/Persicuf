using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface ICorteCuelloServicio
    {
        Task<Confirmacion<ICollection<CorteCuelloDTOconID>>> GetCorteCuello();
        Task<Confirmacion<CorteCuelloDTO>> PostCorteCuello(CorteCuelloDTO CorteCuelloDTO);
        Task<Confirmacion<CorteCuello>> DeleteCorteCuello(int ID);
        Task<Confirmacion<CorteCuelloDTO>> PutCorteCuello(int ID, CorteCuelloDTO CorteCuelloDTO);
    }
}
