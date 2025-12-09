using Dialogix.ChatBot.Interfaces;
using Dialogix.Domain.Common;
using Dialogix.Helpers;
using Essalud.Application.Feature.Interfaces;
using Essalud.Domain;

namespace Dialogix.ChatBot
{
    public class FlujoConsultarResultados : IFlujoConsultarResultados
    {

        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        private readonly IResultadosService _resultadosService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FlujoConsultarResultados(IHttpContextAccessor httpContextAccessor, IResultadosService resultadosService)
        {
            _httpContextAccessor = httpContextAccessor;
            _resultadosService = resultadosService;
        }

        public async Task<string> ConsultarEstadoResultados()
        {
            string mensaje = "";
            EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");
            ResultadosClinicos resultados = new ResultadosClinicos();
            resultados.CitaMedica.Paciente.IdPaciente = estadoConversacion!.IdPaciente;

            try
            {
                List<ResultadosClinicos> listadoResultados = await _resultadosService.ConsultarEstadoResultados(resultados);
                mensaje = "Usted tiene " + listadoResultados.Count + " resultado(s) médico(s) en su historial (el mas reciente aparecerá primero)";
                for(int i = 0; i < listadoResultados.Count; i++)
                {
                    // Dividido por * para que judith pueda hacer la separacion entre resultados
                    mensaje += "*|Fecha de registro: " + listadoResultados.ElementAt(i).FechaRegistro.ToShortDateString();
                    mensaje += "|Tipo de examen: " + listadoResultados.ElementAt(i).TipoExamen;
                    mensaje += "|Estado: " + listadoResultados.ElementAt(i).Estado + "*";
                }
            }
            catch (Exception ex)
            {
                Session.Clear();
                return ex.Message;
            }

            Session.Clear();
            return mensaje;
        }

    }
}
