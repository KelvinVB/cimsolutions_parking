using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Helpers
{
    public class AccountDatabaseSettings : IAccountDatabaseSettings
    {
        public string AccountsCollectionName { get; set; }
        public string AuthenticationCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IAccountDatabaseSettings
    {
        string AccountsCollectionName { get; set; }
        string AuthenticationCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
