using Dialogix.Application.Features.Interfaces;
using Dialogix.ChatBot.Interfaces;
using Dialogix.Domain;
using Dialogix.Domain.Common;
using Dialogix.Helpers;
using Dialogix.Infrastructure.Repositories;
using Essalud.Application.Feature.Interfaces;
using Essalud.Domain;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;

namespace Dialogix.ChatBot
{
    public class InteraccionChatbot
    {
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        private readonly IOpenAIRepository _openAIRepository;
        private readonly IConversacionService _conversacionService;
        private readonly IPacientesService _pacientesService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFlujoAgendarCita _flujoAgendarCita;
        private readonly IFlujoCancelarCita _flujoCancelarCita;
        private readonly IFlujoConsultarResultados _flujoConsultarResultados;
        private readonly IFlujoHistorialCitas _flujoHistorialCitas;

        public InteraccionChatbot(IOpenAIRepository openAIRepository, IConversacionService conversacionService,
            IPacientesService pacientesService, IHttpContextAccessor httpContextAccessor, IFlujoAgendarCita flujoAgendarCita,
            IFlujoCancelarCita flujoCancelarCita, IFlujoConsultarResultados flujoConsultarResultados, IFlujoHistorialCitas flujoHistorialCitas)
        {
            _openAIRepository = openAIRepository;
            _conversacionService = conversacionService;
            _pacientesService = pacientesService;
            _httpContextAccessor = httpContextAccessor;
            _flujoAgendarCita = flujoAgendarCita;
            _flujoCancelarCita = flujoCancelarCita;
            _flujoConsultarResultados = flujoConsultarResultados;
            _flujoHistorialCitas = flujoHistorialCitas;
        }

        //private static int _numeroConsulta = 0;

        public async Task<string> EnviarMensajeAsync(string mensaje)
        {
            string respuesta = "";

            EstadoConversacion? estado = Session.GetObject<EstadoConversacion>("OUser");
            if (estado == null)
            {
                estado = new EstadoConversacion();
                estado.EstadoActual = "Esperando DNI";
                Session.SetObject("OUser", estado);
                return await IniciarConversacion();
            }

            if (estado.EstadoActual == "Esperando DNI")
            {
                string prompt1 = "Analiza estrictamente el siguiente texto: '" + mensaje + "'. " +
                    "Reglas: " +
                    "1. Un DNI peruano válido tiene exactamente 8 dígitos numéricos consecutivos. " +
                    "2. Si el texto contiene una secuencia de exactamente 8 dígitos consecutivos, responde únicamente: VALIDAR DNI. " +
                    "3. Si el texto NO contiene una secuencia de exactamente 8 dígitos consecutivos, responde únicamente: REITERAR DNI. " +
                    "4. No interpretes palabras, no completes, no asumas, no adivines, no analices contexto. Solo busca dígitos. " +
                    "5. No agregues nada más, no incluyas explicaciones, JSON, comillas ni texto adicional.";

                respuesta = await _openAIRepository.GenerarRespuestaAsync(prompt1);

                if (respuesta.Contains("VALIDAR DNI"))
                    return await ValidarDNI(mensaje);
                else if (respuesta.Contains("REITERAR DNI"))
                    return await ReiterarInicioConversacion();
            }

            if (estado.EstadoActual == "MenuPrincipal")
            {
                switch (mensaje.Trim().Replace(".", ""))
                {
                    case "1": return await _flujoAgendarCita.ListarEspecialidades();
                    case "2": return await _flujoCancelarCita.MostrarInformacionCitaMedica();
                    case "3": return await _flujoConsultarResultados.ConsultarEstadoResultados();
                    case "4": return await _flujoHistorialCitas.ConsultarHistorialUltimasCitas();
                    case "5": return await IniciarConversacionConLaIA();
                    case "6": return "Hardcodeado por judith";
                }

            }

            if (estado.EstadoActual.Contains("AgendarCita"))
            {
                if (estado.EstadoActual.Split(';').Length == 1)
                {
                    return await _flujoAgendarCita.ListarDoctoresSegunEspecialidad(mensaje);
                }
                else if (estado.EstadoActual.Split(';')[1] == "EscojerHorario")
                {
                    return await _flujoAgendarCita.ListarHorariosDisponiblesPorDoctor(mensaje);
                }
                else if (estado.EstadoActual.Split(';')[1] == "ResumenCita")
                {
                    return _flujoAgendarCita.ResumenCita(mensaje);
                }
                else if (estado.EstadoActual.Split(';')[1] == "ConfirmarCita")
                {
                    return await _flujoAgendarCita.ConfirmaCita(mensaje);
                }
            }
            else if (estado.EstadoActual.Contains("CancelarCita"))
            {
                if (estado.EstadoActual.Split(';').Length == 1)
                {
                    return await _flujoCancelarCita.ConfirmarCancelarCita(mensaje);
                }
            }
            else if (estado.EstadoActual.Contains("ConversarConIA"))
            {
                if (estado.EstadoActual.Split(';').Length == 1)
                {
                    return await ProcesarPromptConIA(mensaje);
                }
            }

            return respuesta;
        }

        public async Task<string> IniciarConversacion()
        {
            return await _conversacionService.IniciarConversacion();
        }

        public async Task<string> ReiterarInicioConversacion()
        {
            return await _conversacionService.ReiterarInicioConversacion();
        }

        public async Task<string> ValidarDNI(string dni)
        {
            string prompt = "Del siguiente texto: " + dni + " extrae únicamente el número de DNI." +
                " El DNI es un número de 8 dígitos consecutivos." +
                " Devuelve únicamente el número, sin texto adicional, sin frases, sin explicaciones, sin adornos.";

            string respuesta = await _openAIRepository.GenerarRespuestaAsync(prompt);
            try
            {
                Paciente paciente = await _pacientesService.BuscarUsuarioPorDni(respuesta);
                EstadoConversacion conversacion = new EstadoConversacion();
                conversacion.EstadoActual = "MenuPrincipal";
                conversacion.IdPaciente = paciente.IdPaciente;
                conversacion.DniIngresado = respuesta;
                conversacion.NombrePaciente = paciente.Nombre;
                Session.SetObject("OUser", conversacion);

                return "Un gusto tenerlo por aqui " + paciente.Nombre + " seleccione una opcion:\n\n" +
                       "1️⃣ Agendar cita médica\n" +
                       "2️⃣ Cancelar cita médica\n" +
                       "3️⃣ Consultar estado de resultados\n" +
                       "4️⃣ Ver historial de citas\n" +
                       "5️⃣ Conversar con tu IA\n" +
                       "6️⃣ Hablar con un asesor";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<string> IniciarConversacionConLaIA()
        {
            EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");
            estadoConversacion!.EstadoActual = "ConversarConIA";
            Session.SetObject("OUser", estadoConversacion);

            return await _conversacionService.IniciarConversacionConLaIA();
        }

        public async Task<string> ProcesarPromptConIA(string mensaje)
        {
            if (string.IsNullOrWhiteSpace(mensaje)) return "Por faovrv, introduzca su consulta";
            string prompt = "";
            //if(_numeroConsulta == 0)
            {
                //prompt = "En este flujo te encargaras de responder las preguntas del usuario pero estrictamente centrado en dar informacion sobre el flujo de la aplicacion o " +
                //    "temas centrados en el sistema de salud publico peruano EsSalud, tambien necesitas guardar el historial de lo que te escriba el usurio pero solo mientras dure " +
                //    "la conversacion, yo te diré cuando olvidar todo lo que el usuario te escribió, el prompt del usuario es: " + mensaje;

                prompt = "En este flujo te encargaras de responder las preguntas del usuario pero estrictamente centrado en dar informacion sobre el flujo de la aplicacion o " +
                    "temas centrados en el sistema de salud publico peruano EsSalud, OJO los usuario no tienen una cuenta como si de una red social se tratese si no solo debe " + "" +
                    "ingreasr su DNI para validarlo en el sistema de EsSalud, a continuacion las reglas de negocio y el flujo del sistema: " +
                    "-Flujo Agendar Cita: Para agendar una cita el usuario debe validar tu identidad, elegir una especialidad, elejir a un doctor y seleccionar uno de los " +
                    "horarios disponibles; las reglas de negocio para agendar cita son: no tener otra cita agenda porque solo se puede agendar una a la vez. " +
                    "-Flujo Cancelar Cita: Para canelar la cita tambien se necesita que el usuario haya validado su sesion, le aparecerá informacion de su cita y ahi deberá " +
                    "seleccion \"SÍ\" si desea cancelar su cita o \"NO\" si no desea cancelarla, la regla de negocio es que para no puede faltar menos de 24 horas para el inicio " +
                    "de la cita y que si cancela 2 veces sus citas programadas ya no podrá agendar mas citas virtualmente. " +
                    "-Flujo Consultar estado de resultados: despues que el usuario se haya validado le aparecerá los resultados de sus ultimas 3 exmanenes (solo se le mostrará " +
                    "el estado) " +
                    "-Flujo Ver historial de citas: despues de que el usuario se haya validado le aparecerá sus últimas 3 citas generadas en el sistema de essalud. " +
                    "Esas fueron todas las reglas y flujos del sistema, si no logras entender puedes responder con \"Puedo ayudarte con informacion sobre citas, resultados, servicio " +
                    "y procesos de EsSalud, ¿Podrías detallar un poco más tu consulta?\", recuerda que tienes que responder estrictamemte cosas relacionadas al sistema que tiene un enfoque " +
                    "a EsSalud (a menos que sea un \"hola\" o por ejemplo \"gracias\" esas cosas) con palabras no tecnicas faciles de entender para los pacientes, tampoco tienes " +
                    "que revelar flujos internos y tecnicos del sistema, si puedes que las respuestas que des no sean tan largas, NO MUESTRES HISTORIAL DE LA CONVERSACION, a continuacion te paso el prompt del usuario: ";
            }

            //_numeroConsulta++;
            prompt += mensaje;

            try
            {
                string respuestaIA = await _openAIRepository.GenerarRespuestaAsync(prompt);
                return respuestaIA.Replace("\n", "").Replace("*", "");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
