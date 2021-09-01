using Infrastructure.EF;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.MockFactory.Tests.Utils
{
    public interface IDatabaseInitializer
    {
        void InitializeDatabase(Action<ILogger<DatabaseInitializer>, DatabaseContext> initializeAction);
    }
}
