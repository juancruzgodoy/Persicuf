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
    public class ImagenServicio : IImagenServicio
    {
        private readonly PersicufContext _context;
        public ImagenServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Imagen>> DeleteImagen(int ID)
        {

            var respuesta = new Confirmacion<Imagen>();
            respuesta.Datos = null;

            try
            {
                var imagenDB = await _context.Imagenes.FindAsync(ID);
                if (imagenDB != null)
                {
                    _context.Imagenes.Remove(imagenDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = imagenDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La imagen con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro la imagen con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }


        public async Task<Confirmacion<ICollection<ImagenDTOconID>>> GetImagen()
        {
            var respuesta = new Confirmacion<ICollection<ImagenDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var imagenDB = await _context.Imagenes.ToListAsync();
                if (imagenDB.Count() != 0)
                {
                    respuesta.Datos = new List<ImagenDTOconID>();
                    foreach (var Img in imagenDB)
                    {
                        respuesta.Datos.Add(new ImagenDTOconID()
                        {
                            ID = Img.ImagenID,
                            Path = Img.ImagenPath,
                            UbicacionID = Img.UbicacionID ?? 0,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todas los Imagenes";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen Imagenes";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }
        public async Task<Confirmacion<ImagenDTO>> PostImagen(ImagenDTO imagenDTO)
        {
            var respuesta = new Confirmacion<ImagenDTO>();
            respuesta.Datos = null;

            try
            {
                var imagenDB = await _context.Imagenes.AsNoTracking().FirstOrDefaultAsync(x => x.ImagenPath == imagenDTO.Path);
                if (imagenDB == null)
                {
                    var imagenNuevo = imagenDTO.Adapt<Imagen>();
                    imagenNuevo.ImagenPath = imagenDTO.Path;
                    await _context.Imagenes.AddAsync(imagenNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La imagen se creó correctamente.";
                    respuesta.Datos = imagenDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "La imagen ya existe.";
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

        public async Task<Confirmacion<ImagenDTO>> PutImagen(int ID, ImagenDTO imagenDTO)
        {
            var respuesta = new Confirmacion<ImagenDTO>();
            respuesta.Datos = null;

            try
            {
                var imagenBD = await _context.Imagenes.FindAsync(ID);
                if (imagenBD != null)
                {
                    imagenBD.ImagenPath = imagenDTO.Path;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = imagenBD.Adapt<ImagenDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La imagen fué modificada correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "La imagen no existe.";
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