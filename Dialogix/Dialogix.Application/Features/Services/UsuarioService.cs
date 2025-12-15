using Dialogix.Application.Common;
using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Dialogix.Infrastructure.Repositories;

namespace Dialogix.Application.Features.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IActividadAdminService _actividadAdminService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IActividadAdminService actividadAdminService)
        {
            _usuarioRepository = usuarioRepository;
            _actividadAdminService = actividadAdminService;
        }

        public async Task<Usuario> IniciarSesion(string user, string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(user))
                throw new Exception("Ingrese su usuario");

            if (string.IsNullOrWhiteSpace(contrasenia))
                throw new Exception("Ingrese su contraseña");

            Usuario usuario = new Usuario { User = user };
            usuario = await _usuarioRepository.IniciarSesion(usuario);

            if (!EncriptacionSha.VerificarCoincidencia(contrasenia, usuario.Contrasenia))
                throw new Exception("Usuario o contraseña incorrecto");

            return usuario;
        }


        public async Task<Usuario> ActualizarAvatar(
            int IdUsuario,
            string NombreImagen,
            int idAdmin)
        {
            if (string.IsNullOrWhiteSpace(NombreImagen))
                throw new Exception("Debe subir una imagen para poder actualizar su foto de perfil");

            Usuario usuario =
                await _usuarioRepository.ActualizarAvatar(IdUsuario, NombreImagen);

            if (usuario.IdUsuario != 0)
            {
                await _actividadAdminService.RegistrarActividad(
                    idAdmin,
                    "Usuario",
                    "Actualizó su avatar",
                    $"Imagen: {NombreImagen}"
                );
            }

            return usuario;
        }


        public async Task<Usuario> ActualizarDatos(
            int IdUsuario,
            string Nombre,
            string Apellido,
            int idAdmin)
        {
            if (string.IsNullOrWhiteSpace(Nombre))
                throw new Exception("Ingrese un nombre válido");

            if (string.IsNullOrWhiteSpace(Apellido))
                throw new Exception("Ingrese un apellido válido");

            Usuario usuario =
                await _usuarioRepository.ActualizarDatos(IdUsuario, Nombre, Apellido);

            if (usuario.IdUsuario != 0)
            {
                await _actividadAdminService.RegistrarActividad(
                    idAdmin,
                    "Usuario",
                    "Actualizó sus datos personales",
                    $"{Nombre} {Apellido}"
                );
            }

            return usuario;
        }
    }
}
