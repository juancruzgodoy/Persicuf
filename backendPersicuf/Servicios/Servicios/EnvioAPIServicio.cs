using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CORE.DTOs;
using DB.Data;
using Newtonsoft.Json;
using Servicios.Interfaces;

namespace Servicios.Servicios
{
    public class EnvioAPIServicio : IEnvioAPIServicio
    {
        private readonly PersicufContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public EnvioAPIServicio(PersicufContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Confirmacion<string>> CrearEnvio(EnvioDTO nuevoEnvio)
        {
            var respuesta = new Confirmacion<string>();
            var url = "https://veloway-backend-dahf.onrender.com/api/envios/create";
            var token = "kc3lpz8ukoppvaaq1z5vzoyy4ckqzojqgb693gwv";

            try
            {
                if (nuevoEnvio == null ||
                    string.IsNullOrEmpty(nuevoEnvio.descripcion) ||
                    string.IsNullOrEmpty(nuevoEnvio.hora) ||
                    nuevoEnvio.pesoGramos <= 0 ||
                    nuevoEnvio.origen == null ||
                    nuevoEnvio.destino == null ||
                    string.IsNullOrEmpty(nuevoEnvio.cliente))
                {
                    respuesta.Mensaje = "Error: Faltan datos obligatorios en el envío.";
                    return respuesta;
                }

                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Add("origin", "https://localhost:7050/");
                client.DefaultRequestHeaders.Add("x-requested-with", "XMLHttpRequest");

                // Serializar con manejo de valores nulos
                var jsonEnvio = JsonConvert.SerializeObject(nuevoEnvio, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                Console.WriteLine("JSON Enviado: " + jsonEnvio); // Log para depuración

                var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Respuesta API: " + responseBody); // Log para depuración

                if (response.IsSuccessStatusCode)
                {
                    dynamic responseData = JsonConvert.DeserializeObject(responseBody);
                    string nroSeguimiento = responseData?.nroSeguimiento;

                    respuesta.Datos = nroSeguimiento;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Envío creado correctamente con número de seguimiento: " + nroSeguimiento;
                }
                else
                {
                    respuesta.Mensaje = $"Error en la creación del envío. Código: {response.StatusCode}. Detalles: {responseBody}";
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error en la llamada a la API: " + ex.Message;
                if (ex.InnerException != null)
                {
                    respuesta.Mensaje += " Inner Exception: " + ex.InnerException.Message;
                }
            }

            return respuesta;
        }
    }
}
