using Essalud.Application.Feature.Interfaces;
using Essalud.Domain;
using Essalud.Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Application.Feature.Services
{
    public class PacientesService : IPacientesService
    {
        private readonly IPacientesRepository _pacientesRepository;

        public PacientesService(IPacientesRepository pacientesRepository)
        {
            _pacientesRepository = pacientesRepository;
        }

        public async Task<Paciente> BuscarUsuarioPorDni(Paciente paciente)
        {
            Paciente pacienteObj = await _pacientesRepository.BuscarUsuarioPorDni(paciente);
            if (pacienteObj.IdPaciente == 0)
                throw new Exception("No se encontró ningun usuario");

            return pacienteObj;
        }
    }
}
