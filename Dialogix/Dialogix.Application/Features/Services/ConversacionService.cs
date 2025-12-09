using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Dialogix.Application.Features.Interfaces;

namespace Dialogix.Application.Features.Services
{
    public class ConversacionService : IConversacionService
    {
        private readonly IConfiguration _config;

        public ConversacionService(IConfiguration config)
        {
            _config = config;
        }

        public Task<string> IniciarConversacion()
        {
            return Task.FromResult("👋 ¡Hola! Soy tu asistente virtual de salud. Por favor ingresa tu DNI para continuar:");
        }

        public Task<string> ReiterarInicioConversacion()
        {
            return Task.FromResult("Por favor, ingrese su DNI para validarlo en el sistema");
        }

        public Task<string> IniciarConversacionConLaIA()
        {
            return Task.FromResult("Hola!, soy tu asistente virtual. Puedes hacer consultas sobre EsSalud, citas, resultados o servicios institucionales.");
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
