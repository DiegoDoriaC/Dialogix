using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Domain
{
    public class Feedback
    {
        public int IdFeedback { get; set; }
        public int idConversacion { get; set; }
        public int Calificacion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
