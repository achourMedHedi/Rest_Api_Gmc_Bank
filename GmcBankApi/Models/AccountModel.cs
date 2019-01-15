using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GmcBankApi.Models
{
    public class AccountModel
    {
        [JsonRequired]
        public int ownerCin { get; set; }
        [JsonRequired]
        public long accountNumber { get; set; }
        [JsonRequired]
        public string type { get; set; }
    }
}
