using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IRubroServicio
    {
        Task<Confirmacion<ICollection<RubroDTOconID>>> GetRubro();
        Task<Confirmacion<RubroDTO>> PostRubro(RubroDTO RubroDTO);
        Task<Confirmacion<Rubro>> DeleteRubro(int ID);
        Task<Confirmacion<RubroDTO>> PutRubro(int ID, RubroDTO RubroDTO);
        Task<Confirmacion<RubroDTOconID>> BuscarRubroPorID(int ID);
    }
}
