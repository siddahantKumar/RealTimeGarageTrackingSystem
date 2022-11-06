using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlProductRepo : IProductRepo
    {
        private DatabaseContext context;

        public SqlProductRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            foreach (var item in context.Product.ToList())
            {
                if (item.IsDelete != true)
                {
                    products.Add(item);
                }
            }
            return products;
        }

        public Product GetProduct(int product_id)
        {
            return context.Product.FirstOrDefault(x => x.PRODUCT_ID == product_id);
        }

        public void InsertProduct(Product product)
        {
            context.Product.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            context.Product.Update(product);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }


    }
}
