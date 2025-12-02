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
    public class PreguntasFrecuentesService : IPreguntasFrecuentesService
    {
        private readonly IPreguntasFrecuentesRepository _preguntasFrecuentesRepository;

        public PreguntasFrecuentesService(IPreguntasFrecuentesRepository preguntasFrecuentesRepository)
        {
            this._preguntasFrecuentesRepository = preguntasFrecuentesRepository;
        }

        public async Task<bool> EliminarPreguntaFrecuentes(int id)
        {
            PreguntasFrecuentes pregunta = new PreguntasFrecuentes();
            pregunta.IdPreguntaFrecuente = id;
            return await _preguntasFrecuentesRepository.EliminarPreguntaFrecuente(pregunta);
        }

        public async Task<List<Domain.PreguntasFrecuentes>> ListarPreguntasFrecuentes()
        {
            return await _preguntasFrecuentesRepository.ListarPreguntasFrecuentes();
        }

        public async Task<bool> ModificarPreguntaFrecuente(Domain.PreguntasFrecuentes pre)
        {
            return await _preguntasFrecuentesRepository.ModificarPreguntaFrecuente(pre);
        }

        public async Task<bool> RegistrarPreguntasFrecuentes(Domain.PreguntasFrecuentes pre)
        {
            return await _preguntasFrecuentesRepository.RegistrarPreguntaFrecuente(pre);
        }
    }
}
