using System;
using System.Collections.Generic;
using System.Text;

namespace GmcBank.CustomAttribute
{
    [AttributeUsage(AttributeTargets.All)]
    class AutherAttribute : Attribute
    {
        public string name { get; set; }
    }
}
