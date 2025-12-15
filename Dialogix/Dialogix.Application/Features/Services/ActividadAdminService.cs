using Dialogix.Application.Features.Interfaces;
using Dialogix.Infrastructure.Repositories;
using Dialogix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Services
{
    public class ActividadAdminService : IActividadAdminService
    {
        private readonly IActividadAdminRepository _repo;

        public ActividadAdminService(IActividadAdminRepository repo)
        {
            _repo = repo;
        }

        public async Task RegistrarActividad(
            int idUsuario,
            string modulo,
            string accion,
            string? detalle = null)
        {
            ActividadAdmin actividad = new()
            {
                IdUsuario = idUsuario,
                Modulo = modulo,
                Accion = accion,
                Detalle = detalle
            };

            await _repo.RegistrarActividad(actividad);
        }

        public async Task<List<ActividadAdmin>> ObtenerUltimasActividades(int top)
        {
            return await _repo.ListarUltimasActividades(top);
        }
    }
}