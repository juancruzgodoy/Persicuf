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
    public class CorteCuelloController : ControllerBase
    {
        private readonly ICorteCuelloServicio _servicio;

        public CorteCuelloController(ICorteCuelloServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarCorteCuello")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<CorteCuelloDTO>>> modificarCorteCuello(int ID, CorteCuelloDTO corteCuelloDTO)
        {
            var respuesta = await _servicio.PutCorteCuello(ID, corteCuelloDTO);
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

        [HttpPost("crearCorteCuello")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<CorteCuelloDTO>>> crearCorteCuello(CorteCuelloDTO corteCuelloDTO)
        {
            var respuesta = await _servicio.PostCorteCuello(corteCuelloDTO);
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


        [HttpGet("obtenerCortesCuello")]
        public async Task<ActionResult<Confirmacion<ICollection<CorteCuelloDTOconID>>>> obtenerCortesCuello()
        {
            var respuesta = await _servicio.GetCorteCuello();
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

        [HttpDelete("eliminarCorteCuello")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<CorteCuello>>> eliminarCorteCuello(int ID)
        {
            var respuesta = await _servicio.DeleteCorteCuello(ID);
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