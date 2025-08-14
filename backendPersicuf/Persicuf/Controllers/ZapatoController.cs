using CORE.DTOs;
using DB.Data;
using DB.Models;
using Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Persicuf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZapatoController : ControllerBase
    {
        private readonly IZapatoServicio _servicio;

        public ZapatoController(IZapatoServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarZapato")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<ZapatoDTO>>> modificarZapato(int ID, ZapatoDTO zapatoDTO)
        {
            var respuesta = await _servicio.PutZapato(ID, zapatoDTO);
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

        [HttpPost("crearZapato")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<ZapatoDTO>>> crearZapato(ZapatoDTO zapatoDTO)
        {
            var respuesta = await _servicio.PostZapato(zapatoDTO);
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

        [HttpGet("buscarZapatos")]
        public async Task<ActionResult<Confirmacion<ICollection<ZapatoDTOconID>>>> buscarZapatos([FromQuery] string busqueda)
        {
            var respuesta = await _servicio.BuscarZapatos(busqueda);
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


        [HttpGet("obtenerZapatos")]
        public async Task<ActionResult<Confirmacion<ICollection<ZapatoDTOconID>>>> obtenerZapatos()
        {
            var respuesta = await _servicio.GetZapato();
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

        [HttpDelete("eliminarZapato")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Zapato>>> eliminarZapato(int ID)
        {
            var respuesta = await _servicio.DeleteZapato(ID);
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
