using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hippra
{
    public class AppSettings
    {
        public string StorageConnectionString { get; set; }
        public string StorageRootContainer { get; set; }

        public string FTManagerUsr { get; set; }
        public string FTManagerPwd { get; set; }

        public string FTEmailAccount { get; set; }
        public string FTEmailCred { get; set; }
    }
}
