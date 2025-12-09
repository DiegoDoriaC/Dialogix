using Dialogix.Application.Common.DTOs;
using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Microsoft.AspNetCore.Mvc;
using Dialogix.Helpers;

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

                string baseUrl = $"{Request.Scheme}://{Request.Host}";
                string avatarUrl = $"{baseUrl}/avatars/{respuesta.Avatar}";

                respuesta.Avatar = avatarUrl;

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

        [HttpPost("avatar")]
        public async Task<RespuestaGenerica<Usuario>> CambiarAvatar([FromForm] SubirImagenRequest imagen)
        {
            RespuestaGenerica<Usuario> response = new RespuestaGenerica<Usuario>();

            try
            {
                if (imagen.Avatar == null || imagen.Avatar.Length == 0)
                {
                    response.Mensaje = "No se envió ninguna imagen";
                    response.ObjetoRespuesta = new Usuario();
                    response.Estado = false;
                    return response;
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/avatars", imagen.Avatar.FileName);
                using (var stream = new FileStream(path, FileMode.Create)) await imagen.Avatar.CopyToAsync(stream); 

                response.Mensaje = "Avatar actualizado correctamente";
                response.ObjetoRespuesta = await _usuarioService.ActualizarAvatar(imagen.IdUsuario, imagen.Avatar.FileName);

                string baseUrl = $"{Request.Scheme}://{Request.Host}";
                response.ObjetoRespuesta.Avatar = $"{baseUrl}/avatars/{response.ObjetoRespuesta.Avatar}";

                response.Estado= true;
                return response;
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
