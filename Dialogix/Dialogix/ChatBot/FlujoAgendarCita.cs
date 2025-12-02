using Dialogix.ChatBot.Interfaces;
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

            EstadoConversacion? estado = Session.GetObject<EstadoConversacion>("OUser");
            estado!.EstadoActual = "AgendarCita;EscojerHorario";
            estado!.AgendarCita.IndexEspecialidad = input;
            Session.SetObject("OUser", estado);
            Session.SetObject("OListaDoctores", objeto);

            return respuesta;
        }

        public async Task<string> ListarHorariosDisponiblesPorDoctor(string input)
        {
            int idMedico = 0;
            List<Medico> estado = Session.GetObject<List<Medico>>("OListaDoctores")!;

            try { idMedico = estado.ElementAt(Convert.ToInt32(input) - 1).IdMedico; }
            catch (Exception ex) { return "No se pudo detectar al doctor, por favor vuelve a introducir el numero de doctor"; }

            List<DateTime> horariosDisponibles = await _horarioMedicoService.ObtenerHorariosDisponiblesFuturos(idMedico);

            string horasDisponibles = "";

            EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");

            if (horariosDisponibles.Count > 0)
            {
                horasDisponibles = "Escoje un horario";
                estadoConversacion!.EstadoActual = "AgendarCita;ResumenCita";
                estadoConversacion!.AgendarCita.IndexDoctor = input;
            }
            else
            {
                horasDisponibles = "No hay horarios disponibles para el doctor ese dia, por favor escoje otro doctor";
                return await ListarDoctoresSegunEspecialidad(estadoConversacion!.AgendarCita.IndexEspecialidad);
            }

            foreach (DateTime item in horariosDisponibles)
            {
                horasDisponibles += "|" + item.ToString("yyyy/MM/dd hh:mm tt");
            }

            Session.SetObject("OUser", estadoConversacion);
            Session.SetObject("OListaHorarios", horariosDisponibles);

            return horasDisponibles;
        }

        public string ResumenCita(string prompt)
        {
            string resumenCita = "Su cita está lista para ser generada, primero valide la información: ";

            EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");
            estadoConversacion!.AgendarCita.IndexHorario = prompt;
            estadoConversacion!.EstadoActual = "AgendarCita;ConfirmarCita";
            Session.SetObject("OUser", estadoConversacion);

            List<string> listaEspecialidades = Session.GetObject<List<string>>("OListaEspecialidades")!;
            List<Medico> listaDoctores = Session.GetObject<List<Medico>>("OListaDoctores")!;
            List<DateTime> listaHorarios = Session.GetObject<List<DateTime>>("OListaHorarios")!;
            DateTime fechaCita = listaHorarios.ElementAt(int.Parse(estadoConversacion.AgendarCita.IndexHorario) - 1);

            resumenCita += "|Paciente: " + estadoConversacion.NombrePaciente + ".";
            resumenCita += "|Especialidad: " + listaEspecialidades.ElementAt(int.Parse(estadoConversacion.AgendarCita.IndexEspecialidad) - 1) + ".";
            resumenCita += "|Medico: " + listaDoctores.ElementAt(int.Parse(estadoConversacion.AgendarCita.IndexDoctor) - 1).Nombre + ".";
            resumenCita += "|Horario: " + fechaCita.ToString("yyyy/MM/dd hh:mm tt");

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
                    mensaje = "Felicidades " + estadoConversacion.NombrePaciente + " su cita fue agendada correctamente, " +
                        "puede consultar informacion adicional mediante el chat, tenga buen dia";

                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
            }

            Session.Clear();
            return mensaje;
        }
    }
}
