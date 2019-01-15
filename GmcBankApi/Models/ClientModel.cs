using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GmcBankApi.Models
{
    public class ClientModel
    {
        [JsonRequired]
        public string name { get; set; }
        [JsonRequired]
        public int cin { get; set; }
    }
}
