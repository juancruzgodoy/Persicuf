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
    public class UbicacionController : ControllerBase
    {
        private readonly IUbicacionServicio _servicio;

        public UbicacionController(IUbicacionServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarUbicacion")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<UbicacionDTO>>> modificarUbicacion(int ID, UbicacionDTO ubicacionDTO)
        {
            var respuesta = await _servicio.PutUbicacion(ID, ubicacionDTO);
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

        [HttpPost("crearUbicacion")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<UbicacionDTO>>> crearUbicacion(UbicacionDTO ubicacionDTO)
        {
            var respuesta = await _servicio.PostUbicacion(ubicacionDTO);
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


        [HttpGet("obtenerUbicaciones")]
        public async Task<ActionResult<Confirmacion<ICollection<UbicacionDTOconID>>>> obtenerUbicaciones()
        {
            var respuesta = await _servicio.GetUbicacion();
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

        [HttpDelete("eliminarUbicacion")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Ubicacion>>> eliminarUbicacion(int ID)
        {
            var respuesta = await _servicio.DeleteUbicacion(ID);
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