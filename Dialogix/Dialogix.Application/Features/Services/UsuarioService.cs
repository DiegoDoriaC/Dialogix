using Azure.Core;
using Dialogix.Application.Common;
using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Dialogix.Infrastructure.Repositories;
using System.Reflection;

namespace Dialogix.Application.Features.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this._usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> IniciarSesion(string user, string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(user)) throw new Exception("Ingrese su usuario");
            if (string.IsNullOrWhiteSpace(contrasenia)) throw new Exception("Ingrese su contraseña");

            Usuario usuario = new Usuario();
            usuario.User = user;
            usuario = await _usuarioRepository.IniciarSesion(usuario);

            if (!EncriptacionSha.VerificarCoincidencia(contrasenia, usuario.Contrasenia))
                throw new Exception("Usuario o contraseña incorrecto");

            return usuario;
        }

        public async Task<Usuario> ActualizarAvatar(int IdUsuario, string NombreImagen)
        {
            if (string.IsNullOrWhiteSpace(NombreImagen)) throw new Exception("Debe subir una imagen para poder actualizar su foto de perfil");

            Usuario usuario = new Usuario();
            usuario = await _usuarioRepository.ActualizarAvatar(IdUsuario, NombreImagen);
            return usuario;
        }
    }
}
