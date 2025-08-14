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
    public class CamperaServicio : ICamperaServicio
    {
        private readonly PersicufContext _context;
        public CamperaServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Campera>> DeleteCampera(int ID)
        {

            var respuesta = new Confirmacion<Campera>();
            respuesta.Datos = null;

            try
            {
                var camperaDB = await _context.Camperas.FindAsync(ID);
                if (camperaDB != null)
                {
                    _context.Camperas.Remove(camperaDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = camperaDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La campera con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro la campera con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }


        public async Task<Confirmacion<ICollection<CamperaDTOconID>>> GetCampera()
        {
            var respuesta = new Confirmacion<ICollection<CamperaDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var camperaDB = await _context.Camperas.ToListAsync();
                if (camperaDB.Count() != 0)
                {
                    respuesta.Datos = new List<CamperaDTOconID>();
                    foreach (var Campera in camperaDB)
                    {
                        respuesta.Datos.Add(new CamperaDTOconID()
                        {
                            ID = Campera.PrendaID,
                            TalleAlfabeticoID = Campera.TAID,
                            Precio = Campera.Precio,
                            ColorID = Campera.ColorID,
                            MaterialID = Campera.MaterialID,
                            UsuarioID = Campera.UsuarioID,
                            RubroID = Campera.RubroID,
                            ImagenID = Campera.ImagenID,
                            Nombre = Campera.Nombre,
                            EstampadoID = Campera.EstampadoID,
                            PostID = Campera.PostID,

                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos las Camperas";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen Camperas";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<Confirmacion<ICollection<CamperaDTOconID>>> BuscarCamperas(string busqueda)
        {
            var respuesta = new Confirmacion<ICollection<CamperaDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var camperaDB = await _context.Camperas
                .Where(p => p.Nombre.ToLower().Contains(busqueda.ToLower()))
                .OrderByDescending(p => p.Nombre.ToLower().StartsWith(busqueda.ToLower()))
                .ThenBy(p => p.Nombre)
                .ToListAsync();



                if (camperaDB.Count() != 0)
                {
                    respuesta.Datos = new List<CamperaDTOconID>();
                    foreach (var Campera in camperaDB)
                    {
                        respuesta.Datos.Add(new CamperaDTOconID()
                        {
                            ID = Campera.PrendaID,
                            Precio = Campera.Precio,
                            ColorID = Campera.ColorID,
                            MaterialID = Campera.MaterialID,
                            UsuarioID = Campera.UsuarioID,
                            RubroID = Campera.RubroID,
                            ImagenID = Campera.ImagenID,
                            Nombre = Campera.Nombre,
                            EstampadoID = Campera.EstampadoID,
                            PostID = Campera.PostID,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todas las Camperas que coiciden con la busqueda";
                    return respuesta;
                }

                respuesta.Mensaje = "No se encontraron camperas con ese nombre";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<Confirmacion<float>> CalcularPrecio(int MaterialID, int EstampadoID)
        {
            var respuesta = new Confirmacion<float>();

            try
            {
                float materialPrecio = (await _context.Materiales.FindAsync(MaterialID)).Precio;
                float imagenPrecio = 0;
                if (EstampadoID != 0)
                {

                    imagenPrecio = 4000;
                }
                else
                {
                    imagenPrecio = 0;
                }

                respuesta.Datos = materialPrecio + imagenPrecio;
                respuesta.Exito = true;
                respuesta.Mensaje = "Se ha calculado el precio con exito";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    respuesta.Mensaje += "Inner Exception: " + ex.InnerException.Message;
                }
                return (respuesta);
            }
        }

        public async Task<Confirmacion<CamperaDTO>> PostCampera(CamperaDTO camperaDTO)
        {
            var respuesta = new Confirmacion<CamperaDTO>();
            respuesta.Datos = null;

            try
            {
                var camperaDB = await _context.Camperas.AsNoTracking().FirstOrDefaultAsync(x => x.Nombre == camperaDTO.Nombre);
                if (camperaDB == null)
                {
                    var camperaNuevo = camperaDTO.Adapt<Campera>();
                    camperaNuevo.TAID = camperaDTO.TalleAlfabeticoID;
                    var precioResultado = await CalcularPrecio(camperaNuevo.MaterialID, camperaNuevo.ImagenID ?? 0);
                    if (precioResultado.Exito)
                    {
                        camperaNuevo.Precio = precioResultado.Datos;
                    }
                    else
                    {
                        throw new Exception("Error al calcular el precio: " + precioResultado.Mensaje);
                    }
                    await _context.Camperas.AddAsync(camperaNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El campera se creó correctamente.";
                    respuesta.Datos = camperaDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "La campera con nombre: " + camperaDTO.Nombre + " ya existe.";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    respuesta.Mensaje += "/n Inner Exception: " + ex.InnerException.Message;
                }
                return (respuesta);
            }
        }
        public async Task<Confirmacion<CamperaDTO>> PutCampera(int ID, CamperaDTO camperaDTO)
        {
            var respuesta = new Confirmacion<CamperaDTO>();
            respuesta.Datos = null;

            try
            {
                var camperaBD = await _context.Camperas.FindAsync(ID);
                if (camperaBD != null)
                {
                    camperaBD.TAID = camperaDTO.TalleAlfabeticoID;
                    camperaBD.ColorID = camperaDTO.ColorID;
                    camperaBD.MaterialID = camperaDTO.MaterialID;
                    camperaBD.UsuarioID = camperaDTO.UsuarioID;
                    camperaBD.ImagenID = camperaDTO.ImagenID;
                    camperaBD.RubroID = camperaDTO.RubroID;
                    camperaBD.Precio = camperaDTO.Precio;
                    camperaBD.Nombre = camperaDTO.Nombre;
                    camperaBD.EstampadoID = camperaDTO.EstampadoID;
                    camperaBD.PostID = camperaDTO.PostID;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = camperaBD.Adapt<CamperaDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El campera fué modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El campera no existe.";
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