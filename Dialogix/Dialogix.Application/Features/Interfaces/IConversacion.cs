using Dialogix.Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Interfaces
{
    public interface IConversacion
    {
        Task<ConversacionResponse> ConversarConLaIA (ConversacionRequest request);
    }
}
