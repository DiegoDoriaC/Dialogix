using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Application.Feature.Interfaces
{
    public interface IHorariosMedicoService
    {
        Task<List<string>> ObtenerHorariosDisponibles(int idMedico, DateTime fecha);
        Task<List<DateTime>> ObtenerHorariosDisponiblesFuturos(int idMedico, int semanas = 4);
    }
}
