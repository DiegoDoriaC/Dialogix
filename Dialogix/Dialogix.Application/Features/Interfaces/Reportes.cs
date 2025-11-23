using Dialogix.Application.Features.DTOs.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Interfaces
{
    public interface Reportes
    {
        Task<ReporteConversacionesResponse> ReporteConversaciones();
    }
}
