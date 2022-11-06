using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlSaleRepo : ISaleRepo
    {

        private readonly DatabaseContext context;

        public SqlSaleRepo(DatabaseContext context)
        {
            this.context = context;
        }
        public Sale GetSale(int sale_Id)
        {
            return context.Sale.FirstOrDefault(x => x.SALE_ID == sale_Id);
        }

        public List<Sale> GetSales()
        {
            List<Sale> sales = new List<Sale>();
            foreach (var item in context.Sale.ToList())
            {
                if (!item.IsDelete)
                {
                    sales.Add(item);
                }
            }

            return sales;
        }

        public void InsertSale(Sale sale)
        {
            context.Sale.Add(sale);
        }

        public void UpdateSale(Sale sale)
        {
            context.Sale.Update(sale);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }


    }
}
