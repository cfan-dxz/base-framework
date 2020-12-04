using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSide.Framework.Auth
{
    public class AuthConfig
    {
        public string Authority { get; set; }
        public string ApiName { get; set; }
        public string DefaultScheme { get; set; }
    }
}
