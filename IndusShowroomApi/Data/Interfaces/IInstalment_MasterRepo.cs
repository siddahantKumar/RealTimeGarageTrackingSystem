using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IInstalment_MasterRepo
    {
        void InsertInstalmentMaster(Instalment_Master instalment_Master);
        void UpdateInstalmentMaster(Instalment_Master instalment_Master);
        Instalment_Master GetInstalment_Master(int IN_ID);
        List<Instalment_Master> GetInstalment_Masters();
        bool SaveChanges();
    }
}
