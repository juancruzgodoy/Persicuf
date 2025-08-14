using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IPantalonServicio
    {
        Task<Confirmacion<ICollection<PantalonDTOconID>>> GetPantalon();
        Task<Confirmacion<PantalonDTO>> PostPantalon(PantalonDTO pantalonDTO);
        Task<Confirmacion<Pantalon>> DeletePantalon(int ID);
        Task<Confirmacion<PantalonDTO>> PutPantalon(int ID, PantalonDTO pantalonDTO);
        Task<Confirmacion<ICollection<PantalonDTOconID>>> BuscarPantalones(string busqueda);
    }
}
