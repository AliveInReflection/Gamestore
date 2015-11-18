using System;
using System.Web.Security;
using Microsoft.Owin.Security.DataProtection;

namespace GameStore.Auth.Infrastructure
{
    public class TicketProtector : IDataProtector
    {
        private string purpose;

        public TicketProtector(string purpose)
        {
            this.purpose = purpose;
        }

        public byte[] Protect(byte[] userData)
        {
            return MachineKey.Protect(userData, purpose);
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            try
            {
                return MachineKey.Unprotect(protectedData, purpose);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
