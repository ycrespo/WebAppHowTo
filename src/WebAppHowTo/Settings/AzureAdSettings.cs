using System.Collections.Generic;

namespace WebAppHowTo.Settings
{
    public class AzureAdSettings
    {
        public string Instance { get; set; }
        public string Domain { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public IEnumerable<string> GroupsId { get; set; }
        public string CallbackPath { get; set; }
    }
}