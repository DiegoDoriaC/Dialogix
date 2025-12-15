using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Dialogix.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Services
{
    public class ConfiguracionChatbotService : IConfiguracionChatbotService
    {
        private readonly IConfiguracionChatbotRepository _repository;
        private readonly IActividadAdminService _actividadAdminService;

        public ConfiguracionChatbotService(
            IConfiguracionChatbotRepository repository,
            IActividadAdminService actividadAdminService
        )        {

            _repository = repository;
            _actividadAdminService = actividadAdminService;
        }


        public async Task<ConfiguracionChatbot> ObtenerConfiguracionAsync()
        {
            var config = await _repository.ObtenerConfiguracionAsync();
            return config ?? new ConfiguracionChatbot(); 
        }

        public async Task<bool> ActualizarConfiguracionAsync(
    ConfiguracionChatbot config,
    int idAdmin)
        {
            await _repository.ActualizarConfiguracionAsync(config);

            await _actividadAdminService.RegistrarActividad(
                idAdmin,
                "Configuración Chatbot", 
                "Actualización de configuración",  
                "Se actualizaron los mensajes, horarios y límites del chatbot." 
            );

            return true;
        }

    }
}