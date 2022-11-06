using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlProduct_CategoryRepo : IProduct_CategoryRepo
    {
        private DatabaseContext context;

        public SqlProduct_CategoryRepo(DatabaseContext context)
        {
            this.context = context;
        }
        public List<Product_Category> GetProduct_Categories()
        {
            List<Product_Category> product_Categories = new List<Product_Category>();
            foreach (var item in context.Product_Category.ToList())
            {
                if (item.IsDelete != true)
                {
                    product_Categories.Add(item);
                }
            }
            return product_Categories;
        }

        public Product_Category GetProduct_Category(int product_Category_Id)
        {
            return context.Product_Category.FirstOrDefault(x => x.PRODUCT_CAT_ID == product_Category_Id);
        }

        public void InsertProduct_Category(Product_Category product_Category)
        {
            context.Product_Category.Add(product_Category);
        }

        public void UpdateProduct_Category(Product_Category product_Category)
        {
            context.Product_Category.Update(product_Category);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
