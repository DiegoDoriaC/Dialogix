using Essalud.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Infraestructure.Repositories
{
    public interface IResultadosRepository
    {
        Task<List<ResultadosClinicos>> ConsultarEstadoResultados(ResultadosClinicos resultado);
    }
}
