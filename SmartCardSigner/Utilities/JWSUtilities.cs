using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SmartCardSigner.Utilities
{
    public class JWSUtilities
    {
        public static string CreateSignature(X509Certificate2 certificate, string payload)
        {
            switch (certificate.PrivateKey.SignatureAlgorithm)
            {
                case "RSA":
                    var privateKey = certificate.GetRSAPrivateKey();
                    string token = Jose.JWT.Encode(payload, privateKey, JwsAlgorithm.RS256);
                    return token;
                    break;
                case "DSA":
                    break;

                case "EC":
                    break;
            }

            return null;
        }
    }
}
