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
        Task<bool> RegistrarPreguntasFrecuentes(PreguntasFrecuentes pre, int idUsuario);
        Task<bool> ModificarPreguntaFrecuente(PreguntasFrecuentes pre, int idUsuario);
        Task<bool> EliminarPreguntaFrecuentes(int idPreguntaFrecuente, int idUsuario);
    }
}
