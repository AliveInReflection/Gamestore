using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Logger.Interfaces
{
    public interface IGameStoreLogger
    {
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);

        void Warn(Exception e);
        void Error(Exception e);
        void Fatal(Exception e);
    }
}
