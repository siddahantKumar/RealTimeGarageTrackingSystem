using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlSale_Transaction_LogRepo : ISale_Transaction_LogRepo
    {
        private readonly DatabaseContext context;

        public SqlSale_Transaction_LogRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Sale_Transaction_Log> GetSale_Transaction_Logs()
        {
            return context.Sale_Transaction_Log.ToList();
        }

        public List<Sale_Transaction_Log> GetNonDuplicate_Sale_Transaction_Logs(){
            return context.Sale_Transaction_Log.FromSqlRaw("select t1.* from sale_transaction_log as t1 join (select max(CreateDate) as CreateDate from sale_transaction_log group by ITEM_ID) as t2 on  t1.CreateDate = t2.CreateDate;").ToList();
        }

        public void InsertSale_Transaction_Log(Sale_Transaction_Log sale_Transaction_Log)
        {
            context.Sale_Transaction_Log.Add(sale_Transaction_Log);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
