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
    public class LocalidadController : ControllerBase
    {
        private readonly ILocalidadServicio _servicio;

        public LocalidadController(ILocalidadServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarLocalidad")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<LocalidadDTO>>> modificarLocalidad(int ID, LocalidadDTO localidadDTO)
        {
            var respuesta = await _servicio.PutLocalidad(ID, localidadDTO);
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

        [HttpPost("crearLocalidad")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<LocalidadDTO>>> crearLocalidad(LocalidadDTO localidadDTO)
        {
            var respuesta = await _servicio.PostLocalidad(localidadDTO);
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


        [HttpGet("obtenerLocalidades")]
        public async Task<ActionResult<Confirmacion<ICollection<LocalidadDTOconID>>>> obtenerLocalidades()
        {
            var respuesta = await _servicio.GetLocalidad();
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

        [HttpDelete("eliminarLocalidad")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Localidad>>> eliminarLocalidad(int ID)
        {
            var respuesta = await _servicio.DeleteLocalidad(ID);
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