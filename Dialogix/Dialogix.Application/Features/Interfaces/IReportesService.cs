using Dialogix.Application.Features.DTOs.Reportes;
using Dialogix.Domain;
using Essalud.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Interfaces
{
    public interface IReportesService
    {
        Task<bool> RegistrarMetrica();
        Task<int> FiltrarMetricaUso(string FechaInicio, string FechaFin);
        Task<List<MetricaUso>> FiltrarMetricaUsoPorDia(string FechaInicio, string FechaFin);
        Task<List<MetricaUso>> FiltrarMetricaUsoPorMes(string FechaInicio, string FechaFin);
        Task<List<CitasPorEspecialidadDTO>> ListarCantidadConsultasPorEspecialidad(string FechaInicio, string FechaFin);

        Task<List<Feedback>> FiltrarFeedback(Feedback feedback);
        Task<int> ObtenerMetricaHoy();
        Task<int> ObtenerTotalCitasAgendadas();


    }
}
