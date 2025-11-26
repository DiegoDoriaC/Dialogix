using Dialogix.Application.Common.DTOs;
using Dialogix.Application.Features.Interfaces;
using Dialogix.Application.Features.Services;
using Dialogix.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Dialogix.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IReportesService _reportesService;

        public ReportesController(IReportesService reportesService)
        {
            _reportesService = reportesService;
        }

        [HttpGet("filtrarFeetback")]
        public async Task<RespuestaGenerica<List<Feedback>>> FiltrarFeedback(Feedback feedback)
        {
            List<Feedback> listado = new List<Feedback>();
            RespuestaGenerica<List<Feedback>> response = new RespuestaGenerica<List<Feedback>>();

            try
            {
                listado = await _reportesService.FiltrarFeedback(feedback);
                response.Mensaje = "Filtrado obtenido exitosamente";
                response.ObjetoRespuesta = listado;
                response.Estado = true;

                if (listado.Count == 0)
                {
                    response.Mensaje = "No se encontró nigngun feedback";
                    response.Estado = false;
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.Message;
                response.Estado = false;
            }

            return response;
        }


        [HttpGet("filtrarMetricaUso")]
        public async Task<RespuestaGenerica<int>> FiltrarMetricaUso(string FechaInicio, string FechaFin)
        {
            int contador = 0;
            RespuestaGenerica<int> response = new RespuestaGenerica<int>();

            try
            {
                contador = await _reportesService.FiltrarMetricaUso(FechaInicio, FechaFin);
                response.Mensaje = "Metrica obteneda correctamente";
                response.ObjetoRespuesta = contador;
                response.Estado = true;

                if (contador == 0)
                {
                    response.Mensaje = "No se encontraron datos de metrica";
                    response.Estado = false;
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.Message;
                response.Estado = false;
            }

            return response;
        }

        [HttpGet("filtrarMetricaUsoDia")]
        public async Task<RespuestaGenerica<List<MetricaUso>>> FiltrarMetricaUsoPorDia(string FechaInicio, string FechaFin)
        {
            List<MetricaUso> listado = new List<MetricaUso>();
            RespuestaGenerica<List<MetricaUso>> response = new RespuestaGenerica<List<MetricaUso>>();

            try
            {
                listado = await _reportesService.FiltrarMetricaUsoPorDia(FechaInicio, FechaFin);
                response.Mensaje = "Metrica obteneda correctamente";
                response.ObjetoRespuesta = listado;
                response.Estado = true;

                if (listado.Count == 0)
                {
                    response.Mensaje = "No se encontraron datos de metrica";
                    response.Estado = false;
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.Message;
                response.Estado = false;
            }

            return response;
        }

        [HttpGet("filtrarMetricaUsoMes")]
        public async Task<RespuestaGenerica<List<MetricaUso>>> FiltrarMetricaUsoPorMes(string FechaInicio, string FechaFin)
        {
            List<MetricaUso> listado = new List<MetricaUso>();
            RespuestaGenerica<List<MetricaUso>> response = new RespuestaGenerica<List<MetricaUso>>();

            try
            {
                listado = await _reportesService.FiltrarMetricaUsoPorMes(FechaInicio, FechaFin);
                response.Mensaje = "Metrica obteneda correctamente";
                response.ObjetoRespuesta = listado;
                response.Estado = true;

                if (listado.Count == 0)
                {
                    response.Mensaje = "No se encontraron datos de metrica";
                    response.Estado = false;
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.Message;
                response.Estado = false;
            }

            return response;
        }

    }
}
