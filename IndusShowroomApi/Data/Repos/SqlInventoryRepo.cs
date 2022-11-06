using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlInventoryRepo : IInventoryRepo
    {

        private readonly DatabaseContext context;

        public SqlInventoryRepo(DatabaseContext context)
        {
            this.context = context;
        }
        public List<Inventory> GetInventories()
        {

            return context.Inventory.ToList();
        }

        public void InsertInventory(Inventory inventory)
        {
            context.Inventory.Add(inventory);
        }

        public void UpdateInventory(Inventory inventory)
        {
            context.Inventory.Update(inventory);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
