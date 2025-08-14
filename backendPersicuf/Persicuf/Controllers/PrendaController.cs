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
    public class PrendaController : ControllerBase
    {
        private readonly IPrendaServicio _servicio;

        public PrendaController(IPrendaServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarPrenda")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<PrendaDTO>>> modificarPrenda(int ID, PrendaDTO prendaDTO)
        {
            var respuesta = await _servicio.PutPrenda(ID, prendaDTO);
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

        [HttpPost("crearPrenda")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<PrendaDTO>>> crearPrenda(PrendaDTO prendaDTO)
        {
            var respuesta = await _servicio.PostPrenda(prendaDTO);
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


        [HttpGet("obtenerPrendas")]
        public async Task<ActionResult<Confirmacion<ICollection<PrendaDTOconID>>>> obtenerPrendas()
        {
            var respuesta = await _servicio.GetPrenda();
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

        [HttpGet("buscarPrendas")]
        public async Task<ActionResult<Confirmacion<ICollection<PrendaDTOconID>>>> buscarPrendas([FromQuery] string busqueda)
        {
            var respuesta = await _servicio.BuscarPrenda(busqueda);
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

        [HttpGet("obtenerPrendasUsuario")]
        public async Task<ActionResult<Confirmacion<ICollection<PrendaDTOconID>>>> obtenerPrendasUsuario([FromQuery] int ID)
        {
            var respuesta = await _servicio.GetPrendaUsuario(ID);
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

        [HttpGet("buscarPrendaPorID")]
        public async Task<ActionResult<Confirmacion<ICollection<PrendaDTOconID>>>> BuscarPrendaPorID([FromQuery] int ID)
        {
            var respuesta = await _servicio.BuscarPrendaPorID(ID);
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

        [HttpDelete("eliminarPrenda")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Prenda>>> eliminarPrenda(int ID)
        {
            var respuesta = await _servicio.DeletePrenda(ID);
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