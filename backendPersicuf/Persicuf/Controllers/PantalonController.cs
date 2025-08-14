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
    public class PantalonController : ControllerBase
    {
        private readonly IPantalonServicio _servicio;

        public PantalonController(IPantalonServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarPantalon")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<PantalonDTO>>> modificarPantalon(int ID, PantalonDTO pantalonDTO)
        {
            var respuesta = await _servicio.PutPantalon(ID, pantalonDTO);
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

        [HttpPost("crearPantalon")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<PantalonDTO>>> crearPantalon(PantalonDTO pantalonDTO)
        {
            var respuesta = await _servicio.PostPantalon(pantalonDTO);
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


        [HttpGet("obtenerPantalones")]
        public async Task<ActionResult<Confirmacion<ICollection<PantalonDTOconID>>>> obtenerPantalones()
        {
            var respuesta = await _servicio.GetPantalon();
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

        [HttpGet("buscarPantalones")]
        public async Task<ActionResult<Confirmacion<ICollection<PantalonDTOconID>>>> buscarPantalones([FromQuery] string busqueda)
        {
            var respuesta = await _servicio.BuscarPantalones(busqueda);
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


        [HttpDelete("eliminarPantalon")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Pantalon>>> eliminarPantalon(int ID)
        {
            var respuesta = await _servicio.DeletePantalon(ID);
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
