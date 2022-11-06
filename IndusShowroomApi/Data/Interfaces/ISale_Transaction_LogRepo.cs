using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface ISale_Transaction_LogRepo
    {
        List<Sale_Transaction_Log> GetSale_Transaction_Logs();
        List<Sale_Transaction_Log> GetNonDuplicate_Sale_Transaction_Logs();
        void InsertSale_Transaction_Log(Sale_Transaction_Log sale_Transaction_Log);
        bool SaveChanges();
    }
}
