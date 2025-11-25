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
    public class CitaMedicaService : ICitasMedicasService
    {

        private readonly ICitasMedicasRepository _citasMedicasRepository;

        public CitaMedicaService(ICitasMedicasRepository citasMedicasRepository)
        {
            _citasMedicasRepository = citasMedicasRepository;
        }

        public async Task<Domain.CitaMedica> AgendarCitaMedica(Domain.CitaMedica cita)
        {
            bool respuesta = await _citasMedicasRepository.AgendarCitaMedica(cita);
            if (!respuesta)
                throw new Exception("Ocurrió un error, no se pudo registrar la cita");
            return cita;
        }

        public async Task<Domain.CitaMedica> CancelarCitaMedica(Domain.CitaMedica cita)
        {
            CitaMedica citaObj = await _citasMedicasRepository.InformacionCitaMedica(cita);
            if (citaObj.FechaCita.AddDays(1) >= DateTime.Now)
                throw new Exception("Falta menos de un dia para la cita, no se puede cancelar");

            bool respuesta = await _citasMedicasRepository.CancelarCitaMedica(cita);
            if (!respuesta)
                throw new Exception("Ocurrió un error, no se pudo cancelar la cita");

            return citaObj;
        }

        public async Task<List<Domain.CitaMedica>> HistorialCitasMedicas(Domain.CitaMedica cita)
        {
            List<CitaMedica> listadoCitas = await _citasMedicasRepository.HistorialCitasMedicas(cita);
            if (listadoCitas.Count == 0)
                throw new Exception("No se encontró ninguna cita en el historial");

            return listadoCitas;
        }
    }
}
