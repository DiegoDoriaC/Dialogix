using Dialogix.Application.Features.Interfaces;
using Essalud.Application.Feature.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dialogix.Controllers
{
    [ApiController]
    [Route("Dialogix/Paciente")]
    public class PacienteController : Controller
    {
        private readonly IChatbotService _chatbotService;

        public PacienteController(IChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpGet(Name = "Conversar")]
        public async Task<IActionResult> EnviarMensaje(string mensaje)
        {
            var respuesta = await _chatbotService.EnviarMensajeAsync(mensaje);
            return Ok(new { respuesta });
        }

    }
}
