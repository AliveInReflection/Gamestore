using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Infrastructure
{
    public static class KeyManager
    {
        private const int Coefficient = 3;

        public static int Encode(int key, DatabaseType databaseType)
        {
            return key*Coefficient + (int) databaseType;
        }

        public static DatabaseType GetDatabase(int key)
        {
            return (DatabaseType)(key % Coefficient);
        }

    }
}
