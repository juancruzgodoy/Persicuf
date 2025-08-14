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
    public class ColorServicio : IColorServicio
    {
        private readonly PersicufContext _context;
        public ColorServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<DB.Models.Color>> DeleteColor(int ID)
        {

            var respuesta = new Confirmacion<DB.Models.Color>();
            respuesta.Datos = null;

            try
            {
                var ColorDB = await _context.Colores.FindAsync(ID);
                if (ColorDB != null)
                {
                    _context.Colores.Remove(ColorDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = ColorDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El color con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro el color con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }

        public async Task<Confirmacion<ColorDTOconID>> BuscarColorPorID(int ID)
        {
            var respuesta = new Confirmacion<ColorDTOconID>();

            try
            {
                var colorDB = await _context.Colores.FirstOrDefaultAsync(c => c.ColorID == ID);

                if (colorDB != null)
                {

                    respuesta.Datos = new ColorDTOconID
                    {
                        ID = colorDB.ColorID,
                        Nombre = colorDB.ColorNombre,
                        CodigoHexa = colorDB.CodigoHexa, 
                    };
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Color encontrado exitosamente";
                }
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = $"No se encontró ningún color con el ID {ID}";
                }
            }
            catch (Exception ex)
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "Ocurrió un error al buscar el color: " + ex.Message;
            }

            return respuesta;
        }



        public async Task<Confirmacion<ICollection<ColorDTOconID>>> GetColor()
        {
            var respuesta = new Confirmacion<ICollection<ColorDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var colorsDB = await _context.Colores.ToListAsync();
                if (colorsDB.Count() != 0)
                {
                    respuesta.Datos = new List<ColorDTOconID>();
                    foreach (var color in colorsDB)
                    {
                        respuesta.Datos.Add(new ColorDTOconID()
                        {
                            ID = color.ColorID,
                            CodigoHexa = color.CodigoHexa,
                            Nombre = color.ColorNombre,

                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los colores";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen colores";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }


        public async Task<Confirmacion<ColorDTO>> PostColor(ColorDTO colorDTO)
        {
            var respuesta = new Confirmacion<ColorDTO>();
            respuesta.Datos = null;

            try
            {
                var colorDB = await _context.Colores.AsNoTracking().FirstOrDefaultAsync(x => x.CodigoHexa == colorDTO.CodigoHexa);
                if (colorDB == null)
                {
                    var colorNuevo = colorDTO.Adapt<Color>();
                    colorNuevo.ColorNombre = colorDTO.Nombre;
                    await _context.Colores.AddAsync(colorNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El color se creó correctamente.";
                    respuesta.Datos = colorDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El color ya existe.";
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

        public async Task<Confirmacion<ColorDTO>> PutColor(int ID, ColorDTO colorDTO)
        {
            var respuesta = new Confirmacion<ColorDTO>();
            respuesta.Datos = null;

            try
            {
                var colorBD = await _context.Colores.FindAsync(ID);
                if (colorBD != null)
                {
                    colorBD.CodigoHexa = colorDTO.CodigoHexa;
                    colorBD.ColorNombre = colorDTO.Nombre;


                    await _context.SaveChangesAsync();
                    respuesta.Datos = colorBD.Adapt<ColorDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El color fue modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El color no existe.";
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