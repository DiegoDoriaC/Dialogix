using Dialogix.Application.Common.DTOs;
using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Dialogix.Controllers
{
    [Controller]
    [Route("Dialogix/[controller]")]
    public class AdministradorController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public AdministradorController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<RespuestaGenerica<Usuario>> IniciarSesion(string usuario, string contrasenia)
        {
            RespuestaGenerica<Usuario> response = new RespuestaGenerica<Usuario>();

            try
            {
                var respuesta = await _usuarioService.IniciarSesion(usuario, contrasenia);
                response.Mensaje = "Inicio de sesion exitoso";
                response.ObjetoRespuesta = respuesta;
                response.Estado = true;

                if (respuesta.IdUsuario == 0)
                {
                    response.Mensaje = "Usuario no encontrado";
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
