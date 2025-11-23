using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Domain
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string Dni { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Sexo { get; set; } = "";
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; } = "";
        public string Correo { get; set; } = "";
        public string Direccion { get; set; } = "";
    }
}
