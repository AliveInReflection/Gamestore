using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Logger.Interfaces;
using NLog;

namespace GameStore.Logger.Adapters
{
    public class NLogErrorsLoggerAdapter : IErrorsLogger
    {
        private ILogger logger;

        public NLogErrorsLoggerAdapter()
        {
            logger = LogManager.GetLogger("ErrorsLogger");
        }
        public void Log(string message)
        {
            logger.Error(message);
        }
    }
}
