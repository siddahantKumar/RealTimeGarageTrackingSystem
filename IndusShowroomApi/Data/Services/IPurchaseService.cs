using IndusShowroomApi.ViewModel;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Services
{
    public interface IPurchaseService
    {
        List<Invoice> GetPurchaseInvoices();
        IDictionary<string,string> PurchaseInsert(PurchaseInsert purchaseInsert);
    }
}
