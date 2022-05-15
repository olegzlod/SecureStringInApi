using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureStringInApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OlegsTryController : ControllerBase
    {
        

        private readonly ILogger<OlegsTryController> _logger;

        public OlegsTryController(ILogger<OlegsTryController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public AllData Post(CreditCard creditCard)
        {
            return new AllData
            {
                // original uploaded credit card data returned masked
                CreditCard = creditCard,
                // an example to get the sensitive data as a plain text
                OriginalData = creditCard.CreditCardNumber.SecureStringToString()
            };
            
        }
    }
}
