﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SmartCardSigner.Utilities
{
    public class CertificateUtilities
    {
        public static X509Certificate2 GetCertificateFromSmartCard()
        {
            // Acquire public key stored in the default container of the currently inserted card
            CspParameters cspParameters = new CspParameters(1, "Microsoft Base Smart Card Crypto Provider");
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cspParameters);
            string pubKeyXml = rsaProvider.ToXmlString(false);

            // Find the certficate in the CurrentUser\My store that matches the public key
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            x509Store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2 certificate = null;
            foreach (X509Certificate2 cert in x509Store.Certificates)
            {
                if ((cert.PublicKey.Key.ToXmlString(false) == pubKeyXml))
                    if (cert.HasPrivateKey)
                        certificate = cert;
            }
            return certificate;
        }

    }
}
