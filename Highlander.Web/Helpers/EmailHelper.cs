using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Highlander.Web.Helpers
{
    public class EmailHelper
    {
        private static readonly Random random = new Random();

        public string getHostname()
        {
            return Credential.Hostname;
        }

        public string getEmailServer()
        {
            return Credential.EmailServer;
        }

        public string getEmailUserName()
        {
            return Credential.EmailUsername;
        }

        public string getEmailPassword()
        {
            return Credential.EmailPassword;
        }

        public string RandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
