using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Common.DTOs
{
    public class RespuestaGenerica<T> where T : new()
    {
        public string Mensaje { get; set; } = "";
        public T ObjetoRespuesta { get; set; } = new T();
        public bool Estado { get; set; } // true si dió un resultado como se esperaba, false si no lo hizo
    }
}
