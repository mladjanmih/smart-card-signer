using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCardSigner.Utilities;

namespace SmartCardSigner.Controllers
{
    [Route("signature")]
    [ApiController]
    public class SignatureController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return new OkResult();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDigitalSignature([FromBody]string payload)
        {
            var certificate = CertificateUtilities.GetCertificateFromSmartCard();
            if (certificate == null)
            {
                return new BadRequestResult();
            }

            var token = JWSUtilities.CreateSignature(certificate, payload);
            return new ObjectResult(token);
        }


    }
}