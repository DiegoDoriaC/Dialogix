using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Domain
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Apellido { get; set; } = "";
        public string Nombre { get; set; } = "";
        public DateTime FechaNacimiento { get; set; }
        public string Rol { get; set; } = "";
        public string Estado { get; set; } = "";
        public string User { get; set; } = "";
        public string Contrasenia { get; set; } = "";
    }
}
