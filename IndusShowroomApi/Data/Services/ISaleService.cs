using IndusShowroomApi.Dtos;
using IndusShowroomApi.Models;
using IndusShowroomApi.ViewModel;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Services
{
    public interface ISaleService
    {
        IDictionary<string, string> InsertSale(SaleInsert saleInsert);
        IDictionary<string, string> InsertSaleInstalment(SaleInsertInstalment saleInsertInstallement);
        List<Invoice> GetSaleInvoices();
        List<StockModel> SearchedStocks(string search);
        List<StockModel> GetStock();
        //List<StockDto> GetDetailsSale();

    }
}
