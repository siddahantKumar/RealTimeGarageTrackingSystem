using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlInvetory_DetailsRepo : IInventory_DetailsRepo
    {
        private readonly DatabaseContext context;

        public SqlInvetory_DetailsRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Inventory_Details> GetInventory_Details()
        {
            List<Inventory_Details> inventory_Details = new List<Inventory_Details>();

            foreach (var item in context.Inventory_Details.ToList())
            {
                if (!item.IsDelete)
                {
                    inventory_Details.Add(item);
                }
            }

            return inventory_Details;
        }

        public List<Inventory_Details> GetSearchedInventory_Details(string search)
        {
            return context.Inventory_Details.FromSqlRaw("SELECT * FROM inventory_details WHERE ChassisNumber LIKE '%" + search + "%';").ToList();
        }

        public void InsertInventory_Detail(Inventory_Details inventory_Details)
        {
            context.Inventory_Details.Add(inventory_Details);
        }

        public void UpdateInventory_Detail(Inventory_Details inventory_Details)
        {
            context.Inventory_Details.Update(inventory_Details);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
