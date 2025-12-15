using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Dialogix.Application.Features.Interfaces;

namespace Dialogix.Application.Features.Services
{
    public class ConversacionService : IConversacionService
    {
        private readonly IConfiguration _config;
        private readonly IConfiguracionChatbotService _configChatbotService;

        public ConversacionService(
            IConfiguration config,
            IConfiguracionChatbotService configChatbotService)
        {
            _config = config;
            _configChatbotService = configChatbotService;
        }

        public async Task<string> IniciarConversacion()
        {
            var config = await _configChatbotService.ObtenerConfiguracionAsync();

            if (!config.Activo)
                return config.MensajeMantenimiento;

            var ahora = DateTime.Now.TimeOfDay;

            bool fueraHorario =
                ahora < config.HoraInicio || ahora > config.HoraFin;

            if (fueraHorario && config.HabilitarFueraHorario)
                return config.MensajeFueraHorario;

            if (fueraHorario && !config.HabilitarFueraHorario)
                return string.Empty;

            return config.MensajeBienvenida;
        }


        public async Task<string> ReiterarInicioConversacion()
        {
            var config = await _configChatbotService.ObtenerConfiguracionAsync();

            if (!config.Activo)
                return config.MensajeMantenimiento;

            return "Por favor, ingrese su DNI para validarlo en el sistema";
        }


        public async Task<string> IniciarConversacionConLaIA()
        {
            return "Hola!, soy tu asistente virtual. Puedes hacer consultas sobre EsSalud, citas, resultados o servicios institucionales.";
            //var config = await _configChatbotService.ObtenerConfiguracionAsync();

            //if (!config.Activo)
            //    return config.MensajeMantenimiento;

            //return config.MensajeBienvenida;
        }


        public async Task<int> RegistrarConversacion(int dniUsuario, string canal)
        {
            using SqlConnection cn = new SqlConnection(_config.GetConnectionString("CadenaDialogix"));
            await cn.OpenAsync();

            SqlCommand cmd = new SqlCommand("pr_registrar_conversacion", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DniUsuario", dniUsuario);
            cmd.Parameters.AddWithValue("@FechaInicio", DateTime.Now);
            cmd.Parameters.AddWithValue("@Canal", canal);

            SqlParameter output = new SqlParameter("@IdConversacion", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(output);

            await cmd.ExecuteNonQueryAsync();
            return (int)output.Value;
        }

        public async Task RegistrarMensaje(int idConversacion, string texto, string respuesta)
        {
            using SqlConnection cn = new SqlConnection(_config.GetConnectionString("CadenaDialogix"));
            await cn.OpenAsync();

            SqlCommand cmd = new SqlCommand("pr_registrar_mensaje", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdConversacion", idConversacion);
            cmd.Parameters.AddWithValue("@Texto", texto);
            cmd.Parameters.AddWithValue("@Respuesta", respuesta);
            cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task RegistrarMetrica()
        {
            using SqlConnection cn = new SqlConnection(_config.GetConnectionString("CadenaDialogix"));
            await cn.OpenAsync();

            SqlCommand cmd = new SqlCommand("pr_registrar_metrica", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task FinalizarConversacion(int idConversacion)
        {
            using SqlConnection cn = new SqlConnection(_config.GetConnectionString("CadenaDialogix"));
            await cn.OpenAsync();

            SqlCommand cmd = new SqlCommand("pr_actualizar_finalizar_conversacion", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdConversacion", idConversacion);
            cmd.Parameters.AddWithValue("@IDFechaFin", DateTime.Now);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
