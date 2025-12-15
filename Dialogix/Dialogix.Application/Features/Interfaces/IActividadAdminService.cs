using Dialogix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Interfaces
{
    public interface IActividadAdminService
    {
        Task RegistrarActividad(int idUsuario, string modulo, string accion, string? detalle = null);
        Task<List<ActividadAdmin>> ObtenerUltimasActividades(int top);
    }
}
