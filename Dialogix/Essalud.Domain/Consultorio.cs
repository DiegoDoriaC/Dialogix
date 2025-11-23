using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Domain
{
    public class Consultorio
    {
        public int IdConsultorio { get; set; }
        public Establecimiento Establecimiento { get; set; } = new Establecimiento();
        public string Nombre { get; set; } = "";
        public string Especialidad { get; set; } = "";
    }
} 
