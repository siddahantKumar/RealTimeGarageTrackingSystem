using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlProduct_BrandRepo : IProduct_BrandRepo
    {
        private DatabaseContext context;

        public SqlProduct_BrandRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Product_Brand> GetProduct_Brands()
        {
            List<Product_Brand> product_Brands = new List<Product_Brand>();
            foreach (var item in context.Product_Brand.ToList())
            {
                if (item.IsDelete != true)
                {
                    product_Brands.Add(item);
                }
            }
            return product_Brands;
        }

        public Product_Brand GetProduct_Brand(int product_brand_id)
        {
            return context.Product_Brand.FirstOrDefault(x => x.PRODUCT_BRAND_ID == product_brand_id);
        }

        public void InsertProduct_Brand(Product_Brand product_Brand)
        {
            context.Product_Brand.Add(product_Brand);
        }

        public void UpdateProduct_Brand(Product_Brand product_Brand)
        {
            context.Product_Brand.Update(product_Brand);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }

    }
}
