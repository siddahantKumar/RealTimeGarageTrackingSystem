using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlPurchaseRepo : IPurchaseRepo
    {
        private readonly DatabaseContext context;

        public SqlPurchaseRepo(DatabaseContext context)
        {
            this.context = context;
        }
        public Purchase GetPurchase(int purchase_Id)
        {
            return context.Purchase.FirstOrDefault(x => x.PURCHASE_ID == purchase_Id);
        }

        public List<Purchase> GetPurchases()
        {
            List<Purchase> purchases = new List<Purchase>();
            foreach (var item in context.Purchase.ToList())
            {
                if (!item.IsDelete)
                {
                    purchases.Add(item);
                }
            }

            return purchases;
        }

        public void InsertPurchase(Purchase purchase)
        {
            context.Purchase.Add(purchase);
        }

        public void UpdatePurchase(Purchase purchase)
        {
            context.Purchase.Update(purchase);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }

    }
}
