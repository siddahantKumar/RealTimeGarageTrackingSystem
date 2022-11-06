using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IPurchaseRepo
    {
        List<Purchase> GetPurchases();
        Purchase GetPurchase(int purchase_Id);
        void InsertPurchase(Purchase purchase);
        void UpdatePurchase(Purchase purchase);
        bool SaveChanges();
    }
}
