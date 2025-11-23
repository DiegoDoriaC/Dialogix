using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.DTOs.Reportes
{
    public class ReporteConversacionesResponse
    {
        public int IdConversacion { get; set; }
        public string DniUsuario { get; set; } = "";
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Canal { get; set; } = "";
        public string Estado { get; set; } = "";
        public int IdMensaje { get; set; }
        public string Texto { get; set; } = "";
        public string Respuesta { get; set; } = "";
        public DateTime Fecha { get; set; }
        public int Calificacion { get; set; }
    }
}
