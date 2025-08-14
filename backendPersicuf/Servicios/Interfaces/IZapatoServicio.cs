using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IZapatoServicio
    {
        Task<Confirmacion<ICollection<ZapatoDTOconID>>> GetZapato();
        Task<Confirmacion<ZapatoDTO>> PostZapato(ZapatoDTO zapatoDTO);
        Task<Confirmacion<Zapato>> DeleteZapato(int ID);
        Task<Confirmacion<ZapatoDTO>> PutZapato(int ID, ZapatoDTO zapatoDTO);
        Task<Confirmacion<ICollection<ZapatoDTOconID>>> BuscarZapatos(string busqueda);
    }
}
