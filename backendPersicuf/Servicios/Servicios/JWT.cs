
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using CORE.DTOs;
using DB.Data;
using Microsoft.EntityFrameworkCore;
using DB.Models;

namespace Servicios.Servicios
{
    public class JWT : IJWT
    {
        private readonly IConfiguration _configuration;
        private readonly PersicufContext _context;

        public JWT(IConfiguration configuration, PersicufContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string GenerarToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.NombreUsuario),
                new Claim(ClaimTypes.Email, user.Correo),
                new Claim(ClaimTypes.Role, user.Permiso.Descripcion)

            };
            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Get<string>() ?? string.Empty));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}