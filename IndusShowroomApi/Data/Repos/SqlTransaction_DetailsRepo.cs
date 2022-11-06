using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlTransaction_DetailsRepo : ITransaction_DetailsRepo
    {
        private DatabaseContext context;

        public SqlTransaction_DetailsRepo(DatabaseContext context)
        {
            this.context = context;
        }
        public List<Transaction_Details> GetTransaction_Details()
        {
            List<Transaction_Details> transaction_Details = new List<Transaction_Details>();
            foreach (var item in context.Transaction_Details.ToList())
            {
                if (item.IsDelete != true)
                    transaction_Details.Add(item);
            }
            return transaction_Details;
        }

        public void InsertTransaction_Details(Transaction_Details transaction_Details)
        {
            context.Transaction_Details.Add(transaction_Details);
        }

        public List<Transaction_Details> GetTransactionDetailByTM_ID(int TM_ID)
        {

            List<Transaction_Details> transaction_Details = new List<Transaction_Details>();
            foreach (var item in context.Transaction_Details.FromSqlRaw("SELECT * FROM transaction_details WHERE TM_ID = '" + TM_ID + "' order by CreateDate").ToList())
            {

                transaction_Details.Add(item);

            }
            return transaction_Details;
        }
        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
