using Essalud.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Infraestructure.Repositories.Interfaces
{
    public interface IPacientesRepository
    {
        Task<Paciente> BuscarUsuarioPorDni(Paciente paciente);
        Task<string> ObtenerUltimoDigitoDni(int idPaciente);
    }
}
