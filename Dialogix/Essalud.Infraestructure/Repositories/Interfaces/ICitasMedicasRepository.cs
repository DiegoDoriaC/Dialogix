using Essalud.Domain;
using Essalud.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Infraestructure.Repositories.Interfaces
{
    public interface ICitasMedicasRepository
    {
        Task<bool> AgendarCitaMedica(CitaMedica cita);
        Task<bool> CancelarCitaMedica(CitaMedica cita);
        Task<List<CitaMedica>> HistorialCitasMedicas(CitaMedica cita);
        Task<CitaMedica> InformacionCitaMedica(CitaMedica cita);
        Task<List<CitasPorEspecialidadDTO>> ListarCantidadConsultasPorEspecialidad(DateTime FechaInicio, DateTime FechaFin);
        Task<int> ObtenerTotalCitasAtendidas();
        Task<List<CitasPorEspecialidadDTO>> ListarCitasPorEspecialidadTotales();


    }
}
