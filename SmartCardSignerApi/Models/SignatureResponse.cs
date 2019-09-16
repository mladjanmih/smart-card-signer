using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SmartCardSignerApi.Models
{
    [DataContract(Name = "signature-response")]
    [Serializable]
    public class SignatureResponse
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "certificate")]
        public string Certificate { get; set; }

        [DataMember(Name = "algorithm")]
        public string Algorithm { get; set; }
    }
}
