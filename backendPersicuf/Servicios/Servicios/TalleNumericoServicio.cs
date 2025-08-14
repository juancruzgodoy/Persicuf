using System;
using System.Collections.Generic;
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
    public class TalleNumericoServicio : ITalleNumericoServicio
    {
        private readonly PersicufContext _context;
        public TalleNumericoServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<TalleNumerico>> DeleteTalleNumerico(int ID)
        {

            var respuesta = new Confirmacion<TalleNumerico>();
            respuesta.Datos = null;

            try
            {
                var talleNumericoDB = await _context.TallesNumericos.FindAsync(ID);
                if (talleNumericoDB != null)
                {
                    _context.TallesNumericos.Remove(talleNumericoDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = talleNumericoDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El TalleNumerico con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro el TalleNumerico con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }




        public async Task<Confirmacion<ICollection<TalleNumericoDTOconID>>> GetTalleNumerico()
        {
            var respuesta = new Confirmacion<ICollection<TalleNumericoDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var talleNumericoDB = await _context.TallesNumericos.ToListAsync();
                if (talleNumericoDB.Count() != 0)
                {
                    respuesta.Datos = new List<TalleNumericoDTOconID>();
                    foreach (var talleNumerico in talleNumericoDB)
                    {
                        respuesta.Datos.Add(new TalleNumericoDTOconID()
                        {
                            ID = talleNumerico.TNID,
                            Descripcion = talleNumerico.Descripcion,

                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los TalleNumerico";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen TalleNumerico";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }



        public async Task<Confirmacion<TalleNumericoDTO>> PostTalleNumerico(TalleNumericoDTO talleNumericoDTO)
        {
            var respuesta = new Confirmacion<TalleNumericoDTO>();
            respuesta.Datos = null;

            try
            {
                var talleNumericoDB = await _context.TallesNumericos.AsNoTracking().FirstOrDefaultAsync(x => x.Descripcion == talleNumericoDTO.Descripcion);
                if (talleNumericoDB == null)
                {
                    var talleNumericoNuevo = talleNumericoDTO.Adapt<TalleNumerico>();
                    await _context.TallesNumericos.AddAsync(talleNumericoNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El TalleNumerico se creó correctamente.";
                    respuesta.Datos = talleNumericoDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El TalleNumerico ya existe.";
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

        public async Task<Confirmacion<TalleNumericoDTO>> PutTalleNumerico(int ID, TalleNumericoDTO talleNumericoDTO)
        {
            var respuesta = new Confirmacion<TalleNumericoDTO>();
            respuesta.Datos = null;

            try
            {
                var talleNumericoBD = await _context.TallesNumericos.FindAsync(ID);
                if (talleNumericoBD != null)
                {
                    talleNumericoBD.Descripcion = talleNumericoDTO.Descripcion;


                    await _context.SaveChangesAsync();
                    respuesta.Datos = talleNumericoBD.Adapt<TalleNumericoDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El TalleNumerico fue modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El TalleNumerico no existe.";
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
