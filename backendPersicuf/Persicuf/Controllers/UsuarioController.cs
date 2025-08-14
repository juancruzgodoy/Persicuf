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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServicio _servicio;

        public UsuarioController(IUsuarioServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarUsuario")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<UsuarioDTO>>> modificarUsuario(int ID, UsuarioDTO usuarioDTO)
        {
            var respuesta = await _servicio.PutUsuario(ID, usuarioDTO);
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

        [HttpPost("crearUsuario")]
        public async Task<ActionResult<Confirmacion<UsuarioDTO>>> crearUsuario(UsuarioDTO usuarioDTO)
        {
            var respuesta = await _servicio.PostUsuario(usuarioDTO);
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


        [HttpGet("obtenerUsuarios")]
        public async Task<ActionResult<Confirmacion<ICollection<UsuarioDTOconID>>>> obtenerUsuarios()
        {
            var respuesta = await _servicio.GetUsuario();
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


        [HttpGet("BuscarUsuario")]
        public async Task<ActionResult<Confirmacion<ICollection<UsuarioDTOconID>>>> BuscarUsuario(int ID)
        {
            var respuesta = await _servicio.BuscarUsuario(ID);
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


        [HttpDelete("eliminarUsuario")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Confirmacion<Usuario>>> eliminarUsuario(int ID)
        {
            var respuesta = await _servicio.DeleteUsuario(ID);
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

        [HttpPatch("modificarPermisoUsuario")]
        [Authorize]
        public async Task<ActionResult<Confirmacion<UsuarioDTOconID>>> modificarUsuarioRol(int ID, int PermisoID)
        {
            var respuesta = await _servicio.PatchUsuarioPermiso(ID, PermisoID);
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

        //POST: UsuarioController/login
        [HttpPost("login")]
        public async Task<ActionResult<RespuestaPrivada<LoginUsuarioConRolDTO>>> Login(LoginUsuarioDTO loginUsuario)
        {
            var respuesta = await _servicio.AutenticarUsuario(loginUsuario);

            if (respuesta.Datos == null)
                if (respuesta.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                else if (respuesta.Mensaje.StartsWith("Usuario no encontrado"))
                {
                    return Unauthorized(new { message = "Usuario incorrecto" });

                }
                else
                {
                    return Unauthorized(new { message = "Contraseña Incorrecta" });
                }

            return Ok(new
            {
                message = "Login exitoso",
                userData = new
                {
                    nombreUsuario = respuesta.Datos.LoginUsuario.NombreUsuario,
                    id = respuesta.Datos.Id,
                    rol = respuesta.Datos.Rol,
                    token = respuesta.Datos.Token
                }
            });
        }

        //POST: UsuarioController/register
        [HttpPost("register")]
        public async Task<ActionResult<RespuestaPrivada<RegisterUsuarioDTO>>> Register(RegisterUsuarioDTO registerUsuario)
        {
            var respuesta = await _servicio.RegistrarUsuario(registerUsuario);

            if (respuesta.Datos == null)
                if (respuesta.Mensaje.StartsWith("Error interno"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                else
                {
                    return Unauthorized(new { message = respuesta.Mensaje });

                }
            return Ok(new { message = respuesta.Mensaje, data = respuesta.Datos });
        }

        //POST: UsuarioController/logout
        [HttpPost("logout")]

        public IActionResult Logout()
        {

            return Ok(new { message = "Logout exitoso" });
        }
    }

}