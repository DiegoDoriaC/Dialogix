using Essalud.Application.Feature.Interfaces;
using Essalud.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Application.Feature.Services
{
    public class ResultadosService : IResultadosService
    {
        private readonly IResultadosService _resultadosService;

        public ResultadosService(IResultadosService resultadosService)
        {
            _resultadosService = resultadosService;
        }

        public async Task<ResultadosClinicos> ConsultarEstadoResultados(ResultadosClinicos resultados)
        {
            ResultadosClinicos resultadosObj = await _resultadosService.ConsultarEstadoResultados(resultados);
            if (resultadosObj.IdResultadosClinicos == 0)
                throw new Exception("No se encontró ningun resultado");

            return resultadosObj;
        }
    }
}
