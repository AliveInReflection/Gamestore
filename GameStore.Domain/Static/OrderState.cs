using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Static
{
    public enum OrderState
    {
        NotIssued = 1,
        NotPayed = 2,
        CheckingOut = 3,
        Complete = 4
    }
}
