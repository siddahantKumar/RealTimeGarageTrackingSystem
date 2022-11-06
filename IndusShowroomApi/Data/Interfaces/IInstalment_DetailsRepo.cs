using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IInstalment_DetailsRepo
    {
        void InsertInstalmentDetail(Instalment_Details instalment_Details);
        void UpdateInstalment_Detail(Instalment_Details instalment_Detail);
        List<Instalment_Details> GetInstalment_Details();
        List<Instalment_Details> GetNonDuplicateInstalment_Details();
        bool SaveChanges();
    }
}