using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlTransaction_MasterRepo : ITransaction_MasterRepo
    {
        private DatabaseContext context;

        public SqlTransaction_MasterRepo(DatabaseContext context)
        {
            this.context = context;
        }
        public List<Transaction_Master> GetTransaction_Masters()
        {
            List<Transaction_Master> transaction_Masters = new List<Transaction_Master>();
            foreach (var item in context.Transaction_Master.ToList())
            {
                if (item.IsDelete != true)
                    transaction_Masters.Add(item);
            }
            return transaction_Masters;
        }

        public void InsertTransaction_Master(Transaction_Master transaction_Master)
        {
            context.Transaction_Master.Add(transaction_Master);
        }

        public List<Transaction_Master> GetPurchaseTransaction_Master()
        {
            List<Transaction_Master> transaction_Masters = new List<Transaction_Master>();
            foreach (var item in context.Transaction_Master.ToList())
            {
                if (item.Type == "P")
                    transaction_Masters.Add(item);
            }
            return transaction_Masters;
        }

        public Transaction_Master GetTransaction_Master(int OP_ID)
        {
            return context.Transaction_Master.ToList().FirstOrDefault(x => x.OP_ID == OP_ID);
        }

        public Transaction_Master GetTransaction_MasterByIN_ID(int IN_ID)
        {
            return context.Transaction_Master.ToList().FirstOrDefault(x => x.IN_ID == IN_ID);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
