using Dialogix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Infrastructure.Repositories
{
    public interface IPreguntasFrecuentesRepository
    {
        Task<List<PreguntasFrecuentes>> ListarPreguntasFrecuentes();
        Task<bool> RegistrarPreguntaFrecuente(PreguntasFrecuentes pre);
        Task<bool> ModificarPreguntaFrecuente(PreguntasFrecuentes pre);
        Task<bool> EliminarPreguntaFrecuente(PreguntasFrecuentes pre);
    }
}
