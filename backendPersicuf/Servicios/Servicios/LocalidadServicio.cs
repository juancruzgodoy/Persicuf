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
    public class LocalidadServicio : ILocalidadServicio
    {
        private readonly PersicufContext _context;
        public LocalidadServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Localidad>> DeleteLocalidad(int ID)
        {

            var respuesta = new Confirmacion<Localidad>();
            respuesta.Datos = null;

            try
            {
                var localidadDB = await _context.Localidades.FindAsync(ID);
                if (localidadDB != null)
                {
                    _context.Localidades.Remove(localidadDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = localidadDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La localidad con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro la localidad con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }


        public async Task<Confirmacion<ICollection<LocalidadDTOconID>>> GetLocalidad()
        {
            var respuesta = new Confirmacion<ICollection<LocalidadDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var localidadDB = await _context.Localidades.ToListAsync();
                if (localidadDB.Count() != 0)
                {
                    respuesta.Datos = new List<LocalidadDTOconID>();
                    foreach (var Localidad in localidadDB)
                    {
                        respuesta.Datos.Add(new LocalidadDTOconID()
                        {
                            ID = Localidad.LocalidadID,
                            ProvinciaID = Localidad.ProvinciaID,
                            Nombre = Localidad.Nombre,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todas los Localidades";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen Localidades";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }
        public async Task<Confirmacion<LocalidadDTO>> PostLocalidad(LocalidadDTO localidadDTO)
        {
            var respuesta = new Confirmacion<LocalidadDTO>();
            respuesta.Datos = null;

            try
            {
                var localidadDB = await _context.Localidades.AsNoTracking().FirstOrDefaultAsync(x => x.Nombre == localidadDTO.Nombre);
                if (localidadDB == null)
                {
                    var localidadNuevo = localidadDTO.Adapt<Localidad>();
                    await _context.Localidades.AddAsync(localidadNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La localidad se creó correctamente.";
                    respuesta.Datos = localidadDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "La localidad ya existe.";
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

        public async Task<Confirmacion<LocalidadDTO>> PutLocalidad(int ID, LocalidadDTO localidadDTO)
        {
            var respuesta = new Confirmacion<LocalidadDTO>();
            respuesta.Datos = null;

            try
            {
                var localidadBD = await _context.Localidades.FindAsync(ID);
                if (localidadBD != null)
                {
                    localidadBD.Nombre = localidadDTO.Nombre;
                    localidadBD.ProvinciaID = localidadDTO.ProvinciaID;
                    await _context.SaveChangesAsync();
                    respuesta.Datos = localidadBD.Adapt<LocalidadDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La localidad fué modificada correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "La localidad no existe.";
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

