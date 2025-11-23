using Dialogix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Interfaces
{
    public interface IPreguntasFrecuentes
    {
        Task<List<PreguntasFrecuentes>> ListarPreguntasFrecuentes();
        Task<PreguntasFrecuentes> RegistrarPreguntasFrecuentes();
        Task<PreguntasFrecuentes> EliminarPreguntaFrecuentes();
    }
}
