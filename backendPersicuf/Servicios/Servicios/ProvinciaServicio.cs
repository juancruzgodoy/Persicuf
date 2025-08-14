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
    public class ProvinciaServicio : IProvinciaServicio
    {
        private readonly PersicufContext _context;
        public ProvinciaServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Provincia>> DeleteProvincia(int ID)
        {

            var respuesta = new Confirmacion<Provincia>();
            respuesta.Datos = null;

            try
            {
                var provinciaDB = await _context.Provincias.FindAsync(ID);
                if (provinciaDB != null)
                {
                    _context.Provincias.Remove(provinciaDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = provinciaDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La provincia con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro la provincia con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }




        public async Task<Confirmacion<ICollection<ProvinciaDTOconID>>> GetProvincia()
        {
            var respuesta = new Confirmacion<ICollection<ProvinciaDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var provinciasDB = await _context.Provincias.ToListAsync();
                if (provinciasDB.Count() != 0)
                {
                    respuesta.Datos = new List<ProvinciaDTOconID>();
                    foreach (var provincia in provinciasDB)
                    {
                        respuesta.Datos.Add(new ProvinciaDTOconID()
                        {
                            ID = provincia.ProvinciaID,
                            Nombre = provincia.ProvinciaNombre,

                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos las provincias";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen provincias";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }



        public async Task<Confirmacion<ProvinciaDTO>> PostProvincia(ProvinciaDTO provinciaDTO)
        {
            var respuesta = new Confirmacion<ProvinciaDTO>();
            respuesta.Datos = null;

            try
            {
                var provinciaBD = await _context.Provincias.AsNoTracking().FirstOrDefaultAsync(x => x.ProvinciaNombre == provinciaDTO.Nombre);
                if (provinciaBD == null)
                {
                    var ProvinciaNueva = provinciaDTO.Adapt<Provincia>();
                    ProvinciaNueva.ProvinciaNombre = provinciaDTO.Nombre;
                    await _context.Provincias.AddAsync(ProvinciaNueva);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La Provincia se creó correctamente.";
                    respuesta.Datos = provinciaDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "La provincia ya existe.";
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

        public async Task<Confirmacion<ProvinciaDTO>> PutProvincia(int ID, ProvinciaDTO provinciaDTO)
        {
            var respuesta = new Confirmacion<ProvinciaDTO>();
            respuesta.Datos = null;

            try
            {
                var provinciaBD = await _context.Provincias.FindAsync(ID);
                if (provinciaBD != null)
                {
                    provinciaBD.ProvinciaNombre = provinciaDTO.Nombre;


                    await _context.SaveChangesAsync();
                    respuesta.Datos = provinciaBD.Adapt<ProvinciaDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La provincia fue modificada correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "La provincia no existe.";
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