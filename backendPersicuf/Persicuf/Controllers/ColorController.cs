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
    public class ColorController : ControllerBase
    {
        private readonly IColorServicio _servicio;

        public ColorController(IColorServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarColor")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<ColorDTO>>> modificarColor(int ID, ColorDTO colorDTO)
        {
            var respuesta = await _servicio.PutColor(ID, colorDTO);
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

        [HttpGet("buscarColorPorID")]
        public async Task<ActionResult<Confirmacion<ICollection<ColorDTOconID>>>> BuscarColorPorID([FromQuery] int ID)
        {
            var respuesta = await _servicio.BuscarColorPorID(ID);
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

        [HttpPost("crearColor")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<ColorDTO>>> crearColor(ColorDTO colorDTO)
        {
            var respuesta = await _servicio.PostColor(colorDTO);
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


        [HttpGet("obtenerColores")]
        public async Task<ActionResult<Confirmacion<ICollection<ColorDTOconID>>>> obtenerColores()
        {
            var respuesta = await _servicio.GetColor();
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

        [HttpDelete("eliminarColor")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Color>>> eliminarColor(int ID)
        {
            var respuesta = await _servicio.DeleteColor(ID);
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