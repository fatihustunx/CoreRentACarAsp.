using Business.Abstracts;
using Entities.DTOs.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayOfRentsController : ControllerBase
    {
        private readonly IRentPaymentService _payOfRents;

        public PayOfRentsController(IRentPaymentService payOfRents)
        {
            _payOfRents = payOfRents;
        }

        [HttpPost("pay")]
        public IActionResult Pay(PayOfRent payOfRent)
        {
            var res = _payOfRents.PayOfRent(payOfRent);

            return Ok(res);
        }
    }
}