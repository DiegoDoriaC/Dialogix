using Dialogix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Interfaces
{
    public interface IConfiguracionChatbotService
    {
        Task<ConfiguracionChatbot> ObtenerConfiguracionAsync();
        Task<bool> ActualizarConfiguracionAsync(ConfiguracionChatbot config, int idAdmin);
    }
}
