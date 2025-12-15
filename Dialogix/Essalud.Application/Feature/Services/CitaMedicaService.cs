using Essalud.Application.Feature.Interfaces;
using Essalud.Domain;
using Essalud.Domain.DTOs;
using Essalud.Infraestructure.Repositories.Interfaces;


namespace Essalud.Application.Feature.Services
{
    public class CitaMedicaService : ICitasMedicasService
    {

        private readonly ICitasMedicasRepository _citasMedicasRepository;

        public CitaMedicaService(ICitasMedicasRepository citasMedicasRepository)
        {
            _citasMedicasRepository = citasMedicasRepository;
        }

        public async Task<CitaMedica> AgendarCitaMedica(CitaMedica cita)
        {
            bool respuesta = await _citasMedicasRepository.AgendarCitaMedica(cita);
            if (!respuesta)
                throw new Exception("Ocurrió un error, no se pudo registrar la cita");
            return cita;
        }

        public async Task<CitaMedica> CancelarCitaMedica(CitaMedica cita)
        {
            CitaMedica citaObj = await _citasMedicasRepository.InformacionCitaMedica(cita);
            if (citaObj.FechaCita <= DateTime.Now.AddHours(1))
                throw new Exception("Falta menos de una hora para la cita, no se puede cancelar");

            bool respuesta = await _citasMedicasRepository.CancelarCitaMedica(cita);
            if (!respuesta)
                throw new Exception("Ocurrió un error, no se pudo cancelar la cita");

            return citaObj;
        }

        public async Task<List<CitaMedica>> HistorialCitasMedicas(CitaMedica cita)
        {
            List<CitaMedica> listadoCitas = await _citasMedicasRepository.HistorialCitasMedicas(cita);
            if (listadoCitas.Count == 0)
                throw new Exception("No se encontró ninguna cita en el historial");

            return listadoCitas;
        }

        public async Task<int> ObtenerTotalCitasAtendidas()
        {
            return await _citasMedicasRepository.ObtenerTotalCitasAtendidas();
        }

        public async Task<List<CitasPorEspecialidadDTO>> ListarCitasPorEspecialidadTotales()
        {
            return await _citasMedicasRepository.ListarCitasPorEspecialidadTotales();
        }

    }
}
 