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
    public class ImagenController : ControllerBase
    {
        private readonly IImagenServicio _servicio;

        public ImagenController(IImagenServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarImagen")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<ImagenDTO>>> modificarImagen(int ID, ImagenDTO imagenDTO)
        {
            var respuesta = await _servicio.PutImagen(ID, imagenDTO);
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

        [HttpPost("crearImagen")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<ImagenDTO>>> crearImagen([FromBody] ImagenDTO imagenDTO)
        {
            if (imagenDTO?.Path == null || imagenDTO.Path.Length == 0)
            {
                return BadRequest("No image data provided.");
            }

            var respuesta = await _servicio.PostImagen(imagenDTO);
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


        [HttpGet("obtenerImagenes")]
        public async Task<ActionResult<Confirmacion<ICollection<ImagenDTOconID>>>> obtenerImagenes()
        {
            var respuesta = await _servicio.GetImagen();
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

        [HttpDelete("eliminarImagen")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Imagen>>> eliminarImagen(int ID)
        {
            var respuesta = await _servicio.DeleteImagen(ID);
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
