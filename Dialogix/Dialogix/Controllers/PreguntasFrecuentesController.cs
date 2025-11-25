using Dialogix.Application.Common.DTOs;
using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Dialogix.Controllers 
{
    [Controller]
    [Route("Dialogix/[controller]")]
    public class PreguntasFrecuentesController : Controller
    {
        private readonly IPreguntasFrecuentesService _preguntasFrecuentesService;

        public PreguntasFrecuentesController(IPreguntasFrecuentesService preguntasFrecuentesService)
        {
            _preguntasFrecuentesService = preguntasFrecuentesService;
        }

        [HttpGet("listar")]
        public async Task<RespuestaGenerica<List<PreguntasFrecuentes>>> ListarPreguntasFrecuentes()
        {
            List<PreguntasFrecuentes> listado = new List<PreguntasFrecuentes> ();
            RespuestaGenerica<List<PreguntasFrecuentes>> response = new RespuestaGenerica<List<PreguntasFrecuentes>>();

            try
            {
                listado = await _preguntasFrecuentesService.ListarPreguntasFrecuentes();
                response.Mensaje = "Listado obtenido exitosamente";
                response.ObjetoRespuesta = listado;
                response.Estado = true;

                if (listado.Count == 0)
                {
                    response.Mensaje = "No se encontró nigngun resultado";
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

        [HttpPost("registrar")]
        public async Task<RespuestaGenerica<PreguntasFrecuentes>> RegistrarPreguntaFrecuente(PreguntasFrecuentes pre)
        {
            bool respuesta = false;
            RespuestaGenerica<PreguntasFrecuentes> response = new RespuestaGenerica<PreguntasFrecuentes>();

            try
            {
                respuesta = await _preguntasFrecuentesService.RegistrarPreguntasFrecuentes(pre);
                response.Mensaje = "Pregunta Registrada Correctamente";
                response.ObjetoRespuesta = pre;
                response.Estado = true;

                if (!respuesta)
                {
                    response.Mensaje = "No se pudo registrar la pregunta";
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

        [HttpPut("actualizar")]
        public async Task<RespuestaGenerica<PreguntasFrecuentes>> ModificarPreguntaFrecuente(PreguntasFrecuentes pre)
        {
            bool respuesta = false;
            RespuestaGenerica<PreguntasFrecuentes> response = new RespuestaGenerica<PreguntasFrecuentes>();

            try
            {
                respuesta = await _preguntasFrecuentesService.ModificarPreguntaFrecuente(pre);
                response.Mensaje = "Pregunta Modificada Correctamente";
                response.ObjetoRespuesta = pre;
                response.Estado = true;

                if (!respuesta)
                {
                    response.Mensaje = "No se pudo modificar la pregunta";
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

        [HttpDelete("eliminar/{id}")]
        public async Task<RespuestaGenerica<PreguntasFrecuentes>> EliminarPreguntaFrecuente(int id)
        {
            bool respuesta = false;
            RespuestaGenerica<PreguntasFrecuentes> response = new RespuestaGenerica<PreguntasFrecuentes>();

            try
            {
                respuesta = await _preguntasFrecuentesService.EliminarPreguntaFrecuentes(id);
                response.Mensaje = "Pregunta Eliminada Correctamente";
                response.ObjetoRespuesta = new PreguntasFrecuentes();
                response.Estado = true;

                if (!respuesta)
                {
                    response.Mensaje = "No se pudo eliminar la pregunta";
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
