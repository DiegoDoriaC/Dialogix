using Essalud.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Infraestructure.Repositories.Interfaces
{
    public interface IMedicosRepository
    {
        Task<List<Medico>> ListarMedicosSegunEspecialidad(string especialidad);
    }
}
