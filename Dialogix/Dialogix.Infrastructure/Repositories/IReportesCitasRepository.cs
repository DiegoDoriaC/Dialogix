using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Infrastructure.Repositories
{
    public interface IReportesCitasRepository
    {
        Task<int> ObtenerTotalCitasAgendadas();

    }
}
