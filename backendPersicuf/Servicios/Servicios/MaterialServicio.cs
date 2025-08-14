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
    public class MaterialServicio : IMaterialServicio
    {
        private readonly PersicufContext _context;
        public MaterialServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Material>> DeleteMaterial(int ID)
        {

            var respuesta = new Confirmacion<Material>();
            respuesta.Datos = null;

            try
            {
                var materialDB = await _context.Materiales.FindAsync(ID);
                if (materialDB != null)
                {
                    _context.Materiales.Remove(materialDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = materialDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El material con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro el material con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }


        public async Task<Confirmacion<ICollection<MaterialDTOconID>>> GetMaterial()
        {
            var respuesta = new Confirmacion<ICollection<MaterialDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var materialDB = await _context.Materiales.ToListAsync();
                if (materialDB.Count() != 0)
                {
                    respuesta.Datos = new List<MaterialDTOconID>();
                    foreach (var Material in materialDB)
                    {
                        respuesta.Datos.Add(new MaterialDTOconID()
                        {
                            ID = Material.MaterialID,
                            Descripcion = Material.Descripcion,
                            Precio = Material.Precio,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los Materiales";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen Material";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }


        public async Task<Confirmacion<MaterialDTO>> PostMaterial(MaterialDTO materialDTO)
        {
            var respuesta = new Confirmacion<MaterialDTO>();
            respuesta.Datos = null;

            try
            {
                var materialDB = await _context.Materiales.AsNoTracking().FirstOrDefaultAsync(x => x.Descripcion == materialDTO.Descripcion);
                if (materialDB == null)
                {
                    var materialNuevo = materialDTO.Adapt<Material>();
                    await _context.Materiales .AddAsync(materialNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El material se creó correctamente.";
                    respuesta.Datos = materialDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El material ya existe.";
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

        public async Task<Confirmacion<MaterialDTOconID>> BuscarMaterialPorID(int ID)
        {
            var respuesta = new Confirmacion<MaterialDTOconID>();

            try
            {
                var materialDB = await _context.Materiales.FirstOrDefaultAsync(m => m.MaterialID == ID);

                if (materialDB != null)
                {
                    respuesta.Datos = new MaterialDTOconID
                    {
                        ID = materialDB.MaterialID,
                        Descripcion = materialDB.Descripcion,

                    };
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Material encontrado exitosamente";
                }
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = $"No se encontró ningún material con el ID {ID}";
                }
            }
            catch (Exception ex)
            {

                respuesta.Exito = false;
                respuesta.Mensaje = "Ocurrió un error al buscar el material: " + ex.Message;
            }

            return respuesta;
        }


        public async Task<Confirmacion<MaterialDTO>> PutMaterial(int ID, MaterialDTO materialDTO)
        {
            var respuesta = new Confirmacion<MaterialDTO>();
            respuesta.Datos = null;

            try
            {
                var materialBD = await _context.Materiales.FindAsync(ID);
                if (materialBD != null)
                {
                    materialBD.Descripcion = materialDTO.Descripcion;
                    materialBD.Precio = materialDTO.Precio;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = materialBD.Adapt<MaterialDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El material fué modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El material no existe.";
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
