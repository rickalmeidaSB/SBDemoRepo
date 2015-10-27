using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
#pragma warning disable 0649 // Expected warnings in JSON classes
namespace SmartBear.Collab
{
    public class SessionService
    {
        public class getLoginTicket
        {
            public string login;
            public string password;
        }

        public class getLoginTicketResponse
        {
            public string loginTicket;
        }

        public class authenticate
        {
            public string login;
            public string ticket;
        }

        public class authenticateResponse
        {
            // void response return type
        }

        public class setMetadata
        {
            public string clientName;
            public string expectedServerVersion;
        }

        public class setMetadataResponse
        {
            // void response return type
        }
    }
}
#pragma warning restore 0649 // Expected warnings in JSON classes