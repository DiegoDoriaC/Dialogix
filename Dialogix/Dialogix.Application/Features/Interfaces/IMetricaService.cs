using Dialogix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Interfaces
{
    public interface IMetricaService
    {
        Task<MetricaUso> RegistrarMetrica(MetricaUso metrica);
        Task<List<MetricaUso>> FiltrarMetricaUso(string fechaInicio, string fechaFin);
        Task<List<MetricaUso>> FiltrarMetricaUsoPorDia(string fechaInicio, string fechaFin);
        Task<List<MetricaUso>> FiltrarMetricaUsoPorMes(string fechaInicio, string fechaFin);
    }
}
