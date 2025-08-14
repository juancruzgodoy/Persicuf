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
    public class PedidoServicio : IPedidoServicio
    {
        private readonly PersicufContext _context;
        public PedidoServicio(PersicufContext context)
        {
            _context = context;
        }
        public async Task<Confirmacion<Pedido>> DeletePedido(int ID)
        {

            var respuesta = new Confirmacion<Pedido>();
            respuesta.Datos = null;

            try
            {
                var pedidoDB = await _context.Pedidos.FindAsync(ID);
                if (pedidoDB != null)
                {
                    _context.Pedidos.Remove(pedidoDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = pedidoDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El pedido con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro el pedido con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }

        public async Task<Confirmacion<ICollection<PedidoDTOconID>>> GetPedido()
        {
            var respuesta = new Confirmacion<ICollection<PedidoDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var pedidosDB = await _context.Pedidos.ToListAsync();
                if (pedidosDB.Count() != 0)
                {
                    respuesta.Datos = new List<PedidoDTOconID>();
                    foreach (var pedido in pedidosDB)
                    {
                        respuesta.Datos.Add(new PedidoDTOconID()
                        {
                            ID = pedido.PedidoID,
                            PrecioTotal = pedido.PrecioTotal,
                            DomicilioID = pedido.DomicilioID,
                            UsuarioID = pedido.UsuarioID,
                            NroSeguimiento = pedido.NroSeguimiento,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los pedidos";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen pedidos";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }


        public async Task<Confirmacion<ICollection<PedidoDTOconID>>> GetPedidoUsuario(int ID)
        {
            var respuesta = new Confirmacion<ICollection<PedidoDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var pedidosDB = await _context.Pedidos
                .Where(p => p.UsuarioID == ID).OrderBy(p => p.PedidoID).ToListAsync();

                if (pedidosDB.Count() != 0)
                {
                    respuesta.Datos = new List<PedidoDTOconID>();
                    foreach (var pedido in pedidosDB)
                    {
                        respuesta.Datos.Add(new PedidoDTOconID()
                        {
                            ID = pedido.PedidoID,
                            PrecioTotal = pedido.PrecioTotal,
                            DomicilioID = pedido.DomicilioID,
                            UsuarioID = pedido.UsuarioID,
                            NroSeguimiento = pedido.NroSeguimiento,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los pedidos";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen pedidos";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }



        public async Task<Confirmacion<PedidoDTO>> PostPedido(PedidoDTO pedidoDTO)
        {
            var respuesta = new Confirmacion<PedidoDTO>();
            respuesta.Datos = null;

            try
            {

                    var pedidoNuevo = pedidoDTO.Adapt<Pedido>();
                    await _context.Pedidos.AddAsync(pedidoNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El pedido se creó correctamente.";
                    respuesta.Datos = pedidoDTO;
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


        public async Task<Confirmacion<PedidoDTO>> PutPedido(int ID, PedidoDTO pedidoDTO)
        {
            var respuesta = new Confirmacion<PedidoDTO>();
            respuesta.Datos = null;

            try
            {
                var pedidoBD = await _context.Pedidos.FindAsync(ID);
                if (pedidoBD != null)
                {
                    pedidoBD.PrecioTotal = pedidoDTO.PrecioTotal;
                    pedidoBD.DomicilioID = pedidoDTO.DomicilioID;
                    pedidoBD.UsuarioID = pedidoDTO.UsuarioID;
                    pedidoBD.NroSeguimiento = pedidoDTO.NroSeguimiento;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = pedidoBD.Adapt<PedidoDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El pedido fue modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El pedido no existe.";
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
