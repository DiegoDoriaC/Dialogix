using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Domain
{
    public class Conversacion
    {
        public int IdConversacion { get; set; }
        public int DniUsuario { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Canal { get; set; } = "";
        public string Estado { get; set; } = ""; 
    }
}
