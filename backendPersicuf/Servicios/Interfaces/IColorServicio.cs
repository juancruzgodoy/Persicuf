using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.DTOs;
using DB.Models;
namespace Servicios.Interfaces
{
    public interface IColorServicio
    {
        Task<Confirmacion<ICollection<ColorDTOconID>>> GetColor();
        Task<Confirmacion<ColorDTO>> PostColor(ColorDTO colorDTO);
        Task<Confirmacion<Color>> DeleteColor(int ID);
        Task<Confirmacion<ColorDTO>> PutColor(int ID, ColorDTO ColorDTO);
        Task<Confirmacion<ColorDTOconID>> BuscarColorPorID(int ID);
    }
}
