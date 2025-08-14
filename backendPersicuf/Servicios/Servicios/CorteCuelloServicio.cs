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
using System.Drawing;

namespace Servicios.Servicios
{
    public class CorteCuelloServicio : ICorteCuelloServicio
    {
        private readonly PersicufContext _context;
        public CorteCuelloServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<CorteCuello>> DeleteCorteCuello(int ID)
        {

            var respuesta = new Confirmacion<CorteCuello>();
            respuesta.Datos = null;

            try
            {
                var CorteCuelloDB = await _context.CortesCuello.FindAsync(ID);
                if (CorteCuelloDB != null)
                {
                    _context.CortesCuello.Remove(CorteCuelloDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = CorteCuelloDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El CorteCuello con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro el CorteCuello con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }




        public async Task<Confirmacion<ICollection<CorteCuelloDTOconID>>> GetCorteCuello()
        {
            var respuesta = new Confirmacion<ICollection<CorteCuelloDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var CorteCuelloDB = await _context.CortesCuello.ToListAsync();
                if (CorteCuelloDB.Count() != 0)
                {
                    respuesta.Datos = new List<CorteCuelloDTOconID>();
                    foreach (var corteCuello in CorteCuelloDB)
                    {
                        respuesta.Datos.Add(new CorteCuelloDTOconID()
                        {
                            ID = corteCuello.CCID,
                            Descripcion = corteCuello.Descripcion,

                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los CorteCuello";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen CorteCuello";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }


        public async Task<Confirmacion<CorteCuelloDTO>> PostCorteCuello(CorteCuelloDTO corteCuelloDTO)
        {
            var respuesta = new Confirmacion<CorteCuelloDTO>();
            respuesta.Datos = null;

            try
            {
                var corteCuelloDB = await _context.CortesCuello.AsNoTracking().FirstOrDefaultAsync(x => x.Descripcion == corteCuelloDTO.Descripcion);
                if (corteCuelloDB == null)
                {
                    var corteCuelloNuevo = corteCuelloDTO.Adapt<CorteCuello>();
                    await _context.CortesCuello.AddAsync(corteCuelloNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El CorteCuello se creado correctamente.";
                    respuesta.Datos = corteCuelloDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El CorteCuello ya existe.";
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

        public async Task<Confirmacion<CorteCuelloDTO>> PutCorteCuello(int ID, CorteCuelloDTO corteCuelloDTO)
        {
            var respuesta = new Confirmacion<CorteCuelloDTO>();
            respuesta.Datos = null;

            try
            {
                var corteCuelloBD = await _context.CortesCuello.FindAsync(ID);
                if (corteCuelloBD != null)
                {
                    corteCuelloBD.Descripcion = corteCuelloDTO.Descripcion;


                    await _context.SaveChangesAsync();
                    respuesta.Datos = corteCuelloBD.Adapt<CorteCuelloDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El CorteCuello fue modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El CorteCuello no existe.";
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