using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Enums
{
    [Flags]
    public enum Permissions
    {
        Create = 1,
        Retreive = 2,
        Update = 4,
        Delete = 8,
        Ban = 16,
        Crud = 15,
        Full = 31
    }
}
