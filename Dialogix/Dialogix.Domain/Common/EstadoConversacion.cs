using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Domain.Common
{
    public class EstadoConversacion
    {
        public string EstadoActual { get; set; } = "";
        public int IdPaciente { get; set; }
        public string DniIngresado { get; set; } = "";
        public string NombrePaciente { get; set; } = "";
        public string CorreoPaciente { get; set; } = "";
        public AgendarCita AgendarCita { get; set; } = new AgendarCita();
        public int IdConversacion { get; set; } = 0;

    }
}
