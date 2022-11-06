using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface ITransaction_DetailsRepo
    {
        List<Transaction_Details> GetTransaction_Details();
        void InsertTransaction_Details(Transaction_Details transaction_Details);
        List<Transaction_Details> GetTransactionDetailByTM_ID(int TM_ID);
        bool SaveChanges();
    }
}
