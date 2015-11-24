using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CreditCardService.Static
{
    public static class CodeWordGenerator
    {
        private const string symbols = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";

        public static string Generate(int length)
        {
            var random = new Random(DateTime.UtcNow.Millisecond);
            var code = new StringBuilder();
            
            for (int i = 0; i < length; i++)
            {
                var index = random.Next(0, symbols.Length - 1);
                code.Append(symbols[index]);
            }

            return code.ToString();
        }
    }
}