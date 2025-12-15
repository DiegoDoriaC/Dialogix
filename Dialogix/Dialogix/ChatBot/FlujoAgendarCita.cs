using Dialogix.ChatBot.Interfaces;
using Dialogix.Correos;
using Dialogix.Domain.Common;
using Dialogix.Helpers;
using Essalud.Application.Feature.Interfaces;
using Essalud.Domain;
using Essalud.Infraestructure.Repositories.Interfaces;

namespace Dialogix.ChatBot
{
    public class FlujoAgendarCita : IFlujoAgendarCita
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMedicosRepository _medicosRepository;
        private readonly IHorariosMedicoService _horarioMedicoService;
        private readonly IEspecialidadRepository _especialidadRepository;
        private readonly ICitasMedicasService _citasMedicasService;
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public FlujoAgendarCita(IHttpContextAccessor httpContextAccessor, IMedicosRepository medicosRepository,
            IHorariosMedicoService horarioMedicoService, IEspecialidadRepository especialidadRepository, ICitasMedicasService citasMedicasService)
        {
            _httpContextAccessor = httpContextAccessor;
            _medicosRepository = medicosRepository;
            _horarioMedicoService = horarioMedicoService;
            _especialidadRepository = especialidadRepository;
            _citasMedicasService = citasMedicasService;
        }

        public async Task<string> ListarEspecialidades()
        {
            string especialidades = "Indique a la especialidad a la que quiere ir: ";
            EstadoConversacion? estado = Session.GetObject<EstadoConversacion>("OUser");
            if (estado == null) return "Su tiempo de session ha terminado, vuelva a generar la consulta";

            List<string> listadoEspecialidades = await _especialidadRepository.ListarEspecialidades();

            estado.EstadoActual = "AgendarCita";
            estado.HistorialPasos.Clear();
            Session.SetObject("OUser", estado);
            Session.SetObject("OListaEspecialidades", listadoEspecialidades);

            foreach (string item in listadoEspecialidades)
            {
                especialidades += "|" + item;
            }

            return especialidades;

        }

        public async Task<string> ListarDoctoresSegunEspecialidad(string input)
        {
            string especialidad = "";
            if (input.Trim().Replace(".", "") == "1") especialidad = "Cardiología";
            if (input.Trim().Replace(".", "") == "2") especialidad = "Dermatología";
            if (input.Trim().Replace(".", "") == "3") especialidad = "Emergencias";
            if (input.Trim().Replace(".", "") == "4") especialidad = "Ginecología";
            if (input.Trim().Replace(".", "") == "5") especialidad = "Medicina General";
            if (input.Trim().Replace(".", "") == "6") especialidad = "Odontología";
            if (input.Trim().Replace(".", "") == "7") especialidad = "Pediatría";



            string respuesta = "Escoja un doctor: ";
            List<Medico> objeto = await _medicosRepository.ListarMedicosSegunEspecialidad(especialidad);
            foreach (Medico item in objeto)
            {
                respuesta += "|" + item.Nombre;
            }

            respuesta += "|⬅ Volver";

            EstadoConversacion? estado = Session.GetObject<EstadoConversacion>("OUser");
            estado!.HistorialPasos.Push(estado.EstadoActual);
            estado.AgendarCita.IndexEspecialidad = input;
            estado.EstadoActual = "AgendarCita;EscojerHorario";
            Session.SetObject("OUser", estado);
            Session.SetObject("OListaDoctores", objeto);

            return respuesta;
        }

        public async Task<string> ListarHorariosDisponiblesPorDoctor(string input)
        {
            EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");
            if (estadoConversacion == null)
                return "Su sesión ha expirado, por favor vuelva a iniciar la consulta.";

            List<Medico>? listaDoctores = Session.GetObject<List<Medico>>("OListaDoctores");
            if (listaDoctores == null || listaDoctores.Count == 0)
                return "No se encontraron doctores en la sesión. Por favor vuelva a seleccionar la especialidad.";

            int idMedico;

            try
            {
                idMedico = listaDoctores.ElementAt(Convert.ToInt32(input) - 1).IdMedico;
            }
            catch
            {
                return "No se pudo detectar al doctor, por favor vuelva a introducir el número de doctor.";
            }

            List<DateTime> horariosDisponibles =
                await _horarioMedicoService.ObtenerHorariosDisponiblesFuturos(idMedico);

            string horasDisponibles;

            if (horariosDisponibles.Count > 0)
            {
                horasDisponibles = "Escoje un horario";
                estadoConversacion.AgendarCita.IndexDoctor = input;
                estadoConversacion.HistorialPasos.Push(estadoConversacion.EstadoActual);
                estadoConversacion.EstadoActual = "AgendarCita;Horarios";

            }
            else
            {
                horasDisponibles =
                    "No hay horarios disponibles para el doctor ese día, por favor escoja otro doctor.";

                return await ListarDoctoresSegunEspecialidad(
                    estadoConversacion.AgendarCita.IndexEspecialidad
                );
            }

            foreach (DateTime item in horariosDisponibles)
            {
                horasDisponibles += "|" + item.ToString("yyyy/MM/dd hh:mm tt");
            }

            horasDisponibles += "|⬅ Volver";

            Session.SetObject("OUser", estadoConversacion);
            Session.SetObject("OListaHorarios", horariosDisponibles);

            return horasDisponibles;
        }


        public string ResumenCita(string prompt)
        {
            string resumenCita = "Su cita está lista para ser generada, primero valide la información: ";

            EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");
            if (estadoConversacion == null)
                return "Su sesión ha expirado, por favor vuelva a iniciar.";

            estadoConversacion.AgendarCita.IndexHorario = prompt;

            estadoConversacion.HistorialPasos.Push(estadoConversacion.EstadoActual);

            estadoConversacion.EstadoActual = "AgendarCita;ConfirmarCita";
            Session.SetObject("OUser", estadoConversacion);

            List<string> listaEspecialidades = Session.GetObject<List<string>>("OListaEspecialidades")!;
            List<Medico> listaDoctores = Session.GetObject<List<Medico>>("OListaDoctores")!;
            List<DateTime> listaHorarios = Session.GetObject<List<DateTime>>("OListaHorarios")!;
            DateTime fechaCita = listaHorarios.ElementAt(
                int.Parse(estadoConversacion.AgendarCita.IndexHorario) - 1
            );

            resumenCita += "|Paciente: " + estadoConversacion.NombrePaciente + ".";
            resumenCita += "|Especialidad: " +
                listaEspecialidades.ElementAt(int.Parse(estadoConversacion.AgendarCita.IndexEspecialidad) - 1) + ".";
            resumenCita += "|Medico: " +
                listaDoctores.ElementAt(int.Parse(estadoConversacion.AgendarCita.IndexDoctor) - 1).Nombre + ".";
            resumenCita += "|Horario: " + fechaCita.ToString("dd/MM/yyyy 'a las' hh:mm tt");
            resumenCita += "|⬅ Volver";

            return resumenCita;
        }

        public async Task<string> ConfirmaCita(string prompt)
        {
            string mensaje = "Se canceló el registro de la cita, tenga buen día";

            EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");
            List<string> listaEspecialidades = Session.GetObject<List<string>>("OListaEspecialidades")!;
            List<Medico> listaDoctores = Session.GetObject<List<Medico>>("OListaDoctores")!;
            List<DateTime> listaHorarios = Session.GetObject<List<DateTime>>("OListaHorarios")!;

            int IndiceEspecialidades = int.Parse(estadoConversacion!.AgendarCita.IndexEspecialidad);
            int IndiceDoctor = int.Parse(estadoConversacion!.AgendarCita.IndexDoctor);
            int IndiceHorarios = int.Parse(estadoConversacion!.AgendarCita.IndexHorario);

            string especialidad = listaEspecialidades.ElementAt(int.Parse(estadoConversacion.AgendarCita.IndexEspecialidad) - 1);
            string medico = listaDoctores.ElementAt(int.Parse(estadoConversacion.AgendarCita.IndexDoctor) - 1).Nombre;

            if (prompt == "1")
            {
                CitaMedica cita = new CitaMedica();
                cita.Paciente.IdPaciente = estadoConversacion!.IdPaciente;
                cita.Medico.IdMedico = listaDoctores.ElementAt(IndiceDoctor - 1).IdMedico;
                cita.FechaCita = listaHorarios.ElementAt(IndiceHorarios - 1);
                cita.HoraCita = TimeOnly.FromDateTime(listaHorarios.ElementAt(IndiceHorarios - 1));
                cita.Motivo = "";
                cita.Estado = "Programada";

                try
                {
                    await _citasMedicasService.AgendarCitaMedica(cita);
                    try
                    {
                        EnvioCorreo mail = new EnvioCorreo();
                        mail.EnviarNotificacionRegistroCita(estadoConversacion, cita, especialidad, medico);
                    }
                    catch
                    {
                        Session.Clear();
                        return "Felicidades " + estadoConversacion.NombrePaciente + " su cita fue agendada correctamente pero NO se pudo enviar el correo, " +
                        "puede consultar informacion adicional mediante el chat, tenga buen dia";
                    }

                    mensaje = "Felicidades " + estadoConversacion.NombrePaciente + " su cita fue agendada correctamente, " +
                        "le hemos enviado a su correo electrónico el comprobante con el detalle de la cita." + " Puede consultar información adicional mediante el chat. ¡Que tenga un buen día!";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }
            estadoConversacion?.HistorialPasos.Clear();
            Session.Clear();
            return mensaje;
        }

       
    }
}
