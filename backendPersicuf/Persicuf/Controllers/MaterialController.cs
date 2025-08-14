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
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialServicio _servicio;

        public MaterialController(IMaterialServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarMaterial")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<MaterialDTO>>> modificarMaterial(int ID, MaterialDTO materialDTO)
        {
            var respuesta = await _servicio.PutMaterial(ID, materialDTO);
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

        [HttpGet("buscarMaterialPorID")]
        public async Task<ActionResult<Confirmacion<ICollection<MaterialDTOconID>>>> BuscarMaterialPorID([FromQuery] int ID)
        {
            var respuesta = await _servicio.BuscarMaterialPorID(ID);
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

        [HttpPost("crearMaterial")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<MaterialDTO>>> crearMaterial(MaterialDTO materialDTO)
        {
            var respuesta = await _servicio.PostMaterial(materialDTO);
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


        [HttpGet("obtenerMateriales")]
        
        public async Task<ActionResult<Confirmacion<ICollection<MaterialDTOconID>>>> obtenerMateriales()
        {
            var respuesta = await _servicio.GetMaterial();
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


        [HttpDelete("eliminarMaterial")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Material>>> eliminarMaterial(int ID)
        {
            var respuesta = await _servicio.DeleteMaterial(ID);
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