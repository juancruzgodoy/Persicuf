using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using DB.Models;
using DB.Data;
using CORE.DTOs;
using Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Servicios.Servicios
{
    public class MangaServicio : IMangaServicio
    {
        private readonly PersicufContext _context;
        public MangaServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Manga>> DeleteManga(int ID)
        {

            var respuesta = new Confirmacion<Manga>();
            respuesta.Datos = null;

            try
            {
                var mangaDB = await _context.Mangas.FindAsync(ID);
                if (mangaDB != null)
                {
                    _context.Mangas.Remove(mangaDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = mangaDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La manga con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro la manga con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }


        public async Task<Confirmacion<ICollection<MangaDTOconID>>> GetManga()
        {
            var respuesta = new Confirmacion<ICollection<MangaDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var mangaDB = await _context.Mangas.ToListAsync();
                if (mangaDB.Count() != 0)
                {
                    respuesta.Datos = new List<MangaDTOconID>();
                    foreach (var Manga in mangaDB)
                    {
                        respuesta.Datos.Add(new MangaDTOconID()
                        {
                            ID = Manga.MangaID,
                            Descripcion = Manga.Descripcion,
                            Precio = Manga.Precio,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todas los Mangas";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen Mangas";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }
        public async Task<Confirmacion<MangaDTO>> PostManga(MangaDTO mangaDTO)
        {
            var respuesta = new Confirmacion<MangaDTO>();
            respuesta.Datos = null;

            try
            {
                var mangaDB = await _context.Mangas.AsNoTracking().FirstOrDefaultAsync(x => x.Descripcion == mangaDTO.Descripcion);
                if (mangaDB == null)
                {
                    var mangaNuevo = mangaDTO.Adapt<Manga>();
                    await _context.Mangas.AddAsync(mangaNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La manga se creó correctamente.";
                    respuesta.Datos = mangaDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "La manga ya existe.";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    respuesta.Mensaje += " Inner Exception: " + ex.InnerException.Message;
                }
                return (respuesta);
            }
        }

        public async Task<Confirmacion<MangaDTO>> PutManga(int ID, MangaDTO mangaDTO)
        {
            var respuesta = new Confirmacion<MangaDTO>();
            respuesta.Datos = null;

            try
            {
                var mangaBD = await _context.Mangas.FindAsync(ID);
                if (mangaBD != null)
                {
                    mangaBD.Descripcion = mangaDTO.Descripcion;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = mangaBD.Adapt<MangaDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La manga fué modificada correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "La manga no existe.";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return respuesta;
            }
        }
    }
}
