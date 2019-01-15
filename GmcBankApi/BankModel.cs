using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GmcBankApi
{
    public class BankModel
    {
        [JsonRequired]
        public string name { get; set; }
        [JsonRequired]
        public int swiftCode { get; set; }
    }
}
