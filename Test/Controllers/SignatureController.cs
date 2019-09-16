using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCardSigner.Utilities;
using SmartCardSignerApi.Models;

namespace SmartCardSigner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignatureController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return new OkResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDigitalSignature([FromForm] string payload)
        {
       //     var payload = "maj pejloud";
            var certificate = CertificateUtilities.GetCertificateFromSmartCard();
            if (certificate == null)
            {
                return new BadRequestResult();
            }

            (string token, string algorithm) = JWSUtilities.CreateSignature(certificate, payload);

            var response = new SignatureResponse();
            response.Token = token;
            response.Certificate = Convert.ToBase64String(certificate.Export(System.Security.Cryptography.X509Certificates.X509ContentType.Cert));
            response.Algorithm = algorithm;
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "https://localhost:37101");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            return new ObjectResult(response);
        }


    }
}