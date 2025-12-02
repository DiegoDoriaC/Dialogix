using Dialogix.Application.Common.DTOs;
using Dialogix.Application.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Services
{
    public class ConversacionService : IConversacionService
    {
        public Task<string> IniciarConversacion()
        {
            return Task.FromResult("👋 ¡Hola! Soy tu asistente virtual de salud. " +
                    "Por favor ingresa tu DNI para continuar:");
        }

        public Task<string> ReiterarInicioConversacion()
        {
            return Task.FromResult("Por favor, ingrese su DNI para validarlo en el sistema");
        }

        public Task<string> IniciarConversacionConLaIA()
        {
            return Task.FromResult("Hola!, soy tu asistente virtual. Puedes hacer consultas sobre EsSalud, " +
                "citas, resultados o servicios institucionales. Para volver al menu principal escribe menú");
        }

    }
}
