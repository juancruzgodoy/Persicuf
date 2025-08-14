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
    public class PermisoController : ControllerBase
    {
        private readonly IPermisoServicio _servicio;

        public PermisoController(IPermisoServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarPermiso")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<PermisoDTO>>> modificarPermiso(int ID, PermisoDTO permisoDTO)
        {
            var respuesta = await _servicio.PutPermiso(ID, permisoDTO);
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

        [HttpPost("crearPermiso")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<PermisoDTO>>> crearPermiso(PermisoDTO permisoDTO)
        {
            var respuesta = await _servicio.PostPermiso(permisoDTO);
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


        [HttpGet("obtenerPermisos")]
        public async Task<ActionResult<Confirmacion<ICollection<PermisoDTOconID>>>> obtenerPermisos()
        {
            var respuesta = await _servicio.GetPermiso();
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

        [HttpDelete("eliminarPermiso")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Permiso>>> eliminarPermiso(int ID)
        {
            var respuesta = await _servicio.DeletePermiso(ID);
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