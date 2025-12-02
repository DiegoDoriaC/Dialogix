using Essalud.Domain;

namespace Essalud.Infraestructure.Repositories.Interfaces
{
    public interface IHorarioMedicoRepository
    {
        Task<HorarioMedico> ListarHorariosMedico(int idMedico, string fecha);
        Task<List<CitaMedica>> ListarHorariosOcupados(int idMedico, DateTime fecha);
    }
}
