using Dialogix.Domain;
using Dialogix.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ISqlConnectionDialogixFactory _connectionFactory;

        public UsuarioRepository(ISqlConnectionDialogixFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Usuario> IniciarSesion(Usuario usuario)
        {

            Usuario obj = new Usuario();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_iniciar_sesion";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Usuario", usuario.User);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            obj.IdUsuario = rd["IdUsuario"] != DBNull.Value ? Convert.ToInt32(rd["IdUsuario"]) : 0;
                            obj.Nombre = Convert.ToString(rd["Nombre"])!;
                            obj.Apellido = Convert.ToString(rd["Apellido"])!;
                            obj.Rol = Convert.ToString(rd["Rol"])!;
                            obj.Contrasenia = Convert.ToString(rd["Contraseña"])!;
                            obj.Avatar = Convert.ToString(rd["Avatar"])!;
                        }
                    }
                }
            }
            return obj;
        }

        public async Task<Usuario> ActualizarAvatar(int IdUsuario, string NombreImagen)
        {
            Usuario obj = new Usuario();

            using (var connection = (SqlConnection)_connectionFactory.CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "pr_actualizar_avatar";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id_usuario", IdUsuario);
                    command.Parameters.AddWithValue("@Avatar", NombreImagen);

                    await connection.OpenAsync();

                    using (var rd = await command.ExecuteReaderAsync())
                    {
                        while (await rd.ReadAsync())
                        {
                            obj.IdUsuario = rd["IdUsuario"] != DBNull.Value ? Convert.ToInt32(rd["IdUsuario"]) : 0;
                            obj.Nombre = Convert.ToString(rd["Nombre"])!;
                            obj.Apellido = Convert.ToString(rd["Apellido"])!;
                            obj.Rol = Convert.ToString(rd["Rol"])!;
                            obj.Contrasenia = Convert.ToString(rd["Contraseña"])!;
                            obj.Avatar = Convert.ToString(rd["Avatar"])!;
                        }
                    }
                }
            }
            return obj;
        }

    }
}
