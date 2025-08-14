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
    public class TalleNumericoController : ControllerBase
    {
        private readonly ITalleNumericoServicio _servicio;

        public TalleNumericoController(ITalleNumericoServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarTalleNumerico")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<TalleNumericoDTO>>> modificarTalleNumerico(int ID, TalleNumericoDTO talleNumericoDTO)
        {
            var respuesta = await _servicio.PutTalleNumerico(ID, talleNumericoDTO);
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

        [HttpPost("crearTalleNumerico")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<TalleNumericoDTO>>> crearTalleNumerico(TalleNumericoDTO talleNumericoDTO)
        {
            var respuesta = await _servicio.PostTalleNumerico(talleNumericoDTO);
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


        [HttpGet("obtenerTallesNumerico")]
        public async Task<ActionResult<Confirmacion<ICollection<TalleNumericoDTOconID>>>> obtenerTallesNumerico()
        {
            var respuesta = await _servicio.GetTalleNumerico();
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

        [HttpDelete("eliminarTalleNumerico")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<TalleNumerico>>> eliminarTalleNumerico(int ID)
        {
            var respuesta = await _servicio.DeleteTalleNumerico(ID);
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