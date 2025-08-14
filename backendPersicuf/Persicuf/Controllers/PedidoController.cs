using CORE.DTOs;
using DB.Data;
using DB.Models;
using Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Persicuf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoServicio _servicio;

        public PedidoController(IPedidoServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarPedido")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<PedidoDTO>>> modificarPedido(int ID, PedidoDTO pedidoDTO)
        {
            var respuesta = await _servicio.PutPedido(ID, pedidoDTO);
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }

        [HttpPost("crearPedido")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<PedidoDTO>>> crearPedido(PedidoDTO pedidoDTO)
        {
            var respuesta = await _servicio.PostPedido(pedidoDTO);
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return BadRequest(respuesta);
            }
            return StatusCode(StatusCodes.Status201Created, respuesta);
        }


        [HttpGet("obtenerPedidos")]
        public async Task<ActionResult<Confirmacion<ICollection<PedidoDTOconID>>>> obtenerPedidos()
        {
            var respuesta = await _servicio.GetPedido();
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }

        [HttpGet("obtenerPedidosUsuario")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<ICollection<PedidoDTOconID>>>> obtenerPedidosUsuario([FromQuery] int ID)
        {
            var respuesta = await _servicio.GetPedidoUsuario(ID);
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }

        [HttpDelete("eliminarPedido")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Pedido>>> eliminarPedido(int ID)
        {
            var respuesta = await _servicio.DeletePedido(ID);
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return NotFound(respuesta);
            }
            return Ok(respuesta);
        }

    }

}