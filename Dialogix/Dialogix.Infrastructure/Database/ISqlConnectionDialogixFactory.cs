using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Infrastructure.Database
{
    public interface ISqlConnectionDialogixFactory
    {
        IDbConnection CreateConnection();
    }
}
