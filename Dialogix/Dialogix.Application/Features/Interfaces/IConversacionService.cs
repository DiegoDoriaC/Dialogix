using Dialogix.Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Interfaces
{
    public interface IConversacionService
    {
        Task<string> IniciarConversacion();
        Task<string> ReiterarInicioConversacion();
        Task<string> IniciarConversacionConLaIA();
    }
}
