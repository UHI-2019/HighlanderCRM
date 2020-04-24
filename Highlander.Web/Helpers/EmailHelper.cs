using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Highlander.Web.Helpers
{
    public class EmailHelper
    {
        private string _connectionString;
        private static Random random = new Random();
        IConfiguration configuration;

        public string getHostname()
        {
            _connectionString = configuration.GetConnectionString("Hostname");

            return _connectionString;
        }

        public string getEmailServer()
        {
            _connectionString = configuration.GetConnectionString("EmailServer");

            return _connectionString;
        }

        public string getEmailUserName()
        {
            _connectionString = configuration.GetConnectionString("EmailUsername");

            return _connectionString;
        }

        public string getEmailPassword()
        {
            _connectionString = configuration.GetConnectionString("EmailPassword");

            return _connectionString;
        }

        public string RandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
