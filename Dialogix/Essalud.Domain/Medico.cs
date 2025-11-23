using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Domain
{
    public class Medico
    {
        public int IdMedico { get; set; }
        public Establecimiento Establecimiento { get; set; } = new Establecimiento();
        public string Nombre { get; set; } = "";
        public string Cmp { get; set; } = "";
        public string Especialidad { get; set; } = "";
    } 
}
