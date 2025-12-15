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
        public Stack<string> HistorialPasos { get; set; } = new Stack<string>();
        public int IdPaciente { get; set; }
        public string DniIngresado { get; set; } = "";
        public string NombrePaciente { get; set; } = "";
        public string CorreoPaciente { get; set; } = "";
        public AgendarCita AgendarCita { get; set; } = new AgendarCita();
        public int IdConversacion { get; set; } = 0;
        public int IntentosDni { get; set; } = 0;
        public int IntentosCodigo { get; set; } = 0;
        public int IntentosFecha { get; set; } = 0;
        public DateTime UltimaInteraccion { get; set; } = DateTime.Now;
        public bool AvisoInactividadIAEnviado { get; set; } = false;
        public int TotalMensajes { get; set; } = 0;




    }
}
