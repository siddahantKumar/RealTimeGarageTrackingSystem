using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IPurchase_Transaction_LogRepo
    {
        List<Purchase_Transaction_Log> GetPurchase_Transaction_Logs();
        List<Purchase_Transaction_Log> GetNonDuplicate_Purchase_Transaction_Logs();
        void InsertPurchase_Transaction_Log(Purchase_Transaction_Log purchase_Transaction_Log);
        bool SaveChanges();
    }
}
