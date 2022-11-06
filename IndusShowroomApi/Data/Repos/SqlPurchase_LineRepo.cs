using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlPurchase_LineRepo : IPurchase_LineRepo
    {
        private readonly DatabaseContext context;

        public SqlPurchase_LineRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Purchase_Line> GetPurchase_Lines()
        {
            return context.Purchase_Line.ToList();
        }

        public void InsertPurchaseLine(Purchase_Line purchase_Line)
        {
            context.Purchase_Line.Add(purchase_Line);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
