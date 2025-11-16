using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Domain
{
    public class Mensaje
    {
        public int IdMensaje { get; set; }
        public int IdConversacion { get; set; }
        public string Texto { get; set; } = "";
        public string Respuesta { get; set; } = "";
        public DateTime Fecha { get; set; }
    }
}
