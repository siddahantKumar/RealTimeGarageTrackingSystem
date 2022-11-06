using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlPurchase_Transaction_LogRepo : IPurchase_Transaction_LogRepo
    {
        private readonly DatabaseContext context;

        public SqlPurchase_Transaction_LogRepo(DatabaseContext context)
        {
            this.context = context;
        }
        public List<Purchase_Transaction_Log> GetPurchase_Transaction_Logs()
        {
            return context.Purchase_Transaction_Log.ToList();
        }

        public List<Purchase_Transaction_Log> GetNonDuplicate_Purchase_Transaction_Logs()
        {

            return context.Purchase_Transaction_Log.FromSqlRaw("select t1.* from purchase_transaction_log as t1 join (select max(CreateDate) as CreateDate from purchase_transaction_log group by ITEM_ID) as t2 on  t1.CreateDate = t2.CreateDate;").ToList();
        }

        public void InsertPurchase_Transaction_Log(Purchase_Transaction_Log purchase_Transaction_Log)
        {
            context.Purchase_Transaction_Log.Add(purchase_Transaction_Log);
        }
        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
