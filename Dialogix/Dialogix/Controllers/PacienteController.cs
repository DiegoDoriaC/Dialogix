using Dialogix.Application.Features.Interfaces;
using Dialogix.ChatBot;
using Dialogix.Domain;
using Dialogix.Helpers;
using Essalud.Application.Feature.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dialogix.Controllers
{
    [ApiController]
    [Route("Dialogix/[controller]")]
    public class PacienteController : Controller
    {
        private readonly IChatbotService _chatbotService;
        private readonly IConversacionService _conversacionService;
        private readonly InteraccionChatbot _chatbotInteraccion;

        public PacienteController(IChatbotService chatbotService, IConversacionService conversacionService, InteraccionChatbot chatbotInteraccion)
        {
            _chatbotService = chatbotService;
            _conversacionService = conversacionService;
            _chatbotInteraccion = chatbotInteraccion;
        }

        [HttpGet("Hablar")]
        public async Task<IActionResult> EnviarMensaje(string mensaje)
        {
            var respuesta = await _chatbotInteraccion.EnviarMensajeAsync(mensaje);
            return Ok(new { respuesta });
        }

    }
}
