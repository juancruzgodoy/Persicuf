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
    public class PermisoServicio : IPermisoServicio
    {
        private readonly PersicufContext _context;
        public PermisoServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Permiso>> DeletePermiso(int ID)
        {

            var respuesta = new Confirmacion<Permiso>();
            respuesta.Datos = null;

            try
            {
                var permisoDB = await _context.Permisos.FindAsync(ID);
                if (permisoDB != null)
                {
                    _context.Permisos.Remove(permisoDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = permisoDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El Permiso con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro el Permiso con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }




        public async Task<Confirmacion<ICollection<PermisoDTOconID>>> GetPermiso()
        {
            var respuesta = new Confirmacion<ICollection<PermisoDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var permisosDB = await _context.Permisos.ToListAsync();
                if (permisosDB.Count() != 0)
                {
                    respuesta.Datos = new List<PermisoDTOconID>();
                    foreach (var permiso in permisosDB)
                    {
                        respuesta.Datos.Add(new PermisoDTOconID()
                        {
                            ID = permiso.PermisoID,
                            Descripcion = permiso.Descripcion,

                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los Permisos";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen Permisos";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }


        public async Task<Confirmacion<PermisoDTO>> PostPermiso(PermisoDTO permisoDTO)
        {
            var respuesta = new Confirmacion<PermisoDTO>();
            respuesta.Datos = null;

            try
            {
                var permisoDB = await _context.Permisos.AsNoTracking().FirstOrDefaultAsync(x => x.Descripcion == permisoDTO.Descripcion);
                if (permisoDB == null)
                {
                    var permisoNuevo = permisoDTO.Adapt<Permiso>();
                    await _context.Permisos.AddAsync(permisoNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El Permiso se creó correctamente.";
                    respuesta.Datos = permisoDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El Permiso ya existe.";
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

        public async Task<Confirmacion<PermisoDTO>> PutPermiso(int ID, PermisoDTO permisoDTO)
        {
            var respuesta = new Confirmacion<PermisoDTO>();
            respuesta.Datos = null;

            try
            {
                var permisoBD = await _context.Permisos.FindAsync(ID);
                if (permisoBD != null)
                {
                    permisoBD.Descripcion = permisoDTO.Descripcion;


                    await _context.SaveChangesAsync();
                    respuesta.Datos = permisoBD.Adapt<PermisoDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El Permiso fue modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El Permiso no existe.";
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