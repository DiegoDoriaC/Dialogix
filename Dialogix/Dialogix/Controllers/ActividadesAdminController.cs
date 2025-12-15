using Dialogix.Application.Common.DTOs;
using Dialogix.Application.Features.Interfaces;
using Dialogix.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Dialogix.Controllers
{
    [Controller]
    [Route("Dialogix/[controller]")]
    public class ActividadesAdminController : Controller
    {
        private readonly IActividadAdminService _service;

        public ActividadesAdminController(IActividadAdminService service)
        {
            _service = service;
        }

        [HttpGet("ultimas")]
        public async Task<RespuestaGenerica<List<ActividadAdmin>>> ObtenerUltimas(int top = 10)
        {
            var response = new RespuestaGenerica<List<ActividadAdmin>>();

            try
            {
                var lista = await _service.ObtenerUltimasActividades(top);

                response.Estado = true;
                response.Mensaje = "Actividades obtenidas correctamente";
                response.ObjetoRespuesta = lista;
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Mensaje = ex.Message;
            }

            return response;
        }
    }
}
