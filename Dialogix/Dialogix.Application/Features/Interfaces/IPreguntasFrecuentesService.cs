using Dialogix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Interfaces
{
    public interface IPreguntasFrecuentesService
    {
        Task<List<PreguntasFrecuentes>> ListarPreguntasFrecuentes();
        Task<bool> RegistrarPreguntasFrecuentes(PreguntasFrecuentes pre);
        Task<bool> ModificarPreguntaFrecuente(PreguntasFrecuentes pre);
        Task<bool> EliminarPreguntaFrecuentes(int id);
    }
}
