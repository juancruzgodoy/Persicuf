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
    public class LargoServicio : ILargoServicio
    {
        private readonly PersicufContext _context;
        public LargoServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Largo>> DeleteLargo(int ID)
        {

            var respuesta = new Confirmacion<Largo>();
            respuesta.Datos = null;

            try
            {
                var largoDB = await _context.Largos.FindAsync(ID);
                if (largoDB != null)
                {
                    _context.Largos.Remove(largoDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = largoDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El largo con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro el largo con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }




        public async Task<Confirmacion<ICollection<LargoDTOconID>>> GetLargo()
        {
            var respuesta = new Confirmacion<ICollection<LargoDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var largosDB = await _context.Largos.ToListAsync();
                if (largosDB.Count() != 0)
                {
                    respuesta.Datos = new List<LargoDTOconID>();
                    foreach (var largo in largosDB)
                    {
                        respuesta.Datos.Add(new LargoDTOconID()
                        {
                            ID = largo.LargoID,
                            Descripcion = largo.Descripcion,
                            Precio = largo.Precio,

                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los largos";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen largos";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }


        public async Task<Confirmacion<LargoDTO>> PostLargo(LargoDTO largoDTO)
        {
            var respuesta = new Confirmacion<LargoDTO>();
            respuesta.Datos = null;

            try
            {
                var largoDB = await _context.Largos.AsNoTracking().FirstOrDefaultAsync(x => x.Descripcion == largoDTO.Descripcion);
                if (largoDB == null)
                {
                    var largoNuevo = largoDTO.Adapt<Largo>();
                    await _context.Largos.AddAsync(largoNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El largo se creó correctamente.";
                    respuesta.Datos = largoDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El largo ya existe.";
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

        public async Task<Confirmacion<LargoDTO>> PutLargo(int ID, LargoDTO largoDTO)
        {
            var respuesta = new Confirmacion<LargoDTO>();
            respuesta.Datos = null;

            try
            {
                var largoBD = await _context.Largos.FindAsync(ID);
                if (largoBD != null)
                {
                    largoBD.Descripcion = largoDTO.Descripcion;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = largoBD.Adapt<LargoDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El Largo fue modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El largo no existe.";
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
