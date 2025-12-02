using Essalud.Application.Feature.Interfaces;
using Essalud.Domain;
using Essalud.Infraestructure.Repositories.Interfaces;
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

        public async Task<Paciente> BuscarUsuarioPorDni(string dni)
        {
            if(string.IsNullOrWhiteSpace(dni) || dni.Length != 8) 
                throw new Exception("por favor, ingrese su DNI correctamente");

            Paciente paciente = new Paciente();
            paciente.Dni = dni;

            Paciente pacienteObj = await _pacientesRepository.BuscarUsuarioPorDni(paciente);
            if (pacienteObj.IdPaciente == 0)
                throw new Exception("No pudimos encontrarlo en los registros de Essalud, por favor verifique su DNI");

            return pacienteObj;
        }
    }
}
