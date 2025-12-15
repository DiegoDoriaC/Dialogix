using Dialogix.Application.Common.DTOs;
using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Essalud.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Dialogix.Helpers;

namespace Dialogix.Controllers
{

    public class ReportesController : Controller
    {
        private readonly IReportesService _reportesService;

        public ReportesController(IReportesService reportesService)
        {
            _reportesService = reportesService;
        }

        [HttpGet("filtrarFeedback")]
        public async Task<RespuestaGenerica<List<Feedback>>> FiltrarFeedback(Feedback feedback)
        {
            var response = new RespuestaGenerica<List<Feedback>>();

            try
            {
                var admin = HttpContext.Session.GetObject<Usuario>("admin");
                int idAdmin = admin?.IdUsuario ?? 0;

                var listado = await _reportesService.FiltrarFeedback(feedback, idAdmin);

                response.Mensaje = "Filtrado obtenido exitosamente";
                response.ObjetoRespuesta = listado;
                response.Estado = listado.Count > 0;
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
            var response = new RespuestaGenerica<int>();

            try
            {
                var admin = HttpContext.Session.GetObject<Usuario>("admin");
                int idAdmin = admin?.IdUsuario ?? 0;

                int total = await _reportesService.FiltrarMetricaUso(FechaInicio, FechaFin, idAdmin);

                response.Mensaje = "Métrica obtenida correctamente";
                response.ObjetoRespuesta = total;
                response.Estado = true;
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
            var response = new RespuestaGenerica<List<MetricaUso>>();

            try
            {
                var admin = HttpContext.Session.GetObject<Usuario>("admin");
                int idAdmin = admin?.IdUsuario ?? 0;

                var listado = await _reportesService
                    .FiltrarMetricaUsoPorDia(FechaInicio, FechaFin, idAdmin);

                response.Mensaje = "Métrica por día obtenida correctamente";
                response.ObjetoRespuesta = listado;
                response.Estado = listado.Count > 0;
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
            var response = new RespuestaGenerica<List<MetricaUso>>();

            try
            {
                var admin = HttpContext.Session.GetObject<Usuario>("admin");
                int idAdmin = admin?.IdUsuario ?? 0;

                var listado = await _reportesService
                    .FiltrarMetricaUsoPorMes(FechaInicio, FechaFin, idAdmin);

                response.Mensaje = "Métrica por mes obtenida correctamente";
                response.ObjetoRespuesta = listado;
                response.Estado = listado.Count > 0;
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.Message;
                response.Estado = false;
            }

            return response;
        }

        [HttpGet("metricas/hoy")]
        public async Task<RespuestaGenerica<int>> ObtenerMetricasHoy()
        {
            var response = new RespuestaGenerica<int>();

            try
            {
                var admin = HttpContext.Session.GetObject<Usuario>("admin");
                int idAdmin = admin?.IdUsuario ?? 0;

                response.ObjetoRespuesta = await _reportesService.ObtenerMetricaHoy(idAdmin);
                response.Mensaje = "Métricas de hoy obtenidas correctamente";
                response.Estado = true;
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.Message;
                response.Estado = false;
            }

            return response;
        }

        [HttpGet("metricas/citas-total")]
        public async Task<RespuestaGenerica<int>> ObtenerTotalCitas()
        {
            var response = new RespuestaGenerica<int>();

            try
            {
                var admin = HttpContext.Session.GetObject<Usuario>("admin");
                int idAdmin = admin?.IdUsuario ?? 0;

                response.ObjetoRespuesta = await _reportesService.ObtenerTotalCitasAgendadas(idAdmin);
                response.Mensaje = "Total de citas agendadas obtenido correctamente";
                response.Estado = true;
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.Message;
                response.Estado = false;
            }

            return response;
        }

        [HttpGet("citas-atendidas-total")]
        public async Task<RespuestaGenerica<int>> ObtenerTotalCitasAtendidas()
        {
            var response = new RespuestaGenerica<int>();

            try
            {
                var admin = HttpContext.Session.GetObject<Usuario>("admin");
                int idAdmin = admin?.IdUsuario ?? 0;

                response.ObjetoRespuesta = await _reportesService.ObtenerTotalCitasAtendidas(idAdmin);
                response.Mensaje = "Total de citas atendidas obtenido correctamente";
                response.Estado = true;
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.Message;
                response.Estado = false;
            }

            return response;
        }

        [HttpGet("citas-por-especialidad-totales")]
        public async Task<RespuestaGenerica<List<CitasPorEspecialidadDTO>>> ListarCitasPorEspecialidadTotales()
        {
            var response = new RespuestaGenerica<List<CitasPorEspecialidadDTO>>();

            try
            {
                var admin = HttpContext.Session.GetObject<Usuario>("admin");
                int idAdmin = admin?.IdUsuario ?? 0;

                var listado = await _reportesService.ListarCitasPorEspecialidadTotales(idAdmin);

                response.Mensaje = "Citas por especialidad obtenidas correctamente";
                response.ObjetoRespuesta = listado;
                response.Estado = listado.Count > 0;
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
