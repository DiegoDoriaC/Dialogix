using Dialogix.Domain;
using Dialogix.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dialogix.Infrastructure.Repositories
{
    public class ConfiguracionChatbotRepository : IConfiguracionChatbotRepository
    {
        private readonly ISqlConnectionDialogixFactory _connectionFactory;

        public ConfiguracionChatbotRepository(
            ISqlConnectionDialogixFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<ConfiguracionChatbot?> ObtenerConfiguracionAsync()
        {
            using IDbConnection cn = _connectionFactory.CreateConnection();
            await ((SqlConnection)cn).OpenAsync();

            SqlCommand cmd = new SqlCommand(
                "pr_obtener_configuracion_chatbot",
                (SqlConnection)cn
            );
            cmd.CommandType = CommandType.StoredProcedure;

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

            if (!await dr.ReadAsync())
                return null;

            return new ConfiguracionChatbot
            {
                IdConfig = Convert.ToInt32(dr["IdConfig"]),
                MensajeBienvenida = dr["MensajeBienvenida"].ToString()!,
                MensajeFueraHorario = dr["MensajeFueraHorario"].ToString()!,
                MensajeMantenimiento = dr["MensajeMantenimiento"].ToString()!,
                HoraInicio = (TimeSpan)dr["HoraInicio"],
                HoraFin = (TimeSpan)dr["HoraFin"],
                HabilitarFueraHorario = Convert.ToBoolean(dr["HabilitarFueraHorario"]),
                MaxMensajes = Convert.ToInt32(dr["MaxMensajes"]),
                TimeoutSegundos = Convert.ToInt32(dr["TimeoutSegundos"]),
                Activo = Convert.ToBoolean(dr["Activo"]),
                FechaActualizacion = Convert.ToDateTime(dr["FechaActualizacion"])
            };
        }

        public async Task ActualizarConfiguracionAsync(ConfiguracionChatbot c)
        {
            using IDbConnection cn = _connectionFactory.CreateConnection();
            await ((SqlConnection)cn).OpenAsync();

            SqlCommand cmd = new SqlCommand(
                "pr_actualizar_configuracion_chatbot",
                (SqlConnection)cn
            );
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MensajeBienvenida", c.MensajeBienvenida);
            cmd.Parameters.AddWithValue("@MensajeFueraHorario", c.MensajeFueraHorario);
            cmd.Parameters.AddWithValue("@MensajeMantenimiento", c.MensajeMantenimiento);
            cmd.Parameters.AddWithValue("@HoraInicio", c.HoraInicio);
            cmd.Parameters.AddWithValue("@HoraFin", c.HoraFin);
            cmd.Parameters.AddWithValue("@HabilitarFueraHorario", c.HabilitarFueraHorario);
            cmd.Parameters.AddWithValue("@MaxMensajes", c.MaxMensajes);
            cmd.Parameters.AddWithValue("@TimeoutSegundos", c.TimeoutSegundos);
            cmd.Parameters.AddWithValue("@Activo", c.Activo);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
