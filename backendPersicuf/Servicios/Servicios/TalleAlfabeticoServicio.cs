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
    public class TalleAlfabeticoServicio : ITalleAlfabeticoServicio
    {
        private readonly PersicufContext _context;
        public TalleAlfabeticoServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<TalleAlfabetico>> DeleteTalleAlfabetico(int ID)
        {

            var respuesta = new Confirmacion<TalleAlfabetico>();
            respuesta.Datos = null;

            try
            {
                var talleAlfabeticoDB = await _context.TallesAlfabeticos.FindAsync(ID);
                if (talleAlfabeticoDB != null)
                {
                    _context.TallesAlfabeticos.Remove(talleAlfabeticoDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = talleAlfabeticoDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El TalleAlfabetico con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro el TalleAlfabetico con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }




        public async Task<Confirmacion<ICollection<TalleAlfabeticoDTOconID>>> GetTalleAlfabetico()
        {
            var respuesta = new Confirmacion<ICollection<TalleAlfabeticoDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var talleAlfabeticoDB = await _context.TallesAlfabeticos.ToListAsync();
                if (talleAlfabeticoDB.Count() != 0)
                {
                    respuesta.Datos = new List<TalleAlfabeticoDTOconID>();
                    foreach (var talleAlfabetico in talleAlfabeticoDB)
                    {
                        respuesta.Datos.Add(new TalleAlfabeticoDTOconID()
                        {
                            ID = talleAlfabetico.TAID,
                            Descripcion = talleAlfabetico.Descripcion,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los TalleAlfabeticos";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen TalleAlfabetico";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }



        public async Task<Confirmacion<TalleAlfabeticoDTO>> PostTalleAlfabetico(TalleAlfabeticoDTO talleAlfabeticoDTO)
        {
            var respuesta = new Confirmacion<TalleAlfabeticoDTO>();
            respuesta.Datos = null;

            try
            {
                var talleAlfabeticoDB = await _context.TallesAlfabeticos.AsNoTracking().FirstOrDefaultAsync(x => x.Descripcion == talleAlfabeticoDTO.Descripcion);
                if (talleAlfabeticoDB == null)
                {
                    var talleAlfabeticoNuevo = talleAlfabeticoDTO.Adapt<TalleAlfabetico>();
                    await _context.TallesAlfabeticos.AddAsync(talleAlfabeticoNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El TalleAlfabetico se creó correctamente.";
                    respuesta.Datos = talleAlfabeticoDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El TalleAlfabetico ya existe.";
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

        public async Task<Confirmacion<TalleAlfabeticoDTO>> PutTalleAlfabetico(int ID, TalleAlfabeticoDTO talleAlfabeticoDTO)
        {
            var respuesta = new Confirmacion<TalleAlfabeticoDTO>();
            respuesta.Datos = null;

            try
            {
                var talleAlfabeticoBD = await _context.TallesAlfabeticos.FindAsync(ID);
                if (talleAlfabeticoBD != null)
                {
                    talleAlfabeticoBD.Descripcion = talleAlfabeticoDTO.Descripcion;


                    await _context.SaveChangesAsync();
                    respuesta.Datos = talleAlfabeticoBD.Adapt<TalleAlfabeticoDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El TalleAlfabetico fue modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El TalleAlfabetico no existe.";
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