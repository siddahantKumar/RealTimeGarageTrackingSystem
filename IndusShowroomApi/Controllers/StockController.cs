using IndusShowroomApi.Data.Services;
using IndusShowroomApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IndusShowroomApi.Controllers
{
    //[Authorize]
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly ISaleService saleService;

        public StockController(ISaleService saleService)
        {
            this.saleService = saleService;
        }

        [HttpGet("stock")]
        public ActionResult<StockModel> GetStock()
        {
            var stock = saleService.GetStock();
            if (stock.Count > 0)
            {
                return Ok(stock);
            }
            return NoContent();
        }

        [HttpGet("searchedstock/{search}")]
        public ActionResult<List<StockModel>> SearchedStock(string search)
        {
            return saleService.SearchedStocks(search);
        }

        //[HttpGet("detailssales")]
        //public ActionResult<StockDto> GetDetailsSale()
        //{
        //    var sales = saleService.GetDetailsSale();
        //    if (sales.Count > 0)
        //    {
        //        return Ok(sales);
        //    }
        //    return NoContent();
        //}

    }
}
