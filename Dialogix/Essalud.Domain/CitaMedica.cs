using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Domain
{
    public class CitaMedica
    {
        public int IdCitaMedica { get; set; }
        public Paciente Paciente { get; set; } = new Paciente();
        public Medico Medico { get; set; } = new Medico();
        public DateTime FechaCita { get; set; }
        public TimeOnly HoraCita { get; set; }
        public string Motivo { get; set; } = "";
        public string Estado { get; set; } = "";
    }
}
