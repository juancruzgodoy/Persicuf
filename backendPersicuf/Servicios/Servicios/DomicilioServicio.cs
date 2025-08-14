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
    public class DomicilioServicio : IDomicilioServicio
    {
        private readonly PersicufContext _context;
        public DomicilioServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Domicilio>> DeleteDomicilio(int ID)
        {

            var respuesta = new Confirmacion<Domicilio>();
            respuesta.Datos = null;

            try
            {
                var domicilioDB = await _context.Domicilios.FindAsync(ID);
                if (domicilioDB != null)
                {
                    _context.Domicilios.Remove(domicilioDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = domicilioDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El Domicilio con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro el Domicilio con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }




        public async Task<Confirmacion<ICollection<DomicilioDTOconID>>> GetDomicilio()
        {
            var respuesta = new Confirmacion<ICollection<DomicilioDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var domicilioDB = await _context.Domicilios.ToListAsync();
                if (domicilioDB.Count() != 0)
                {
                    respuesta.Datos = new List<DomicilioDTOconID>();
                    foreach (var domicilio in domicilioDB)
                    {
                        respuesta.Datos.Add(new DomicilioDTOconID()
                        {
                            ID = domicilio.DomicilioID,
                            Calle = domicilio.Calle,
                            Numero = domicilio.Numero,
                            Piso = domicilio.Piso,
                            Depto = domicilio.Depto,
                            Descripcion = domicilio.Descripcion,
                            UsuarioID = domicilio.UsuarioID,
                            LocalidadID = domicilio.LocalidadID,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los domicilios";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen domicilios";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }


        public async Task<Confirmacion<DomicilioDTO>> PostDomicilio(DomicilioDTO domicilioDTO)
        {
            var respuesta = new Confirmacion<DomicilioDTO>();
            respuesta.Datos = null;

            try
            {
                var domicilioDB = await _context.Domicilios.AsNoTracking().FirstOrDefaultAsync(x => x.UsuarioID == domicilioDTO.UsuarioID
                 && x.LocalidadID == domicilioDTO.LocalidadID 
                 && x.Calle == domicilioDTO.Calle 
                 && x.Numero == domicilioDTO.Numero 
                 && x.Piso == domicilioDTO.Piso 
                 && x.Depto == domicilioDTO.Depto);

                if (domicilioDB == null)
                {
                    var domicilioNuevo = domicilioDTO.Adapt<Domicilio>();
                    await _context.Domicilios.AddAsync(domicilioNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El domicilio se creó correctamente.";
                    respuesta.Datos = domicilioDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El Domicilio ya existe.";
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

        public async Task<Confirmacion<DomicilioDTO>> PutDomicilio(int ID, DomicilioDTO domicilioDTO)
        {
            var respuesta = new Confirmacion<DomicilioDTO>();
            respuesta.Datos = null;

            try
            {
                var domicilioBD = await _context.Domicilios.FindAsync(ID);
                if (domicilioBD != null)
                {
                    domicilioBD.Calle = domicilioDTO.Calle;
                    domicilioBD.Numero = domicilioDTO.Numero;
                    domicilioBD.Piso = domicilioDTO.Piso;
                    domicilioBD.Depto = domicilioDTO.Depto;
                    domicilioBD.Descripcion = domicilioDTO.Descripcion;
                    domicilioBD.UsuarioID = domicilioDTO.UsuarioID;
                    domicilioBD.LocalidadID = domicilioDTO.LocalidadID;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = domicilioBD.Adapt<DomicilioDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El Domicilio fue modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El Domicilio no existe.";
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