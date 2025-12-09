using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Dialogix.Infrastructure.Repositories;
using Essalud.Domain.DTOs;
using Essalud.Infraestructure.Repositories.Interfaces;

namespace Dialogix.Application.Features.Services
{
    public class ReportesService : IReportesService
    {
        private readonly IReportesRepository _ReportesRepository;
        private readonly IReportesCitasRepository _citasRepo;
        private readonly ICitasMedicasRepository _citasMedicasRepository;

        public ReportesService(IReportesRepository reportesRepository, IReportesCitasRepository citasRepo, ICitasMedicasRepository citasMedicasRepository)
        {
            _ReportesRepository = reportesRepository;
            _citasRepo = citasRepo;
            _citasMedicasRepository = citasMedicasRepository;
        }

        public Task<List<Feedback>> FiltrarFeedback(Feedback feedback)
        {
            return _ReportesRepository.FiltrarFeedback(feedback);

        }

        public Task<int> FiltrarMetricaUso(string FechaInicio, string FechaFin)
        {
            DateTime fechaIncioParseado = DateTime.MinValue;
            DateTime fechaFinParseado = DateTime.MaxValue;

            if (!DateTime.TryParse(FechaInicio, out fechaIncioParseado)) throw new Exception("Error al convertir la fecha de inicio");
            if (!DateTime.TryParse(FechaFin, out fechaFinParseado)) throw new Exception("Error al convertir la fecha de fin");

            return _ReportesRepository.FiltrarMetricaUso(fechaIncioParseado, fechaFinParseado);
        }

        public Task<List<MetricaUso>> FiltrarMetricaUsoPorDia(string FechaInicio, string FechaFin)
        {
            DateTime fechaIncioParseado = DateTime.MinValue;
            DateTime fechaFinParseado = DateTime.MaxValue;

            if (!DateTime.TryParse(FechaInicio, out fechaIncioParseado)) throw new Exception("Error al convertir la fecha de inicio");
            if (!DateTime.TryParse(FechaFin, out fechaFinParseado)) throw new Exception("Error al convertir la fecha de fin");

            return _ReportesRepository.FiltrarMetricaUsoPorDia(fechaIncioParseado, fechaFinParseado);
        }

        public Task<List<MetricaUso>> FiltrarMetricaUsoPorMes(string FechaInicio, string FechaFin)
        {
            DateTime fechaIncioParseado = DateTime.MinValue;
            DateTime fechaFinParseado = DateTime.MaxValue;

            if (!DateTime.TryParse(FechaInicio, out fechaIncioParseado)) throw new Exception("Error al convertir la fecha de inicio");
            if (!DateTime.TryParse(FechaFin, out fechaFinParseado)) throw new Exception("Error al convertir la fecha de fin");

            return _ReportesRepository.FiltrarMetricaUsoPorMes(fechaIncioParseado, fechaFinParseado);
        }

        public Task<bool> RegistrarMetrica()
        {
            return _ReportesRepository.RegistrarMetrica();
        }

        public Task<int> ObtenerMetricaHoy()
        {
            return _ReportesRepository.ObtenerMetricaHoy();
        }

        public Task<int> ObtenerTotalCitasAgendadas()
        {
            return _citasRepo.ObtenerTotalCitasAgendadas();
        }

        public Task<List<CitasPorEspecialidadDTO>> ListarCantidadConsultasPorEspecialidad(string FechaInicio, string FechaFin)
        {
            DateTime fechaIncioParseado = DateTime.MinValue;
            DateTime fechaFinParseado = DateTime.MaxValue;

            if (!DateTime.TryParse(FechaInicio, out fechaIncioParseado)) throw new Exception("Error al convertir la fecha de inicio");
            if (!DateTime.TryParse(FechaFin, out fechaFinParseado)) throw new Exception("Error al convertir la fecha de fin");

            return _citasMedicasRepository.ListarCantidadConsultasPorEspecialidad(fechaIncioParseado, fechaFinParseado);
        }

    }
}
