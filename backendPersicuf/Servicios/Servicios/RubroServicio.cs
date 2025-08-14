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
    public class RubroServicio : IRubroServicio
    {
        private readonly PersicufContext _context;
        public RubroServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Rubro>> DeleteRubro(int ID)
        {

            var respuesta = new Confirmacion<Rubro>();
            respuesta.Datos = null;

            try
            {
                var RubroDB = await _context.Rubros.FindAsync(ID);
                if (RubroDB != null)
                {
                    _context.Rubros.Remove(RubroDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = RubroDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El Rubro con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro el Rubro con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }




        public async Task<Confirmacion<ICollection<RubroDTOconID>>> GetRubro()
        {
            var respuesta = new Confirmacion<ICollection<RubroDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var RubroDB = await _context.Rubros.ToListAsync();
                if (RubroDB.Count() != 0)
                {
                    respuesta.Datos = new List<RubroDTOconID>();
                    foreach (var rubro in RubroDB)
                    {
                        respuesta.Datos.Add(new RubroDTOconID()
                        {
                            ID = rubro.RubroID,
                            Descripcion = rubro.Descripcion,

                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los Rubro";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen Rubro";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<Confirmacion<RubroDTOconID>> BuscarRubroPorID(int ID)
        {
            var respuesta = new Confirmacion<RubroDTOconID>();

            try
            {
              
                var rubroDB = await _context.Rubros.FirstOrDefaultAsync(r => r.RubroID == ID);

                if (rubroDB != null)
                {
                   
                    respuesta.Datos = new RubroDTOconID
                    {
                        ID = rubroDB.RubroID,
                        Descripcion = rubroDB.Descripcion,
                        
                    };
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Rubro encontrado exitosamente";
                }
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = $"No se encontró ningún rubro con el ID {ID}";
                }
            }
            catch (Exception ex)
            {
               
                respuesta.Exito = false;
                respuesta.Mensaje = "Ocurrió un error al buscar el rubro: " + ex.Message;
            }

            return respuesta;
        }


        public async Task<Confirmacion<RubroDTO>> PostRubro(RubroDTO rubroDTO)
        {
            var respuesta = new Confirmacion<RubroDTO>();
            respuesta.Datos = null;

            try
            {
                var rubroDB = await _context.Rubros.AsNoTracking().FirstOrDefaultAsync(x => x.Descripcion == rubroDTO.Descripcion);
                if (rubroDB == null)
                {
                    var rubroNuevo = rubroDTO.Adapt<Rubro>();
                    await _context.Rubros.AddAsync(rubroNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El Rubro se creó correctamente.";
                    respuesta.Datos = rubroDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El Rubro ya existe.";
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

        public async Task<Confirmacion<RubroDTO>> PutRubro(int ID, RubroDTO rubroDTO)
        {
            var respuesta = new Confirmacion<RubroDTO>();
            respuesta.Datos = null;

            try
            {
                var rubroBD = await _context.Rubros.FindAsync(ID);
                if (rubroBD != null)
                {
                    rubroBD.Descripcion = rubroDTO.Descripcion;


                    await _context.SaveChangesAsync();
                    respuesta.Datos = rubroBD.Adapt<RubroDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El Rubro fue modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El Rubro no existe.";
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