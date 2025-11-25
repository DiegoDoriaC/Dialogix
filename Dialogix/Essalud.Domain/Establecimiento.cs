using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essalud.Domain
{
    public class Establecimiento
    {
        public int IdEstablecimiento { get; set; }
        public string Nombre { get; set; } = "";
        public string Tipo { get; set; } = "";
        public string Region { get; set; } = "";
    }
}
