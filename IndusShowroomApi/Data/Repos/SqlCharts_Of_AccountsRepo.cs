using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlCharts_Of_AccountsRepo : ICharts_Of_AccountsRepo
    {
        private readonly DatabaseContext context;

        public SqlCharts_Of_AccountsRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Charts_Of_Accounts> GetCharts_Of_Accounts()
        {
            return context.Charts_Of_Accounts.ToList();
        }

        public Charts_Of_Accounts GetCharts_Of_Account(int ACC_ID)
        {
            return context.Charts_Of_Accounts.FirstOrDefault(x => x.ACC_ID == ACC_ID);
        }

        public void InsertChart_Of_Accounts(Charts_Of_Accounts charts_Of_Accounts)
        {
            context.Charts_Of_Accounts.Add(charts_Of_Accounts);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() > 0);
        }
    }
}
