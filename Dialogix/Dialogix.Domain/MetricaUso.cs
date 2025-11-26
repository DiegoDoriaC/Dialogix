using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Domain
{
    public class MetricaUso
    {
        public int IdMetrica { get; set; }
        public DateTime Fecha { get; set; }
        public int NumeroMes { get; set; }  // Uso especial para el filtrado de metricas por mes
        public int TotalConversaciones { get; set; }
    }
}
