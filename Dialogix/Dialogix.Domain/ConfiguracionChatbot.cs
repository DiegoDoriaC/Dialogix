using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Domain
{
    public class ConfiguracionChatbot
    {
        public int IdConfig { get; set; }

        public string MensajeBienvenida { get; set; } = string.Empty;
        public string MensajeFueraHorario { get; set; } = string.Empty;
        public string MensajeMantenimiento { get; set; } = string.Empty;

        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        public bool HabilitarFueraHorario { get; set; }

        public int MaxMensajes { get; set; }
        public int TimeoutSegundos { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
