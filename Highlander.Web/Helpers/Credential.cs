using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Highlander.Web.Helpers
{
    public class Credential
    {
        public static string EmailServer { get; set; }
        public static string EmailUsername { get; set; }
        public static string EmailPassword { get; set; }
        public static string AmazonS3AccessKeyID { get; set; }
        public static string AmazonS3Secret { get; set; }
        public static string AmazonBucket { get; set; }
        public static string Hostname { get; set; }
    }
}

