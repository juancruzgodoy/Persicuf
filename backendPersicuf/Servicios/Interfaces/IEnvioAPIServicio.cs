using CORE.DTOs;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    public interface IEnvioAPIServicio
    {
        Task<Confirmacion<string>> CrearEnvio(EnvioDTO nuevoEnvio);
    }
}
