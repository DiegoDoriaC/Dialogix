using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Domain
{
    public class ResultadosClinicos
    {
        public int IdResultadosClinicos { get; set; }
        public CitaMedica CitaMedica { get; set; } = new CitaMedica();
        public string TipoExamen { get; set; } = "";
        public string Valor { get; set; } = "";
        public DateTime FechaResultado { get; set; }
        public string Observaciones { get; set; } = "";
        public string Estado { get; set; } = "";
        public DateTime FechaRegistro { get; set; }
    }
}
