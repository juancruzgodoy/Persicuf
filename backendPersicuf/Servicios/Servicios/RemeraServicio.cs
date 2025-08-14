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
    public class RemeraServicio : IRemeraServicio
    {
        private readonly PersicufContext _context;
        public RemeraServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Remera>> DeleteRemera(int ID)
        {

            var respuesta = new Confirmacion<Remera>();
            respuesta.Datos = null;

            try
            {
                var remeraDB = await _context.Remeras.FindAsync(ID);
                if (remeraDB != null)
                {
                    _context.Remeras.Remove(remeraDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = remeraDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "La remera con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro la remera con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }


        public async Task<Confirmacion<ICollection<RemeraDTOconID>>> GetRemera()
        {
            var respuesta = new Confirmacion<ICollection<RemeraDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var remeraDB = await _context.Remeras.ToListAsync();
                if (remeraDB.Count() != 0)
                {
                    respuesta.Datos = new List<RemeraDTOconID>();
                    foreach (var Remera in remeraDB)
                    {
                        respuesta.Datos.Add(new RemeraDTOconID()
                        {
                            ID = Remera.PrendaID,
                            CorteCuelloID = Remera.CCID,
                            TalleAlfabeticoID = Remera.TAID,
                            MangaID = Remera.MangaID,
                            Precio = Remera.Precio,
                            ColorID = Remera.ColorID,
                            MaterialID = Remera.MaterialID,
                            UsuarioID = Remera.UsuarioID,
                            RubroID = Remera.RubroID,
                            ImagenID = Remera.ImagenID,
                            Nombre = Remera.Nombre,
                            EstampadoID = Remera.EstampadoID,
                            PostID = Remera.PostID,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todas las Remeras";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen Remeras";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<Confirmacion<ICollection<RemeraDTOconID>>> BuscarRemeras(string busqueda)
        {
            var respuesta = new Confirmacion<ICollection<RemeraDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var remeraDB = await _context.Remeras
                .Where(p => p.Nombre.ToLower().Contains(busqueda.ToLower()))
                .OrderByDescending(p => p.Nombre.ToLower().StartsWith(busqueda.ToLower()))
                .ThenBy(p => p.Nombre)
                .ToListAsync();



                if (remeraDB.Count() != 0)
                {
                    respuesta.Datos = new List<RemeraDTOconID>();
                    foreach (var Remera in remeraDB)
                    {
                        respuesta.Datos.Add(new RemeraDTOconID()
                        {
                            ID = Remera.PrendaID,
                            Precio = Remera.Precio,
                            ColorID = Remera.ColorID,
                            MaterialID = Remera.MaterialID,
                            UsuarioID = Remera.UsuarioID,
                            RubroID = Remera.RubroID,
                            ImagenID = Remera.ImagenID,
                            Nombre = Remera.Nombre,
                            EstampadoID = Remera.EstampadoID,
                            PostID = Remera.PostID,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todas las Remeras que coiciden con la busqueda";
                    return respuesta;
                }

                respuesta.Mensaje = "No se encontraron remeras con ese nombre";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }
        public async Task<Confirmacion<RemeraDTO>> PostRemera(RemeraDTO remeraDTO)
        {
            var respuesta = new Confirmacion<RemeraDTO>();
            respuesta.Datos = null;

            try
            {
                var remeraDB = await _context.Remeras.AsNoTracking().FirstOrDefaultAsync(x => x.Nombre == remeraDTO.Nombre);
                if (remeraDB == null)
                {
                    var remeraNuevo = remeraDTO.Adapt<Remera>();
                    remeraNuevo.CCID = remeraDTO.CorteCuelloID;
                    remeraNuevo.MangaID = remeraDTO.MangaID;
                    remeraNuevo.TAID = remeraDTO.TalleAlfabeticoID;
                    var precioResultado = await CalcularPrecio(remeraNuevo.MaterialID, remeraNuevo.MangaID, remeraNuevo.ImagenID ?? 0);
                    if (precioResultado.Exito)
                    {
                        remeraNuevo.Precio = precioResultado.Datos;
                    }
                    else
                    {
                        throw new Exception("Error al calcular el precio: " + precioResultado.Mensaje);
                    }


                    await _context.Remeras.AddAsync(remeraNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El remera se creó correctamente.";
                    respuesta.Datos = remeraDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "La remera con nombre: " + remeraDTO.Nombre + " ya existe.";
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

        public async Task<Confirmacion<float>> CalcularPrecio(int MaterialID, int MangaID, int EstampadoID)
        {
            var respuesta = new Confirmacion<float>();

            try
            {
                float materialPrecio = (await _context.Materiales.FindAsync(MaterialID)).Precio;
                float mangaPrecio = (await _context.Mangas.FindAsync(MangaID)).Precio;
                float imagenPrecio = 0;
                if (EstampadoID != 0)
                {

                    imagenPrecio = 4000;
                }
                else
                {
                    imagenPrecio = 0;
                }

                respuesta.Datos = materialPrecio + mangaPrecio + imagenPrecio;
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

        //public async Task<Confirmacion<RemeraDTO>> PostRemeraCliente(RemeraDTO remeraDTO)
        //{
        //    var respuesta = new Confirmacion<RemeraDTO>();
        //    respuesta.Datos = null;

        //    try
        //    {
        //        var remeraDB = await _context.Remeras.AsNoTracking().FirstOrDefaultAsync(x => x.Nombre == remeraDTO.Nombre);
        //        if (remeraDB == null)
        //        {
        //            var remeraNuevo = remeraDTO.Adapt<Remera>();
        //            remeraNuevo.CCID = remeraDTO.CorteCuelloID;
        //            remeraNuevo.MangaID = remeraDTO.MangaID;
        //            remeraNuevo.TAID = remeraDTO.TalleAlfabeticoID;
        //            var precioResultado = await CalcularPrecio(remeraNuevo.MaterialID, remeraNuevo.MangaID, remeraNuevo.ImagenID ?? 0);
        //            if (precioResultado.Exito)
        //            {
        //                remeraNuevo.Precio = precioResultado.Datos;
        //            }
        //            else
        //            {
        //                throw new Exception("Error al calcular el precio: " + precioResultado.Mensaje);
        //            }


        //            await _context.Remeras.AddAsync(remeraNuevo);
        //            await _context.SaveChangesAsync();
        //            respuesta.Exito = true;
        //            respuesta.Mensaje = "El remera se creó correctamente.";
        //            respuesta.Datos = remeraDTO;
        //            return (respuesta);
        //        }
        //        respuesta.Mensaje = "La remera con nombre: " + remeraDTO.Nombre + " ya existe.";
        //        return (respuesta);
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Mensaje = "Error: " + ex.Message;
        //        if (ex.InnerException != null)
        //        {
        //            respuesta.Mensaje += " Inner Exception: " + ex.InnerException.Message;
        //        }
        //        return (respuesta);
        //    }
        //}

        public async Task<Confirmacion<RemeraDTO>> PutRemera(int ID, RemeraDTO remeraDTO)
        {
            var respuesta = new Confirmacion<RemeraDTO>();
            respuesta.Datos = null;

            try
            {
                var remeraBD = await _context.Remeras.FindAsync(ID);
                if (remeraBD != null)
                {
                    remeraBD.CorteCuello.CCID = remeraDTO.CorteCuelloID;
                    remeraBD.TAID = remeraDTO.TalleAlfabeticoID;
                    remeraBD.Manga.MangaID = remeraDTO.MangaID;
                    remeraBD.ColorID = remeraDTO.ColorID;
                    remeraBD.MaterialID = remeraDTO.MaterialID;
                    remeraBD.UsuarioID = remeraDTO.UsuarioID;
                    remeraBD.ImagenID = remeraDTO.ImagenID;
                    remeraBD.RubroID = remeraDTO.RubroID;
                    remeraBD.Precio = remeraDTO.Precio;
                    remeraBD.Nombre = remeraDTO.Nombre;
                    remeraBD.EstampadoID = remeraDTO.EstampadoID;
                    remeraBD.PostID = remeraDTO.PostID;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = remeraBD.Adapt<RemeraDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El remera fué modificada correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El remera no existe.";
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
