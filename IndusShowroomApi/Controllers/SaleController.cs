using IndusShowroomApi.Data.Services;
using IndusShowroomApi.Dtos;
using IndusShowroomApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IndusShowroomApi.Controllers
{
    //[Authorize]
    [Route("api/sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService saleService;

        public SaleController(ISaleService saleService)
        {
            this.saleService = saleService;
        }

        [HttpPost("saleinsert")]
        public ActionResult<IDictionary<string, string>> InsertSale(SaleInsert saleInsert)
        {
            Dictionary<string, string> response = (Dictionary<string, string>)saleService.InsertSale(saleInsert);
            if (response.ContainsKey("saleSuccess"))
                return Ok(response);
            else if (response.ContainsKey("saleFailure"))
                return StatusCode(500, response);
            else
                return StatusCode(500, response);
        }


        [HttpPost("saleinstalmentinsert")]
        public ActionResult<IDictionary<string, string>> InsertSaleInstalement(SaleInsertInstalment saleInsertInstalement)
        {
            Dictionary<string, string> response = (Dictionary<string, string>)saleService.InsertSaleInstalment(saleInsertInstalement);
            if (response.ContainsKey("saleSuccess"))
                return Ok(response);
            else if (response.ContainsKey("saleFailure"))
                return StatusCode(500, response);
            else
                return StatusCode(500, response);
        }


        [HttpGet("saleinvoices")]
        public ActionResult<Invoice> GetSaleInvoices()
        {
            return Ok(saleService.GetSaleInvoices());
        }
    }
}
