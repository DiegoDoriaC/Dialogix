using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Domain
{
    public class HorarioMedico
    {
        public int IdHorarioMedico { get; set; }
        public Medico Medico { get; set; } = new Medico();
        public string DiaSemana { get; set; } = "";
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
    } 
}
