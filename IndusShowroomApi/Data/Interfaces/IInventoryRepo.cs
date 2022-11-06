using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IInventoryRepo
    {
        List<Inventory> GetInventories();
        void InsertInventory(Inventory inventory);
        void UpdateInventory(Inventory inventory);
        bool SaveChanges();
    }
}
