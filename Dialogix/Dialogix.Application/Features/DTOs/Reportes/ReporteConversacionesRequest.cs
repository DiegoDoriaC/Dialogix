using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.DTOs.Reportes
{
    public class ReporteConversacionesRequest
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DniUsuario { get; set; }
        public string Estado { get; set; } = "";
        public int Calificacion { get; set; }
        public string Canal { get; set; } = "";
    }
}
