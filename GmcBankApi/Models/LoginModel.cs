using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GmcBankApi.Models
{
    public class LoginModel
    {
        [EmailAddress]
        [JsonRequired]
        public string Email { get; set; }
        [JsonRequired]
        public string Password { get; set; }

    }
}
