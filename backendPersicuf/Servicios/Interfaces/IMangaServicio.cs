using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IMangaServicio
    {
        Task<Confirmacion<ICollection<MangaDTOconID>>> GetManga();
        Task<Confirmacion<MangaDTO>> PostManga(MangaDTO MangaDTO);
        Task<Confirmacion<Manga>> DeleteManga(int ID);
        Task<Confirmacion<MangaDTO>> PutManga(int ID, MangaDTO MangaDTO);
    }
}
