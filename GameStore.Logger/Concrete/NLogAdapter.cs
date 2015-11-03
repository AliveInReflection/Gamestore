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


        public void Warn(Exception e)
        {
            logger.Warn(BuildMessage(e));
        }

        public void Error(Exception e)
        {
            logger.Error(BuildMessage(e));
        }

        public void Fatal(Exception e)
        {
            logger.Fatal(BuildMessage(e));
        }


        private string BuildMessage(Exception e)
        {
            var message = new StringBuilder();
            message.Append("Type: " + e.GetType().Name + " | ");
            message.Append("Message: " + e.Message + " | ");
            message.Append("Where: " + e.StackTrace.Split('\n').Last());
            return message.ToString();
        }
    }
}
