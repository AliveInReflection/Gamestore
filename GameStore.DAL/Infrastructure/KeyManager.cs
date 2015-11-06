using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Infrastructure
{
    public static class KeyManager
    {
        private const int coefficient = 3;

        public static int Coefficient {
            get { return coefficient; }
        }

        public static int Encode(int key, DatabaseType databaseType)
        {
            return key*coefficient + (int) databaseType;
        }

        public static int Decode(int key)
        {
            return key / coefficient;
        }

        public static DatabaseType GetDatabase(int key)
        {
            return (DatabaseType)(key % coefficient);
        }


    }
}
