using Dialogix.Application.Features.Interfaces;
using Dialogix.ChatBot.Interfaces;
using Dialogix.Domain.Common;
using Dialogix.Helpers;
using Dialogix.Infrastructure.Repositories;
using Essalud.Application.Feature.Interfaces;
using Essalud.Domain;

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

        public InteraccionChatbot(
            IOpenAIRepository openAIRepository,
            IConversacionService conversacionService,
            IPacientesService pacientesService,
            IHttpContextAccessor httpContextAccessor,
            IFlujoAgendarCita flujoAgendarCita,
            IFlujoCancelarCita flujoCancelarCita,
            IFlujoConsultarResultados flujoConsultarResultados,
            IFlujoHistorialCitas flujoHistorialCitas)
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

        public async Task<string> EnviarMensajeAsync(string mensaje)
        {
            if (mensaje == null) mensaje = string.Empty;
            mensaje = mensaje.Trim();

            string respuesta = string.Empty;
            EstadoConversacion? estado = Session.GetObject<EstadoConversacion>("OUser");

            if (estado == null)
            {
                estado = new EstadoConversacion
                {
                    EstadoActual = "Esperando DNI",
                    IdConversacion = 0
                };
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
                if (mensaje.Trim().Length == 8) respuesta = "VALIDAR DNI";

                if (respuesta != null && respuesta.Contains("VALIDAR DNI"))
                {
                    respuesta = await ValidarDNI(mensaje);
                }
                else
                {
                    respuesta = await ReiterarInicioConversacion();
                }

                await RegistrarSiCorresponde(mensaje, respuesta);
                return respuesta;
            }

            if (estado.EstadoActual == "SegundaValidacion")
            {
                string opcion = mensaje.Trim().Replace(".", "");
                return await ValidarUltimoDigitoDNI(opcion);
            }

            if (estado.EstadoActual == "MenuPrincipal")
            {
                string opcion = mensaje.Trim().Replace(".", "");
                switch (opcion)
                {
                    case "1":
                        respuesta = await _flujoAgendarCita.ListarEspecialidades();
                        break;
                    case "2":
                        respuesta = await _flujoCancelarCita.MostrarInformacionCitaMedica();
                        break;
                    case "3":
                        respuesta = await _flujoConsultarResultados.ConsultarEstadoResultados();
                        break;
                    case "4":
                        respuesta = await _flujoHistorialCitas.ConsultarHistorialUltimasCitas();
                        break;
                    case "5":
                        respuesta = await IniciarConversacionConLaIA();
                        break;
                    case "6":
                        respuesta = ObtenerCanalesAsesor();
                        await RegistrarSiCorresponde(mensaje, respuesta);
                        return respuesta;
                    default:
                        respuesta = "Opción no válida. Por favor seleccione una opción del menú.";
                        break;
                }

                await RegistrarSiCorresponde(mensaje, respuesta);
                return respuesta;
            }

            if (estado.EstadoActual.Contains("AgendarCita"))
            {
                if (estado.EstadoActual.Split(';').Length == 1)
                {
                    respuesta = await _flujoAgendarCita.ListarDoctoresSegunEspecialidad(mensaje);
                }
                else if (estado.EstadoActual.Split(';')[1] == "EscojerHorario")
                {
                    respuesta = await _flujoAgendarCita.ListarHorariosDisponiblesPorDoctor(mensaje);
                }
                else if (estado.EstadoActual.Split(';')[1] == "ResumenCita")
                {
                    respuesta = _flujoAgendarCita.ResumenCita(mensaje);
                }
                else if (estado.EstadoActual.Split(';')[1] == "ConfirmarCita")
                {
                    respuesta = await _flujoAgendarCita.ConfirmaCita(mensaje);
                }

                await RegistrarSiCorresponde(mensaje, respuesta);
                return respuesta;
            }

            if (estado.EstadoActual.Contains("CancelarCita"))
            {
                if (estado.EstadoActual.Split(';').Length == 1)
                {
                    respuesta = await _flujoCancelarCita.ConfirmarCancelarCita(mensaje);
                }

                await RegistrarSiCorresponde(mensaje, respuesta);
                return respuesta;
            }

            if (estado.EstadoActual.Contains("ConversarConIA"))
            {
                if (estado.EstadoActual.Split(';').Length == 1)
                {
                    respuesta = await ProcesarPromptConIA(mensaje);
                }

                await RegistrarSiCorresponde(mensaje, respuesta);
                return respuesta;
            }

            respuesta = "No entendí tu petición. Escribe 'menu' para volver al inicio.";

            await RegistrarSiCorresponde(mensaje, respuesta);
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

        public async Task<string> ValidarDNI(string dniTexto)
        {
            string prompt = "Del siguiente texto: " + dniTexto + " extrae únicamente el número de DNI." +
                " El DNI es un número de 8 dígitos consecutivos." +
                " Devuelve únicamente el número, sin texto adicional, sin frases, sin explicaciones, sin adornos.";

            string dniExtraido = await _openAIRepository.GenerarRespuestaAsync(prompt);

            if (dniTexto.Trim().Length == 8) dniExtraido = dniTexto;

            dniExtraido = dniExtraido?.Trim() ?? string.Empty;

            try
            {
                Paciente paciente = await _pacientesService.BuscarUsuarioPorDni(dniExtraido);

                int idConversacion = 0;
                if (int.TryParse(dniExtraido, out int dniParsed))
                {
                    idConversacion = await _conversacionService.RegistrarConversacion(dniParsed, "WEB");
                }

                EstadoConversacion conversacion = new EstadoConversacion
                {
                    EstadoActual = "SegundaValidacion",
                    IdPaciente = paciente.IdPaciente,
                    CorreoPaciente = paciente.Correo,
                    DniIngresado = dniExtraido,
                    NombrePaciente = paciente.Nombre,
                    IdConversacion = idConversacion
                };

                Session.SetObject("OUser", conversacion);

                string respuestaMenu = "Por favor, ingrese el código de verificacion de su DNI:\n\n";

                await RegistrarSiCorresponde(dniTexto, respuestaMenu);

                return respuestaMenu;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> ValidarUltimoDigitoDNI(string dniTexto)
        {
            string prompt = "Del siguiente texto extrae ÚNICAMENTE el código de verificación del DNI." +
                " El código es un solo dígito del 0 al 9." +
                " Devuelve solo el dígito, sin texto adicional, sin palabras, sin explicaciones. Si no encuentras el dígito de 1 solo caracter devuelvo la letra O. El texto es: " + dniTexto;

            string dniExtraido = await _openAIRepository.GenerarRespuestaAsync(prompt);

            if (dniTexto.Trim().Length == 1) dniExtraido = dniTexto;

            dniExtraido = dniExtraido?.Trim() ?? string.Empty;
            EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");

            try
            {
                string paciente = await _pacientesService.ComprobarUltimoDigitoDNI(estadoConversacion!.IdPaciente, dniExtraido);

                int idConversacion = 0;
                if (int.TryParse(dniExtraido, out int dniParsed))
                {
                    idConversacion = await _conversacionService.RegistrarConversacion(dniParsed, "WEB");
                }

                estadoConversacion.EstadoActual = "MenuPrincipal";

                Session.SetObject("OUser", estadoConversacion);

                string respuestaMenu = "Un gusto tenerlo(a) por aqui " + estadoConversacion.NombrePaciente + ", seleccione una opcion:\n\n" +
                       "1. Agendar cita médica\n" +
                       "2. Cancelar cita médica\n" +
                       "3. Consultar estado de resultados\n" +
                       "4. Ver historial de citas\n" +
                       "5. Conversar con tu IA\n" +
                       "6. Hablar con un asesor";

                await RegistrarSiCorresponde(dniTexto, respuestaMenu);

                return respuestaMenu;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> IniciarConversacionConLaIA()
        {
            EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");
            if (estadoConversacion != null)
            {
                estadoConversacion.EstadoActual = "ConversarConIA";
                Session.SetObject("OUser", estadoConversacion);
            }

            return await _conversacionService.IniciarConversacionConLaIA();
        }

        public async Task<string> ProcesarPromptConIA(string mensaje)
        {
            if (string.IsNullOrWhiteSpace(mensaje)) return "Por favor, introduzca su consulta";

            string prompt = "En este flujo te encargaras de responder las preguntas del usuario pero estrictamente centrado en dar informacion sobre el flujo de la aplicacion o " +
                    "temas centrados en el sistema de salud publico peruano EsSalud. A continuacion las reglas y flujos: " +
                    "-Flujo Agendar Cita: ... (resumen de reglas) " +
                    "-Flujo Cancelar Cita: ... " +
                    "-Flujo Consultar estado de resultados: ... " +
                    "-Flujo Ver historial de citas: ... " +
                    "No muestres historial de conversación. Responde de forma clara y breve. Prompt del usuario: ";

            prompt += mensaje;

            try
            {
                string respuestaIA = await _openAIRepository.GenerarRespuestaAsync(prompt);
                return respuestaIA?.Replace("\n", "").Replace("*", "") ?? "No se obtuvo respuesta de la IA";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string ObtenerCanalesAsesor()
        {
            try
            {
                EstadoConversacion? estadoConversacion = Session.GetObject<EstadoConversacion>("OUser");

                if (estadoConversacion == null)
                {
                    Session.Clear();
                    throw new Exception("Sin sesión activa");
                }

                if (estadoConversacion.IdConversacion > 0)
                {
                    _ = _conversacionService.FinalizarConversacion(estadoConversacion.IdConversacion);
                }

                string mensaje =
                    "CANALES_ASESOR|" +
                    "Telefono:(01) 411-8000|" +
                    "WhatsApp:+51 948 123 456|" +
                    "Correo:atencionalasegurado@essalud.gob.pe|" +
                    "Horario:Lunes a Sábado — 7:00 a.m. a 7:00 p.m.";

                Session.Clear();

                return mensaje;
            }
            catch (Exception)
            {
                Session.Clear();
                return string.Empty;
            }
        }

        private async Task RegistrarSiCorresponde(string textoUsuario, string respuesta)
        {
            try
            {
                EstadoConversacion? estadoActual = Session.GetObject<EstadoConversacion>("OUser");
                if (estadoActual != null && estadoActual.IdConversacion > 0)
                {
                    await _conversacion_service_safeRegistrarMensaje(estadoActual.IdConversacion, textoUsuario, respuesta);
                    await _conversacion_service_safeRegistrarMetrica();
                }
            }
            catch
            {
            }
        }

        private async Task _conversacion_service_safeRegistrarMensaje(int idConversacion, string texto, string respuesta)
        {
            try
            {
                await _conversacionService.RegistrarMensaje(idConversacion, texto ?? string.Empty, respuesta ?? string.Empty);
            }
            catch
            {
            }
        }

        private async Task _conversacion_service_safeRegistrarMetrica()
        {
            try
            {
                await _conversacionService.RegistrarMetrica();
            }
            catch
            {
            }
        }
    }
}
