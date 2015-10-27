using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
#pragma warning disable 0649 // Expected warnings in JSON classes
namespace SmartBear.Collab
{
    public class ServerInfoService
    {
        public class getVersion
        {
        }

        public class getVersionResponse
        {
            public string version;
        }

        public class getServerBuild
        {
        }

        public class getServerBuildResponse
        {
            public int serverBuild;
        }

        public class getMinimumJavaClientBuild
        {
        }

        public class getMinimumJavaClientBuildResponse
        {
            public int minimumJavaClientBuild;
        }
    }
}
#pragma warning restore 0649 // Expected warnings in JSON classes