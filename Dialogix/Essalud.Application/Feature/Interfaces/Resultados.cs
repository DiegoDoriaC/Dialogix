using Essalud.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Application.Feature.Interfaces
{
    public interface Resultados
    {
        Task<ResultadosClinicos> ConsultarEstadoResultados(ResultadosClinicos resultados);
    }
}
