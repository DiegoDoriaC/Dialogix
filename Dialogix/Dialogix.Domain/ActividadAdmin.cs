using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Domain
{
    public class ActividadAdmin
    {
        public int IdActividad { get; set; }
        public int IdUsuario { get; set; }
        public string Modulo { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public string? Detalle { get; set; }
        public DateTime Fecha { get; set; }
        public string? AdminNombre { get; set; }
    }
}
