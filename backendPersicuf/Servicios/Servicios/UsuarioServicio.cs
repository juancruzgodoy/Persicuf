using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using DB.Models;
using DB.Data;
using CORE.DTOs;
using Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Servicios.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly PersicufContext _context;

        private readonly IJWT _jwt;
        public UsuarioServicio(PersicufContext context, IJWT jwt)
        {
            _context = context;
            _jwt = jwt;
        }
        public async Task<Confirmacion<Usuario>> DeleteUsuario(int ID)
        {

            var respuesta = new Confirmacion<Usuario>();
            respuesta.Datos = null;

            try
            {
                var usuarioDB = await _context.Usuarios.FindAsync(ID);
                if (usuarioDB != null)
                {
                    _context.Usuarios.Remove(usuarioDB);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = usuarioDB;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El usuario con ID: " + ID + "se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "No se encontro el usuario con ID:" + ID;
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error:" + ex.Message;
                return respuesta;
            }
        }


    

        public async Task<Confirmacion<ICollection<UsuarioDTOconID>>> GetUsuario()
        {
            var respuesta = new Confirmacion<ICollection<UsuarioDTOconID>>();
            respuesta.Datos = null;

            try
            {
                var usuariosDB = await _context.Usuarios.ToListAsync();
                if (usuariosDB.Count() != 0)
                {
                    respuesta.Datos = new List<UsuarioDTOconID>();
                    foreach (var usuario in usuariosDB)
                    {
                        respuesta.Datos.Add(new UsuarioDTOconID()
                        {
                            ID = usuario.UsuarioID,
                            NombreUsuario = usuario.NombreUsuario,
                            Contrasenia = usuario.Contrasenia,
                            PermisoID = usuario.PermisoID,
                            Correo = usuario.Correo,
                        });
                    }
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron todos los usuarios";
                    return respuesta;
                }

                respuesta.Mensaje = "No existen usuarios";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<Confirmacion<UsuarioDTOconID>> BuscarUsuario(int ID)
        {
            var respuesta = new Confirmacion<UsuarioDTOconID>();
            respuesta.Datos= null;

            try
            {
                var usuarioDB = await _context.Usuarios.FindAsync(ID);

                if (usuarioDB != null)
                {
                    respuesta.Datos = usuarioDB.Adapt<UsuarioDTOconID>();
                    respuesta.Datos.ID = usuarioDB.UsuarioID;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El usuario con ID: " + ID + " ha sido encontrado.";
                }
                else
                {
                    respuesta.Mensaje = "No se encontró el usuario con ID: " + ID;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
            }

            return respuesta;
        }




        public async Task<Confirmacion<UsuarioDTOconID>> PatchUsuarioPermiso(int ID, int PermID)
        {
            var respuesta = new Confirmacion<UsuarioDTOconID>();
            respuesta.Datos = null;

            try
            {
                var usuarioDB = await _context.Usuarios.FirstOrDefaultAsync(x => x.UsuarioID == ID);
                if (usuarioDB != null)
                {
                    usuarioDB.PermisoID = PermID;
                    await _context.SaveChangesAsync();
                    respuesta.Datos = usuarioDB.Adapt<UsuarioDTOconID>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El permiso del usuario fue actualizado.";
                    return respuesta;
                }
                respuesta.Mensaje = "No se ha encontrado al usuario";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return (respuesta);
            }
        }





        public async Task<Confirmacion<UsuarioDTO>> PostUsuario(UsuarioDTO usuarioDTO)
        {
            var respuesta = new Confirmacion<UsuarioDTO>();
            respuesta.Datos = null;

            try
            {
                var usuarioDB = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.NombreUsuario == usuarioDTO.NombreUsuario);
                if (usuarioDB == null)
                {
                    var usuarioNuevo = usuarioDTO.Adapt<Usuario>(); 
                    await _context.Usuarios.AddAsync(usuarioNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El usuario se creó correctamente.";
                    respuesta.Datos = usuarioDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El usuario ya existe.";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    respuesta.Mensaje += " Inner Exception: " + ex.InnerException.Message;
                }
                return (respuesta);
            }
        }

        public async Task<Confirmacion<UsuarioDTO>> PutUsuario(int ID, UsuarioDTO usuarioDTO)
        {
            var respuesta = new Confirmacion<UsuarioDTO>();
            respuesta.Datos = null;

            try
            {
                var usuarioBD = await _context.Usuarios.FindAsync(ID);
                if (usuarioBD != null)
                {
                    usuarioBD.NombreUsuario = usuarioDTO.NombreUsuario;
                    usuarioBD.Contrasenia = usuarioDTO.Contrasenia;
                    usuarioBD.PermisoID = usuarioDTO.PermisoID;
                    usuarioBD.Correo = usuarioDTO.Correo;

                    await _context.SaveChangesAsync();
                    respuesta.Datos = usuarioBD.Adapt<UsuarioDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El usuario fué modificado correctamente.";
                    return respuesta;
                }
                respuesta.Mensaje = "El usuario no existe.";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error: " + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<LoginUsuarioConRolDTO>> AutenticarUsuario(LoginUsuarioDTO loginUsuario)
        {
            var respuesta = new RespuestaPrivada<LoginUsuarioConRolDTO>();
            respuesta.Datos = null;

            try
            {
                var usuarioBD = await _context.Usuarios
                    .Include(u => u.Permiso) // Asegura que se cargue Permiso
                    .FirstOrDefaultAsync(u => u.NombreUsuario == loginUsuario.NombreUsuario);

                if (usuarioBD == null)
                {
                    respuesta.Mensaje = "Usuario no encontrado";
                    return respuesta;
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginUsuario.Clave, usuarioBD.Contrasenia);

                if (!isPasswordValid)
                {
                    respuesta.Mensaje = "Contraseña incorrecta";
                    return respuesta;
                }

                if (usuarioBD.Permiso == null)
                {
                    respuesta.Mensaje = "Permiso no asignado al usuario";
                    return respuesta;
                }

                var data = new LoginUsuarioConRolDTO
                {
                    Token = _jwt.GenerarToken(usuarioBD),
                    Rol = usuarioBD.Permiso.Descripcion,
                    Id = usuarioBD.UsuarioID,
                    LoginUsuario = loginUsuario
                };

                respuesta.Datos = data;
                respuesta.Exito = true;
                respuesta.Mensaje = "Autenticación exitosa";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno: " + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<RegisterUsuarioDTO>> RegistrarUsuario(RegisterUsuarioDTO registerUsuario)
        {
            var respuesta = new RespuestaPrivada<RegisterUsuarioDTO>();
            respuesta.Datos = null;

            try
            {
                var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario.ToLower() == registerUsuario.NombreUsuario.ToLower());
                if (usuarioExistente != null)
                {
                    respuesta.Mensaje = "El nombre de usuario ya está en uso.";
                    return respuesta;
                }

                var usuarioNuevo = registerUsuario.Adapt<Usuario>();
                usuarioNuevo.PermisoID = 2;
                usuarioNuevo.Contrasenia = BCrypt.Net.BCrypt.HashPassword(registerUsuario.Clave);

                await _context.Usuarios.AddAsync(usuarioNuevo);
                await _context.SaveChangesAsync();

                respuesta.Exito = true;
                respuesta.Mensaje = "El usuario se ha registrado correctamente";
                respuesta.Datos = usuarioNuevo.Adapt<RegisterUsuarioDTO>();
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno: " + ex.Message;
                return respuesta;
            }
        }
    }
}
