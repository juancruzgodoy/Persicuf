using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface ITalleAlfabeticoServicio
    {
        Task<Confirmacion<ICollection<TalleAlfabeticoDTOconID>>> GetTalleAlfabetico();
        Task<Confirmacion<TalleAlfabeticoDTO>> PostTalleAlfabetico(TalleAlfabeticoDTO TalleAlfabeticoDTO);
        Task<Confirmacion<TalleAlfabetico>> DeleteTalleAlfabetico(int ID);
        Task<Confirmacion<TalleAlfabeticoDTO>> PutTalleAlfabetico(int ID, TalleAlfabeticoDTO TalleAlfabeticoDTO);
    }
}
