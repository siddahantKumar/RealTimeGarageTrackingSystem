using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface ICharts_Of_AccountsRepo
    {
        List<Charts_Of_Accounts> GetCharts_Of_Accounts();
        Charts_Of_Accounts GetCharts_Of_Account(int ACC_ID);
        void InsertChart_Of_Accounts(Charts_Of_Accounts charts_Of_Accounts);
        bool SaveChanges();
    }
}
