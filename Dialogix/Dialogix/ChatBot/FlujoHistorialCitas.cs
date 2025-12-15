using Dialogix.ChatBot.Interfaces;
using Dialogix.Domain.Common;
using Dialogix.Helpers;
using Essalud.Domain;
using Essalud.Application.Feature.Interfaces;
using Essalud.Infraestructure.Repositories.Interfaces;

namespace Dialogix.ChatBot
{
    public class FlujoHistorialCitas : IFlujoHistorialCitas
    {
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        private readonly ICitasMedicasService _citasMedicasRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FlujoHistorialCitas(ICitasMedicasService citasMedicasRepository, IHttpContextAccessor httpContextAccessor)
        {
            _citasMedicasRepository = citasMedicasRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> ConsultarHistorialUltimasCitas()
        {
            string mensaje = "";
            EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");
            CitaMedica cita = new CitaMedica();
            cita.Paciente.IdPaciente = estadoConversacion!.IdPaciente;

            try
            {            
                List<CitaMedica> listadoCitas = await _citasMedicasRepository.HistorialCitasMedicas(cita);
                foreach(CitaMedica item in listadoCitas)
                {
                    mensaje += "*|Especialidad: " + item.Medico.Especialidad;
                    mensaje += "|Fecha: " + item.FechaCita.ToShortDateString();
                    mensaje += "|Hora: " + item.FechaCita.ToString("hh:mm tt");
                    mensaje += "|Estado: " + item.Estado + "*";
                }
                mensaje += "|Solo se muestran las citas más recientes (max. 3). Para el historial completo debe ingresar al portal de EsSalud.";
            }
            catch (Exception ex)
            {
                Session.Clear();
                return ex.Message;
            }

            Session.Clear();
            return mensaje;
        }


    }
}
