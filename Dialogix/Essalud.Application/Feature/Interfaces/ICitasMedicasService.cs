using Essalud.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Application.Feature.Interfaces
{
    public interface ICitasMedicasService
    {
        Task<CitaMedica> AgendarCitaMedica(CitaMedica cita);
        Task<CitaMedica> CancelarCitaMedica(CitaMedica cita);
        Task<List<CitaMedica>> HistorialCitasMedicas(CitaMedica cita);
    }
}
