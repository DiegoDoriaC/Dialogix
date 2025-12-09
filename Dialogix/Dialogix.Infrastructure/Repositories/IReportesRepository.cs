using Dialogix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Infrastructure.Repositories
{
    public interface IReportesRepository
    {        
        Task<bool> RegistrarMetrica();
        Task<int> FiltrarMetricaUso(DateTime FechaInicio, DateTime FechaFin);
        Task<List<MetricaUso>> FiltrarMetricaUsoPorDia(DateTime FechaInicio, DateTime FechaFin);
        Task<List<MetricaUso>> FiltrarMetricaUsoPorMes(DateTime FechaInicio, DateTime FechaFin);

        Task<List<Feedback>> FiltrarFeedback(Feedback feedback);
        Task<int> ObtenerMetricaHoy();

    }
}
