using Essalud.Application.Feature.Interfaces;
using Essalud.Domain;
using Essalud.Infraestructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Application.Feature.Services
{
    public class ResultadosService : IResultadosService
    {
        private readonly IResultadosRepository _resultadosRepository;

        public ResultadosService(IResultadosRepository resultadosRepository)
        {
            _resultadosRepository = resultadosRepository;
        }

        public async Task<List<ResultadosClinicos>> ConsultarEstadoResultados(ResultadosClinicos resultados)
        {
            List<ResultadosClinicos> listaResultados = await _resultadosRepository.ConsultarEstadoResultados(resultados);
            if (listaResultados.Count == 0)
                throw new Exception("No se encontró ningun resultado");

            return listaResultados;
        }
    }
}
