using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlItem_CriteriaRepo : IItem_CriteriaRepo
    {
        private readonly DatabaseContext context;

        public SqlItem_CriteriaRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Item_Criteria> GetItem_Criterias()
        {
            return context.Item_Criteria.ToList();
        }
        public void InsertItem_Criteria(Item_Criteria item_Criteria)
        {
            context.Item_Criteria.Add(item_Criteria);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
