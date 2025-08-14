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
    public class LargoController : ControllerBase
    {
        private readonly ILargoServicio _servicio;

        public LargoController(ILargoServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarLargo")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<LargoDTO>>> modificarLargo(int ID, LargoDTO largoDTO)
        {
            var respuesta = await _servicio.PutLargo(ID, largoDTO);
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

        [HttpPost("crearLargo")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<LargoDTO>>> crearLargo(LargoDTO largoDTO)
        {
            var respuesta = await _servicio.PostLargo(largoDTO);
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


        [HttpGet("obtenerLargos")]
        public async Task<ActionResult<Confirmacion<ICollection<LargoDTOconID>>>> obtenerLargos()
        {
            var respuesta = await _servicio.GetLargo();
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

        [HttpDelete("eliminarLargo")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Largo>>> eliminarLargo(int ID)
        {
            var respuesta = await _servicio.DeleteLargo(ID);
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
