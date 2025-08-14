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
    public class UbicacionServicio : IUbicacionServicio
    {
        private readonly PersicufContext _context;
        public UbicacionServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Ubicacion>> DeleteUbicacion(int ID)
        {

            var respuesta = new Confirmacion<Ubicacion>();
            respuesta.Datos = null;

            try
            {
                var ubicacionDB = await _context.Ubicaciones.FindAsync(ID);
                if (ubicacionDB != null)
                {
                    _context.Ubicaciones.Remove(ubicacionDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = ubicacionDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La ubicacion con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro la ubicacion con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }




        public async Task<Confirmacion<ICollection<UbicacionDTOconID>>> GetUbicacion()
        {
            var respuesta = new Confirmacion<ICollection<UbicacionDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var ubicacionDB = await _context.Ubicaciones.ToListAsync();
                if (ubicacionDB.Count() != 0)
                {
                    respuesta.Datos = new List<UbicacionDTOconID>();
                    foreach (var ubicacion in ubicacionDB)
                    {
                        respuesta.Datos.Add(new UbicacionDTOconID()
                        {
                            ID = ubicacion.UbicacionID,
                            Descripcion = ubicacion.Descripcion,

                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos las ubicaciones";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen ubicaciones";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }


        public async Task<Confirmacion<UbicacionDTO>> PostUbicacion(UbicacionDTO ubicacionDTO)
        {
            var respuesta = new Confirmacion<UbicacionDTO>();
            respuesta.Datos = null;

            try
            {
                var ubicacionDB = await _context.Ubicaciones.AsNoTracking().FirstOrDefaultAsync(x => x.Descripcion == ubicacionDTO.Descripcion);
                if (ubicacionDB == null)
                {
                    var ubicacionNuevo = ubicacionDTO.Adapt<Ubicacion>();
                    await _context.Ubicaciones.AddAsync(ubicacionNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La Ubicacion se creó correctamente.";
                    respuesta.Datos = ubicacionDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "La ubicacion ya existe.";
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

        public async Task<Confirmacion<UbicacionDTO>> PutUbicacion(int ID, UbicacionDTO ubicacionDTO)
        {
            var respuesta = new Confirmacion<UbicacionDTO>();
            respuesta.Datos = null;

            try
            {
                var ubicacionBD = await _context.Ubicaciones.FindAsync(ID);
                if (ubicacionBD != null)
                {
                    ubicacionBD.Descripcion = ubicacionDTO.Descripcion;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = ubicacionBD.Adapt<UbicacionDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La ubicacion fue modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "La ubicacion no existe.";
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