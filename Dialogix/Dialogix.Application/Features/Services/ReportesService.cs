using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Dialogix.Infrastructure.Repositories;
using Essalud.Domain.DTOs;
using Essalud.Infraestructure.Repositories.Interfaces;

namespace Dialogix.Application.Features.Services
{
    public class ReportesService : IReportesService
    {
        private readonly IReportesRepository _reportesRepository;
        private readonly IReportesCitasRepository _citasRepo;
        private readonly ICitasMedicasRepository _citasMedicasRepository;
        private readonly IActividadAdminService _actividadAdminService;

        public ReportesService(
            IReportesRepository reportesRepository,
            IReportesCitasRepository citasRepo,
            ICitasMedicasRepository citasMedicasRepository,
            IActividadAdminService actividadAdminService)
        {
            _reportesRepository = reportesRepository;
            _citasRepo = citasRepo;
            _citasMedicasRepository = citasMedicasRepository;
            _actividadAdminService = actividadAdminService;
        }

        public Task<bool> RegistrarMetrica()
        {
            return _reportesRepository.RegistrarMetrica();
        }

        public async Task<List<Feedback>> FiltrarFeedback(Feedback feedback, int idAdmin)
        {
            var lista = await _reportesRepository.FiltrarFeedback(feedback);

            await _actividadAdminService.RegistrarActividad(
                idAdmin,
                "Reportes",
                "Filtró feedback",
                $"Registros: {lista.Count}"
            );

            return lista;
        }

        public async Task<int> FiltrarMetricaUso(string FechaInicio, string FechaFin, int idAdmin)
        {
            DateTime inicio = DateTime.Parse(FechaInicio);
            DateTime fin = DateTime.Parse(FechaFin);

            int total = await _reportesRepository.FiltrarMetricaUso(inicio, fin);

            await _actividadAdminService.RegistrarActividad(
                idAdmin,
                "Reportes",
                "Filtró métricas de uso",
                $"{FechaInicio} - {FechaFin}"
            );

            return total;
        }

        public async Task<List<MetricaUso>> FiltrarMetricaUsoPorDia(string FechaInicio, string FechaFin, int idAdmin)
        {
            DateTime inicio = DateTime.Parse(FechaInicio);
            DateTime fin = DateTime.Parse(FechaFin);

            var lista = await _reportesRepository.FiltrarMetricaUsoPorDia(inicio, fin);

            await _actividadAdminService.RegistrarActividad(
                idAdmin,
                "Reportes",
                "Consultó métricas por día",
                $"{FechaInicio} - {FechaFin}"
            );

            return lista;
        }

        public async Task<List<MetricaUso>> FiltrarMetricaUsoPorMes(string FechaInicio, string FechaFin, int idAdmin)
        {
            DateTime inicio = DateTime.Parse(FechaInicio);
            DateTime fin = DateTime.Parse(FechaFin);

            var lista = await _reportesRepository.FiltrarMetricaUsoPorMes(inicio, fin);

            await _actividadAdminService.RegistrarActividad(
                idAdmin,
                "Reportes",
                "Consultó métricas por mes",
                $"{FechaInicio} - {FechaFin}"
            );

            return lista;
        }

        public async Task<int> ObtenerMetricaHoy(int idAdmin)
        {
            int total = await _reportesRepository.ObtenerMetricaHoy();

            await _actividadAdminService.RegistrarActividad(
                idAdmin,
                "Reportes",
                "Consultó métricas de hoy",
                DateTime.Now.ToString("dd/MM/yyyy")
            );

            return total;
        }

        public async Task<int> ObtenerTotalCitasAgendadas(int idAdmin)
        {
            int total = await _citasRepo.ObtenerTotalCitasAgendadas();

            await _actividadAdminService.RegistrarActividad(
                idAdmin,
                "Reportes",
                "Consultó total de citas agendadas",
                $"Total: {total}"
            );

            return total;
        }

        public async Task<int> ObtenerTotalCitasAtendidas(int idAdmin)
        {
            int total = await _citasMedicasRepository.ObtenerTotalCitasAtendidas();

            await _actividadAdminService.RegistrarActividad(
                idAdmin,
                "Reportes",
                "Consultó total de citas atendidas",
                $"Total: {total}"
            );

            return total;
        }

        public async Task<List<CitasPorEspecialidadDTO>> ListarCantidadConsultasPorEspecialidad(
            string FechaInicio,
            string FechaFin,
            int idAdmin)
        {
            DateTime inicio = DateTime.Parse(FechaInicio);
            DateTime fin = DateTime.Parse(FechaFin);

            var lista = await _citasMedicasRepository
                .ListarCantidadConsultasPorEspecialidad(inicio, fin);

            await _actividadAdminService.RegistrarActividad(
                idAdmin,
                "Reportes",
                "Consultó cantidad de consultas por especialidad",
                $"{FechaInicio} - {FechaFin}"
            );

            return lista;
        }

        public async Task<List<CitasPorEspecialidadDTO>> ListarCitasPorEspecialidadTotales(int idAdmin)
        {
            var lista = await _citasMedicasRepository.ListarCitasPorEspecialidadTotales();

            await _actividadAdminService.RegistrarActividad(
                idAdmin,
                "Reportes",
                "Consultó citas totales por especialidad",
                $"Registros: {lista.Count}"
            );

            return lista;
        }
    }
}
