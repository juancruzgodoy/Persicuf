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
    public class TalleAlfabeticoController : ControllerBase
    {
        private readonly ITalleAlfabeticoServicio _servicio;

        public TalleAlfabeticoController(ITalleAlfabeticoServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarTalleAlfabetico")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<TalleAlfabeticoDTO>>> modificarTalleAlfabetico(int ID, TalleAlfabeticoDTO talleAlfabeticoDTO)
        {
            var respuesta = await _servicio.PutTalleAlfabetico(ID, talleAlfabeticoDTO);
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

        [HttpPost("crearTalleAlfabetico")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<TalleAlfabeticoDTO>>> crearTalleAlfabetico(TalleAlfabeticoDTO talleAlfabeticoDTO)
        {
            var respuesta = await _servicio.PostTalleAlfabetico(talleAlfabeticoDTO);
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


        [HttpGet("obtenerTallesAlfabetico")]
        public async Task<ActionResult<Confirmacion<ICollection<TalleAlfabeticoDTOconID>>>> obtenerTallesAlfabetico()
        {
            var respuesta = await _servicio.GetTalleAlfabetico();
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

        [HttpDelete("eliminarTalleAlfabetico")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<TalleAlfabetico>>> eliminarTalleAlfabetico(int ID)
        {
            var respuesta = await _servicio.DeleteTalleAlfabetico(ID);
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