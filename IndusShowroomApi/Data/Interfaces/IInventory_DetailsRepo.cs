using IndusShowroomApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IInventory_DetailsRepo
    {
        List<Inventory_Details> GetInventory_Details();
        List<Inventory_Details> GetSearchedInventory_Details(string search);
        void InsertInventory_Detail(Inventory_Details inventory_Details);
        void UpdateInventory_Detail(Inventory_Details inventory_Details);
        bool SaveChanges();
    }
}
