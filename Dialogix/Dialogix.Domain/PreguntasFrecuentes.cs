using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Domain
{
    public class PreguntasFrecuentes
    {
        public int IdPreguntaFrecuente { get; set; }
        public string Descripcion { get; set; } = "";
        public string Estado { get; set; } = "";
        public int Orden { get; set; }
    }
}
