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
            Paciente paciente = new Paciente();
            paciente.Dni = dni;

            Paciente pacienteObj = await _pacientesRepository.BuscarUsuarioPorDni(paciente);
            if (pacienteObj.IdPaciente == 0)
                throw new Exception("No pudimos encontrarlo en los registros de Essalud, por favor verifique su DNI");

            return pacienteObj;
        }

        public async Task<string> ComprobarUltimoDigitoDNI(int idPaciente, string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt) || prompt.Length != 1)
                throw new Exception("Por favor, ingrese su código correctamente");

            if(!int.TryParse(prompt, out _))
                throw new Exception("Debe ingresar su código de verificación");

            string ultDigito = await _pacientesRepository.ObtenerUltimoDigitoDni(idPaciente);
            if (ultDigito == prompt) return ultDigito;
            else throw new Exception("El dígito no coincide con el registro en el sistema");
        }
        public async Task ValidarFechaNacimiento(int idPaciente, DateTime fecha)
        {
            bool esCorrecto = await _pacientesRepository.ValidarFechaNacimiento(idPaciente, fecha);
            if (!esCorrecto)
                throw new Exception("La fecha de nacimiento no coincide con nuestros registros.");
        }


    }
}
