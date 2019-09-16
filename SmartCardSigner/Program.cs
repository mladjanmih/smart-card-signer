using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SmartCardSigner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GetCertificateFromSmartCard();
            CreateWebHostBuilder(args).Build().Run();
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

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
