using GameStore.Logger.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Logger.Concrete
{
    public class NLogAdapter : IGameStoreLogger
    {
        private readonly ILogger logger = LogManager.GetLogger("ErrorsLogger");
        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }
    }
}
