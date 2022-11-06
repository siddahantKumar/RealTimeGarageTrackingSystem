using IndusShowroomApi.Data.Services;
using IndusShowroomApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IndusShowroomApi.Controllers
{
    //[Authorize]
    [Route("api/purchase")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        [HttpGet("purchaseinvoices")]
        public ActionResult<Invoice> GetPurchaseInvoices()
        {
            var purchaseInvoices = purchaseService.GetPurchaseInvoices();
            if (purchaseInvoices != null)
            {
                return Ok(purchaseInvoices);
            }

            return NoContent();
        }

        [HttpPost("purchaseinsert")]
        public ActionResult<Dictionary<string, string>> PurchaseInsert(PurchaseInsert purchaseInsert)
        {
            Dictionary<string, string> response = (Dictionary<string, string>)purchaseService.PurchaseInsert(purchaseInsert);
            if (response.ContainsKey("purchaseSuccess"))
                return Ok(response);
            else if (response.ContainsKey("purchaseFailure"))
                return StatusCode(500, response);
            else if (response.ContainsKey("purchaseExists"))
                return StatusCode(409, response);
            else
                return StatusCode(500, response);
        }
    }
}
