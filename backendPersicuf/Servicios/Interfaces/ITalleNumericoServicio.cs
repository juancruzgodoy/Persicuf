using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface ITalleNumericoServicio
    {
        Task<Confirmacion<ICollection<TalleNumericoDTOconID>>> GetTalleNumerico();
        Task<Confirmacion<TalleNumericoDTO>> PostTalleNumerico(TalleNumericoDTO TalleNumericoDTO);
        Task<Confirmacion<TalleNumerico>> DeleteTalleNumerico(int ID);
        Task<Confirmacion<TalleNumericoDTO>> PutTalleNumerico(int ID, TalleNumericoDTO TalleNumericoDTO);
    }
}
