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
    public class PrendaServicio : IPrendaServicio
    {
        private readonly PersicufContext _context;
        public PrendaServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Prenda>> DeletePrenda(int ID)
        {

            var respuesta = new Confirmacion<Prenda>();
            respuesta.Datos = null;

            try
            {
                var prendaDB = await _context.Prendas.FindAsync(ID);
                if (prendaDB != null)
                {
                    _context.Prendas.Remove(prendaDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = prendaDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La prenda con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro la prenda con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }


        public async Task<Confirmacion<ICollection<PrendaDTOconID>>> GetPrenda()
        {
            var respuesta = new Confirmacion<ICollection<PrendaDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var prendaDB = await _context.Prendas.ToListAsync();
                if (prendaDB.Count() != 0)
                {
                    respuesta.Datos = new List<PrendaDTOconID>();
                    foreach (var Prenda in prendaDB)
                    {
                        respuesta.Datos.Add(new PrendaDTOconID()
                        {
                            ID = Prenda.PrendaID,
                            Precio = Prenda.Precio,
                            ColorID = Prenda.ColorID,
                            MaterialID = Prenda.MaterialID,
                            UsuarioID = Prenda.UsuarioID,
                            RubroID = Prenda.RubroID,
                            ImagenID = Prenda.ImagenID,
                            Nombre = Prenda.Nombre,
                            EstampadoID = Prenda.EstampadoID,
                            PostID = Prenda.PostID,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todas las Prendas";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen Prendas";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<Confirmacion<ICollection<PrendaDTOconID>>> BuscarPrenda(string busqueda)
        {
            var respuesta = new Confirmacion<ICollection<PrendaDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var prendaDB = await _context.Prendas
                .Where(p => p.Nombre.ToLower().Contains(busqueda.ToLower()))
                .OrderByDescending(p => p.Nombre.ToLower().StartsWith(busqueda.ToLower()))
                .ThenBy(p => p.Nombre)
                .ToListAsync();



                if (prendaDB.Count() != 0)
                {
                    respuesta.Datos = new List<PrendaDTOconID>();
                    foreach (var Prenda in prendaDB)
                    {
                        respuesta.Datos.Add(new PrendaDTOconID()
                        {
                            ID = Prenda.PrendaID,
                            Precio = Prenda.Precio,
                            ColorID = Prenda.ColorID,
                            MaterialID = Prenda.MaterialID,
                            UsuarioID = Prenda.UsuarioID,
                            RubroID = Prenda.RubroID,
                            ImagenID = Prenda.ImagenID,
                            Nombre = Prenda.Nombre,
                            EstampadoID = Prenda.EstampadoID,
                            PostID = Prenda.PostID,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todas las Prendas que coiciden con la busqueda";
                    return respuesta;
                }

                respuesta.Mensaje = "No se encontraron prendas con ese nombre";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<Confirmacion<ICollection<PrendaDTOconID>>> GetPrendaUsuario(int ID)
        {
            var respuesta = new Confirmacion<ICollection<PrendaDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var prendaDB = await _context.Prendas
                .Where(p => p.UsuarioID == ID).OrderBy(p => p.PrendaID).ToListAsync();



                if (prendaDB.Count() != 0)
                {
                    respuesta.Datos = new List<PrendaDTOconID>();
                    foreach (var Prenda in prendaDB)
                    {
                        respuesta.Datos.Add(new PrendaDTOconID()
                        {
                            ID = Prenda.PrendaID,
                            Precio = Prenda.Precio,
                            ColorID = Prenda.ColorID,
                            MaterialID = Prenda.MaterialID,
                            UsuarioID = Prenda.UsuarioID,
                            RubroID = Prenda.RubroID,
                            ImagenID = Prenda.ImagenID,
                            Nombre = Prenda.Nombre,
                            EstampadoID = Prenda.EstampadoID,
                            PostID = Prenda.PostID,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todas las Prendas";
                    return respuesta;
                }

                respuesta.Mensaje = "No se encontraron prendas";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<Confirmacion<PrendaDTOconID>> BuscarPrendaPorID(int ID)
        {
            var respuesta = new Confirmacion<PrendaDTOconID>();

            try
            {
                var prendaDB = await _context.Prendas.FirstOrDefaultAsync(p => p.PrendaID == ID);

                if (prendaDB != null)
                {
                    // Mapea la entidad a DTO
                    respuesta.Datos = new PrendaDTOconID
                    {
                        ID = prendaDB.PrendaID,
                        Precio = prendaDB.Precio,
                        ColorID = prendaDB.ColorID,
                        MaterialID = prendaDB.MaterialID,
                        UsuarioID = prendaDB.UsuarioID,
                        RubroID = prendaDB.RubroID,
                        ImagenID = prendaDB.ImagenID,
                        Nombre = prendaDB.Nombre,
                        EstampadoID = prendaDB.EstampadoID,
                        PostID = prendaDB.PostID,
                    };
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Prenda encontrada exitosamente";
                }
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = $"No se encontró ninguna prenda con el ID {ID}";
                }
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                respuesta.Exito = false;
                respuesta.Mensaje = "Ocurrió un error al buscar la prenda: " + ex.Message;
            }

            return respuesta;
        }



        public async Task<Confirmacion<PrendaDTO>> PostPrenda(PrendaDTO prendaDTO)
        {
            var respuesta = new Confirmacion<PrendaDTO>();
            respuesta.Datos = null;

            try
            {
                var prendaDB = await _context.Prendas.AsNoTracking().FirstOrDefaultAsync(x => x.Nombre == prendaDTO.Nombre);
                if (prendaDB == null)
                {
                    var prendaNuevo = prendaDTO.Adapt<Prenda>();
                    await _context.Prendas.AddAsync(prendaNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El prenda se creó correctamente.";
                    respuesta.Datos = prendaDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "La prenda con nombre: " + prendaDTO.Nombre + " ya existe.";
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

        public async Task<Confirmacion<PrendaDTO>> PutPrenda(int ID, PrendaDTO prendaDTO)
        {
            var respuesta = new Confirmacion<PrendaDTO>();
            respuesta.Datos = null;

            try
            {
                var prendaBD = await _context.Prendas.FindAsync(ID);
                if (prendaBD != null)
                {
                    prendaBD.ColorID = prendaDTO.ColorID;
                    prendaBD.MaterialID = prendaDTO.MaterialID;
                    prendaBD.UsuarioID = prendaDTO.UsuarioID;
                    prendaBD.ImagenID = prendaDTO.ImagenID;
                    prendaBD.RubroID = prendaDTO.RubroID;
                    prendaBD.Precio = prendaDTO.Precio;
                    prendaBD.Nombre = prendaDTO.Nombre; 
                    prendaBD.EstampadoID = prendaDTO.EstampadoID;
                    prendaBD.PostID = prendaDTO.PostID;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = prendaBD.Adapt<PrendaDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El prenda fué modificada correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El prenda no existe.";
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
