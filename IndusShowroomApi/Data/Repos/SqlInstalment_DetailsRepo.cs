using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlInstalment_DetailsRepo : IInstalment_DetailsRepo
    {
        private readonly DatabaseContext context;

        public SqlInstalment_DetailsRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public void InsertInstalmentDetail(Instalment_Details instalment_Details)
        {
            context.Instalment_Details.Add(instalment_Details);
        }

        public void UpdateInstalment_Detail(Instalment_Details instalment_Detail)
        {
            context.Instalment_Details.Update(instalment_Detail);
        }

        public List<Instalment_Details> GetInstalment_Details()
        {
            return context.Instalment_Details.ToList();
        }

        public List<Instalment_Details> GetNonDuplicateInstalment_Details()
        {
            return context.Instalment_Details.FromSqlRaw("select t1.* from instalment_details as t1 join (select max(CreateDate) as CreateDate from instalment_details group by IN_ID) as t2 on  t1.CreateDate = t2.CreateDate;").ToList();
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
