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
    public class ReportesService : IReportesService
    {
        private readonly IReportesRepository _ReportesRepository;

        public ReportesService(IReportesRepository reportesRepository)
        {
            _ReportesRepository = reportesRepository;
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
    }
}
