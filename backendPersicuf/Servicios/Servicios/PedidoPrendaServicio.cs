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
    public class PedidoPrendaServicio : IPedidoPrendaServicio
    {
        private readonly IDomicilioServicio _domicilioServicio;
        private readonly IPedidoServicio _pedidoServicio;
        private readonly IEnvioAPIServicio _envioAPIServicio;
        private readonly PersicufContext _context;
        

        public PedidoPrendaServicio(PersicufContext context, IDomicilioServicio domicilioServicio, IPedidoServicio pedidoServicio, IEnvioAPIServicio envioAPIServicio)
        {
            _context = context;
            _domicilioServicio = domicilioServicio;
            _pedidoServicio = pedidoServicio;
            _envioAPIServicio = envioAPIServicio;
        }
        public async Task<Confirmacion<PedidoPrenda>> DeletePedidoPrenda(int ID)
        {

            var respuesta = new Confirmacion<PedidoPrenda>();
            respuesta.Datos = null;

            try
            {
                var pedidoPrendaDB = await _context.PedidosPrenda.FindAsync(ID);
                if (pedidoPrendaDB != null)
                {
                    _context.PedidosPrenda.Remove(pedidoPrendaDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = pedidoPrendaDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El PedidoPrenda con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro el PedidoPrenda con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }




        public async Task<Confirmacion<ICollection<PedidoPrendaDTOconID>>> GetPedidoPrenda()
        {
            var respuesta = new Confirmacion<ICollection<PedidoPrendaDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var pedidosPrendaDB = await _context.PedidosPrenda.ToListAsync();
                if (pedidosPrendaDB.Count() != 0)
                {
                    respuesta.Datos = new List<PedidoPrendaDTOconID>();
                    foreach (var pedidoPrenda in pedidosPrendaDB)
                    {
                        respuesta.Datos.Add(new PedidoPrendaDTOconID()
                        {
                            ID = pedidoPrenda.PPID,
                            Cantidad = pedidoPrenda.Cantidad,
                            PrendaID = pedidoPrenda.PrendaID,
                            PedidoID = pedidoPrenda.PedidoID,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los PedidosPrenda";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen PedidosPrenda";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }



        public async Task<Confirmacion<PedidoPrendaDTO>> PostPedidoPrenda(PedidoPrendaDTO pedidoPrendaDTO)
        {
            var respuesta = new Confirmacion<PedidoPrendaDTO>();
            respuesta.Datos = null;

            try
            {
                var pedidoPrendaDB = await _context.PedidosPrenda.AsNoTracking().FirstOrDefaultAsync(x => x.PedidoID == pedidoPrendaDTO.PedidoID && x.PrendaID == pedidoPrendaDTO.PrendaID);
                if (pedidoPrendaDB == null)
                {
                    var pedidoPrendaNuevo = pedidoPrendaDTO.Adapt<PedidoPrenda>();
                    await _context.PedidosPrenda.AddAsync(pedidoPrendaNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El PedidoPrenda se creó correctamente.";
                    respuesta.Datos = pedidoPrendaDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El PedidoPrenda ya existe.";
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

        public async Task<Confirmacion<string>> PostPedidoCliente(PedidoClienteDTO pedidoClienteDTO)
        {
            var respuesta = new Confirmacion<string>();
            respuesta.Datos = null;

            try
            {
                var remerasDB = await _context.Remeras.Where(p => p.Nombre == pedidoClienteDTO.NombreEmpresa + " " + pedidoClienteDTO.Talle + " " + pedidoClienteDTO.Manga).FirstOrDefaultAsync();
                if (remerasDB != null)
                {
                    var domiciliosDB = await _context.Domicilios.Where(p =>
                        p.Calle == pedidoClienteDTO.domicilio.Calle &&
                        p.Numero == pedidoClienteDTO.domicilio.Numero &&
                        p.Piso == pedidoClienteDTO.domicilio.Piso &&
                        p.Depto == pedidoClienteDTO.domicilio.Depto &&
                        p.LocalidadID == pedidoClienteDTO.domicilio.LocalidadID).FirstOrDefaultAsync();

                    if (domiciliosDB == null)
                    {
                        await _domicilioServicio.PostDomicilio(pedidoClienteDTO.domicilio);
                        domiciliosDB = await _context.Domicilios.Where(d => d.UsuarioID == pedidoClienteDTO.domicilio.UsuarioID).OrderByDescending(d => d.DomicilioID).FirstOrDefaultAsync();
                        await _context.SaveChangesAsync();

                    }

                    var pedidoNuevo = new PedidoDTO()
                    {
                        PrecioTotal = remerasDB.Precio,
                        DomicilioID = domiciliosDB.DomicilioID,
                        UsuarioID = pedidoClienteDTO.domicilio.UsuarioID,
                    };

                    await _pedidoServicio.PostPedido(pedidoNuevo);
                    var pedido = await _context.Pedidos.Where(p => p.UsuarioID == pedidoClienteDTO.domicilio.UsuarioID).OrderByDescending(p => p.PedidoID) 
                    .FirstOrDefaultAsync();

                    var pedidoPrenda = new PedidoPrendaDTO()
                    {
                        Cantidad = 1,
                        PrendaID = remerasDB.PrendaID,
                        PedidoID = pedido.PedidoID,
                    };

                    await PostPedidoPrenda(pedidoPrenda);

                    var nuevoEnvio = new EnvioDTO
                    {
                        descripcion = "Envío de prenda Persicuf Nro: " + pedido.PedidoID,
                        hora = "15:30",
                        pesoGramos = 230,
                        reserva = true,
                        origen = new Direccion
                        {
                            calle = "52",
                            numero = 777,
                            piso = 0,
                            depto = "",
                            descripcion = "Fabrica Persicuf",
                            localidadID = 5
                        },
                        destino = new Direccion
                        {
                            calle = domiciliosDB.Calle,
                            numero = domiciliosDB.Numero,
                            piso = domiciliosDB.Piso,
                            depto = domiciliosDB.Depto,
                            descripcion = domiciliosDB.Descripcion,
                            localidadID = 5
                        },
                        cliente = "850cdde8-591e-413d-8e67-48c649a8650f"
                    };

                    var respuestaEnvio = await _envioAPIServicio.CrearEnvio(nuevoEnvio);


                    if (string.IsNullOrEmpty(respuestaEnvio.Datos))
                    {
                        return new Confirmacion<string> { Mensaje = respuestaEnvio.Mensaje };
                    }

                    string nroSeguimiento = respuestaEnvio.Datos;


                    var pedido2 = new PedidoDTO()
                    {
                        PrecioTotal = remerasDB.Precio,
                        DomicilioID = domiciliosDB.DomicilioID,
                        UsuarioID = pedidoClienteDTO.domicilio.UsuarioID,
                        NroSeguimiento = int.Parse(nroSeguimiento),
                    };

                    await _pedidoServicio.PutPedido(pedido.PedidoID, pedido2);

                    respuesta.Datos = "";
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Pedido creado con éxito!";
                    return respuesta;
                }
                else
                {
                    respuesta.Mensaje = "No existe la remera";
                    return (respuesta);
                }
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

        public async Task<Confirmacion<PedidoPrendaDTO>> PutPedidoPrenda(int ID, PedidoPrendaDTO pedidoprendaDTO)
        {
            var respuesta = new Confirmacion<PedidoPrendaDTO>();
            respuesta.Datos = null;

            try
            {
                var pedidoprendaBD = await _context.PedidosPrenda.FindAsync(ID);
                if (pedidoprendaBD != null)
                {
                    pedidoprendaBD.Cantidad = pedidoprendaDTO.Cantidad;
                    pedidoprendaBD.PrendaID = pedidoprendaDTO.PrendaID;
                    pedidoprendaBD.PedidoID = pedidoprendaDTO.PedidoID;



                    await _context.SaveChangesAsync();
                    respuesta.Datos = pedidoprendaBD.Adapt<PedidoPrendaDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El PedidoPrenda fue modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El PedidoPrenda no existe.";
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