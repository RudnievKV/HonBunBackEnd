using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public static class ApiRoutes
    {
        public static class Authorization
        {
            public const string Root = "https://univerauthorizationapi.azurewebsites.net";

            public const string Register = Root + "/api/auth/register";
            public const string Login = Root + "/api/auth/login";
        }
        public static class HonBunNoAnki
        {
            public const string Root = "https://univerhonbunnoankiapi.azurewebsites.net";
            public static class Users
            {
                public const string GetAll = Root + "/api/users";
            }
        }

    }
}
