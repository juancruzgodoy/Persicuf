using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IDomicilioServicio
    {
        Task<Confirmacion<ICollection<DomicilioDTOconID>>> GetDomicilio();
        Task<Confirmacion<DomicilioDTO>> PostDomicilio(DomicilioDTO DomicilioDTO);
        Task<Confirmacion<Domicilio>> DeleteDomicilio(int ID);
        Task<Confirmacion<DomicilioDTO>> PutDomicilio(int ID, DomicilioDTO DomicilioDTO);
    }
}
