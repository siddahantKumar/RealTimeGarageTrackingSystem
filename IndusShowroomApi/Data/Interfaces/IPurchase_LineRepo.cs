using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IPurchase_LineRepo
    {
        List<Purchase_Line> GetPurchase_Lines();
        void InsertPurchaseLine(Purchase_Line purchase_Line);
        bool SaveChanges();
    }
}
