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
    public class DomicilioController : ControllerBase
    {
        private readonly IDomicilioServicio _servicio;

        public DomicilioController(IDomicilioServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarDomicilio")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<DomicilioDTO>>> modificarDomicilio(int ID, DomicilioDTO domicilioDTO)
        {
            var respuesta = await _servicio.PutDomicilio(ID, domicilioDTO);
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

        [HttpPost("crearDomicilio")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<DomicilioDTO>>> crearDomicilio(DomicilioDTO domicilioDTO)
        {
            var respuesta = await _servicio.PostDomicilio(domicilioDTO);
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


        [HttpGet("obtenerDomicilios")]
        public async Task<ActionResult<Confirmacion<ICollection<DomicilioDTOconID>>>> obtenerDomicilios()
        {
            var respuesta = await _servicio.GetDomicilio();
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

        [HttpDelete("eliminarDomicilio")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Domicilio>>> eliminarDomicilio(int ID)
        {
            var respuesta = await _servicio.DeleteDomicilio(ID);
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
