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
    public class MangaController : ControllerBase
    {
        private readonly IMangaServicio _servicio;

        public MangaController(IMangaServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarManga")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<MangaDTO>>> modificarManga(int ID, MangaDTO mangaDTO)
        {
            var respuesta = await _servicio.PutManga(ID, mangaDTO);
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

        [HttpPost("crearManga")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<MangaDTO>>> crearManga(MangaDTO mangaDTO)
        {
            var respuesta = await _servicio.PostManga(mangaDTO);
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


        [HttpGet("obtenerMangas")]
        public async Task<ActionResult<Confirmacion<ICollection<MangaDTOconID>>>> obtenerMangas()
        {
            var respuesta = await _servicio.GetManga();
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


        [HttpDelete("eliminarManga")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Manga>>> eliminarManga(int ID)
        {
            var respuesta = await _servicio.DeleteManga(ID);
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