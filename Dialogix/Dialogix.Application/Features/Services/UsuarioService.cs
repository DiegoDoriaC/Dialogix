using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Dialogix.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(string.IsNullOrWhiteSpace(contrasenia)) throw new Exception("Ingrese su contraseña");

            Usuario usuario = new Usuario();
            usuario.User = user;
            usuario.Contrasenia = contrasenia;
            usuario = await _usuarioRepository.IniciarSesion(usuario);
            return usuario;
        }
    }
}
