using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IItem_CriteriaRepo
    {
        List<Item_Criteria> GetItem_Criterias();
        void InsertItem_Criteria(Item_Criteria item_Criteria);
        bool SaveChanges();
    }
}
