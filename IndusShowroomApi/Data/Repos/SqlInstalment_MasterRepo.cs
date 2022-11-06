using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlInstalment_MasterRepo : IInstalment_MasterRepo
    {
        private readonly DatabaseContext context;

        public SqlInstalment_MasterRepo(DatabaseContext context)
        {
            this.context = context;
        }
        public void InsertInstalmentMaster(Instalment_Master instalment_Master)
        {
            context.Instalment_Master.Add(instalment_Master);

        }
        public void UpdateInstalmentMaster(Instalment_Master instalment_Master)
        {
            context.Instalment_Master.Update(instalment_Master);
        }
        public Instalment_Master GetInstalment_Master(int IN_ID)
        {
            return context.Instalment_Master.FirstOrDefault(x => x.IN_ID == IN_ID);
        }
        public List<Instalment_Master> GetInstalment_Masters()
        {
            return context.Instalment_Master.ToList();
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
