using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Dialogix.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace Dialogix.Application.Features.Services
{
    public class PreguntasFrecuentesService : IPreguntasFrecuentesService
    {
        private readonly IPreguntasFrecuentesRepository _preguntasFrecuentesRepository;
        private readonly IActividadAdminService _actividadAdminService;

        public PreguntasFrecuentesService(
            IPreguntasFrecuentesRepository preguntasFrecuentesRepository,
            IActividadAdminService actividadAdminService)
        {
            _preguntasFrecuentesRepository = preguntasFrecuentesRepository;
            _actividadAdminService = actividadAdminService;
        }

        public async Task<List<PreguntasFrecuentes>> ListarPreguntasFrecuentes()
        {
            return await _preguntasFrecuentesRepository.ListarPreguntasFrecuentes();
        }

        public async Task<bool> RegistrarPreguntasFrecuentes(PreguntasFrecuentes pre, int idUsuario)
        {
            bool ok = await _preguntasFrecuentesRepository.RegistrarPreguntaFrecuente(pre);

            if (ok)
            {
                Console.WriteLine("ENTRÓ A REGISTRAR ACTIVIDAD FAQ");

                await _actividadAdminService.RegistrarActividad(
                    idUsuario,
                    "Preguntas Frecuentes",
                    "Registró una pregunta frecuente",
                    pre.Descripcion
                );
            }

            return ok;
        }

        public async Task<bool> ModificarPreguntaFrecuente(PreguntasFrecuentes pre, int idUsuario)
        {
            bool ok = await _preguntasFrecuentesRepository.ModificarPreguntaFrecuente(pre);

            if (ok)
            {
                await _actividadAdminService.RegistrarActividad(
                    idUsuario,
                    "Preguntas Frecuentes",
                    "Modificó una pregunta frecuente",
                    pre.Descripcion
                );
            }

            return ok;
        }

        public async Task<bool> EliminarPreguntaFrecuentes(int idPreguntaFrecuente, int idUsuario)
        {
            var pregunta = new PreguntasFrecuentes { IdPreguntaFrecuente = idPreguntaFrecuente };

            bool ok = await _preguntasFrecuentesRepository.EliminarPreguntaFrecuente(pregunta);

            if (ok)
            {
                await _actividadAdminService.RegistrarActividad(
                    idUsuario,
                    "Preguntas Frecuentes",
                    "Eliminó una pregunta frecuente",
                    $"IdPreguntaFrecuente: {idPreguntaFrecuente}"
                );
            }

            return ok;
        }
    }
}