using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IImagenServicio
    {
        Task<Confirmacion<ICollection<ImagenDTOconID>>> GetImagen();
        Task<Confirmacion<ImagenDTO>> PostImagen(ImagenDTO ImagenDTO);
        Task<Confirmacion<Imagen>> DeleteImagen(int ID);
        Task<Confirmacion<ImagenDTO>> PutImagen(int ID, ImagenDTO ImagenDTO);
    }
}
