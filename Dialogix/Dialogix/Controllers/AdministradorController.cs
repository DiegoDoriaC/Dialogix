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

                if (respuesta.IdUsuario == 0)
                {
                    response.Mensaje = "Usuario no encontrado";
                    response.Estado = false;
                    response.ObjetoRespuesta = new Usuario();
                    return response;
                }

                HttpContext.Session.SetObject("admin", respuesta);

                string baseUrl = $"{Request.Scheme}://{Request.Host}";
                respuesta.Avatar = $"{baseUrl}/avatars/{respuesta.Avatar}";

                response.Mensaje = "Inicio de sesión exitoso";
                response.ObjetoRespuesta = respuesta;
                response.Estado = true;
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

                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/avatars",
                    imagen.Avatar.FileName
                );

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imagen.Avatar.CopyToAsync(stream);
                }

                var admin = HttpContext.Session.GetObject<Usuario>("admin");
                int idAdmin = admin?.IdUsuario ?? 0;

                response.ObjetoRespuesta =
                    await _usuarioService.ActualizarAvatar(
                        imagen.IdUsuario,
                        imagen.Avatar.FileName,
                        idAdmin
                    );


                string baseUrl = $"{Request.Scheme}://{Request.Host}";
                response.ObjetoRespuesta.Avatar =
                    $"{baseUrl}/avatars/{response.ObjetoRespuesta.Avatar}";

                response.Mensaje = "Avatar actualizado correctamente";
                response.Estado = true;
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.Message;
                response.Estado = false;
            }

            return response;
        }


        [HttpPut("actualizar-datos")]
        public async Task<RespuestaGenerica<Usuario>> ActualizarDatos(
            int idUsuario,
            string nombre,
            string apellido)
        {
            RespuestaGenerica<Usuario> response = new RespuestaGenerica<Usuario>();

            try
            {
                var admin = HttpContext.Session.GetObject<Usuario>("admin");
                int idAdmin = admin?.IdUsuario ?? 0;

                var usuarioActualizado =
                    await _usuarioService.ActualizarDatos(
                        idUsuario,
                        nombre,
                        apellido,
                        idAdmin
                    );


                if (usuarioActualizado.IdUsuario == 0)
                {
                    response.Mensaje = "No se encontró el usuario";
                    response.Estado = false;
                    return response;
                }

                string baseUrl = $"{Request.Scheme}://{Request.Host}";
                usuarioActualizado.Avatar =
                    $"{baseUrl}/avatars/{usuarioActualizado.Avatar}";

                response.Mensaje = "Datos actualizados correctamente";
                response.ObjetoRespuesta = usuarioActualizado;
                response.Estado = true;
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
