using Essalud.Application.Feature.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Application.Feature.Services
{
    public class CitaMedicaService : ICitasMedicasService
    {

        private readonly ICitasMedicasService _citasMedicasRepository;

        public CitaMedicaService(ICitasMedicasService citasMedicasRepository)
        {
            _citasMedicasRepository = citasMedicasRepository;
        }

        public Task<Domain.CitaMedica> AgendarCitaMedica(Domain.CitaMedica cita)
        {
            return _citasMedicasRepository.AgendarCitaMedica(cita);
        }

        public Task<Domain.CitaMedica> CancelarCitaMedica(Domain.CitaMedica cita)
        {
            throw new NotImplementedException();
        }

        public Task<List<Domain.CitaMedica>> HistorialCitasMedicas(Domain.CitaMedica cita)
        {
            throw new NotImplementedException();
        }
    }
}
