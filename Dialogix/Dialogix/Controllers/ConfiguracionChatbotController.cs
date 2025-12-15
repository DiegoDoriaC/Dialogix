using Dialogix.Application.Common.DTOs;
using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Dialogix.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Dialogix.Controllers
{
    [Controller]
    [Route("Dialogix/[controller]")]
    public class ConfiguracionChatbotController : Controller
    {
        private readonly IConfiguracionChatbotService _service;

        public ConfiguracionChatbotController(
            IConfiguracionChatbotService service)
        {
            _service = service;
        }

       
        [HttpGet("obtener")]
        public async Task<RespuestaGenerica<ConfiguracionChatbot>> Obtener()
        {
            var response = new RespuestaGenerica<ConfiguracionChatbot>();

            try
            {
                response.ObjetoRespuesta =
                    await _service.ObtenerConfiguracionAsync();

                response.Estado = true;
                response.Mensaje = "Configuración obtenida correctamente";
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Mensaje = ex.Message;
                response.ObjetoRespuesta = new ConfiguracionChatbot();
            }

            return response;
        }


        [HttpPut("actualizar")]
        public async Task<RespuestaGenerica<bool>> Actualizar(
            [FromBody] ConfiguracionChatbot config)
        {
            var response = new RespuestaGenerica<bool>();

            try
            {
                var admin =
                    HttpContext.Session.GetObject<Usuario>("admin");

                int idAdmin = admin?.IdUsuario ?? 0;

                if (idAdmin == 0)
                {
                    response.Estado = false;
                    response.Mensaje = "Sesión de administrador no válida";
                    response.ObjetoRespuesta = false;
                    return response;
                }
                response.ObjetoRespuesta =
    await _service.ActualizarConfiguracionAsync(config, idAdmin);

                if (response.ObjetoRespuesta)
                {
                    HttpContext.Session.Remove("OUser");
                }

                response.Estado = response.ObjetoRespuesta;
                response.Mensaje = response.ObjetoRespuesta
                    ? "Configuración actualizada correctamente"
                    : "No se pudo actualizar la configuración";

            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Mensaje = ex.Message;
                response.ObjetoRespuesta = false;
            }

            return response;
        }
    }
}
