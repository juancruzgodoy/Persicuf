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
    public class RubroController : ControllerBase
    {
        private readonly IRubroServicio _servicio;

        public RubroController(IRubroServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarRubro")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<RubroDTO>>> modificarRubro(int ID, RubroDTO rubroDTO)
        {
            var respuesta = await _servicio.PutRubro(ID, rubroDTO);
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

        [HttpGet("buscarRubroPorID")]
        public async Task<ActionResult<Confirmacion<ICollection<RubroDTOconID>>>> BuscarRubroPorID([FromQuery] int ID)
        {
            var respuesta = await _servicio.BuscarRubroPorID(ID);
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

        [HttpPost("crearRubro")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<RubroDTO>>> crearRubro(RubroDTO rubroDTO)
        {
            var respuesta = await _servicio.PostRubro(rubroDTO);
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


        [HttpGet("obtenerRubros")]
        public async Task<ActionResult<Confirmacion<ICollection<RubroDTOconID>>>> obtenerRubros()
        {
            var respuesta = await _servicio.GetRubro();
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

        [HttpDelete("eliminarRubro")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Rubro>>> eliminarRubro(int ID)
        {
            var respuesta = await _servicio.DeleteRubro(ID);
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
