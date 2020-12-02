using System;
using System.Collections.Generic;
using System.Text;

namespace TimeBank.Core.DataAccess
{
    public class ConnectionData
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string ConnectionString { get; set; }

        public ConnectionData() { }
    }

}
