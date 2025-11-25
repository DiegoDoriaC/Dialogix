using Dialogix.Application.Features.Interfaces;
using Dialogix.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Services
{
    public class ChatbotService: IChatbotService
    {
        private readonly IOpenAIRepository _openAIRepository;

        public ChatbotService(IOpenAIRepository openAIRepository)
        {
            _openAIRepository = openAIRepository;
        }

        public async Task<string> EnviarMensajeAsync(string mensaje)
        {
            string respuesta = await _openAIRepository.GenerarRespuestaAsync(mensaje);
            return respuesta;
        }

    }
}
