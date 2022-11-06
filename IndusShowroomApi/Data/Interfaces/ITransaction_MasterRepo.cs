using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface ITransaction_MasterRepo
    {
        List<Transaction_Master> GetTransaction_Masters();
        Transaction_Master GetTransaction_Master(int OP_ID);
        Transaction_Master GetTransaction_MasterByIN_ID(int IN_ID);
        List<Transaction_Master> GetPurchaseTransaction_Master();
        void InsertTransaction_Master(Transaction_Master transaction_Master);
        bool SaveChanges();
    }
}
