using Dialogix.ChatBot.Interfaces;
using Dialogix.Domain;
using Dialogix.Domain.Common;
using Dialogix.Helpers;
using Essalud.Application.Feature.Interfaces;
using Essalud.Domain;

namespace Dialogix.ChatBot
{
    public class FlujoCancelarCita : IFlujoCancelarCita
    {
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        private readonly ICitasMedicasService _citasMedicasService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FlujoCancelarCita(ICitasMedicasService citasMedicasService, IHttpContextAccessor httpContextAccessor)
        {
            _citasMedicasService = citasMedicasService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> MostrarInformacionCitaMedica()
        {
            string mensaje = "Usted tiene programada la siguiente cita: ";
            EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");
            CitaMedica cita = new CitaMedica();
            cita.Paciente.IdPaciente = estadoConversacion!.IdPaciente;
            cita.Estado = "Programada";

            try
            {
                List<CitaMedica> listadoCitas = await _citasMedicasService.HistorialCitasMedicas(cita);
                CitaMedica primeraCita = listadoCitas.FirstOrDefault()!;
                mensaje += "|Paciente: " + primeraCita.Paciente.Nombre;
                mensaje += "|Doctor: " + primeraCita.Medico.Nombre;
                mensaje += "|Motivo: " + primeraCita.Motivo;
                mensaje += "|Fecha: " + primeraCita.FechaCita.ToString("dd/MM/yyyy 'a las' hh:mm tt");
                mensaje += "|¿Está seguro que desea cancelar su cita médica?, el cancelar sus citas " +
                    "repetitivamente le prohibirá agendar citas virtualmente en el futuro";
                Session.SetObject("OCita", primeraCita);
            }
            catch (Exception ex)
            {
                Session.Clear();
                return ex.Message;
            }

            estadoConversacion.EstadoActual = "CancelarCita";
            Session.SetObject("OUser", estadoConversacion);

            return mensaje;
        }

        public async Task<string> ConfirmarCancelarCita(string prompt)
        {
            EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");
            CitaMedica? citaMedica = Session.GetObject<CitaMedica>("OCita");

            string fechaFormateada = citaMedica!.FechaCita.ToString("dd/MM/yyyy 'a las' hh:mm tt");

            string mensaje = "Estimado(a) " + estadoConversacion!.NombrePaciente +
                ", su cita programada para el " + fechaFormateada + " fue cancelada exitosamente";

            try
            {
                if (prompt == "1")
                    await _citasMedicasService.CancelarCitaMedica(citaMedica!);
                else
                    mensaje = "La cita se ha mantenido sin cambios";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            Session.Clear();
            return mensaje;
        }


    }
}
