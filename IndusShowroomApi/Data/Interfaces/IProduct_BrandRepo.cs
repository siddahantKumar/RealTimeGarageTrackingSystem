using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IProduct_BrandRepo
    {
        List<Product_Brand> GetProduct_Brands();
        Product_Brand GetProduct_Brand(int product_brand_id);
        void InsertProduct_Brand(Product_Brand product_Brand);
        void UpdateProduct_Brand(Product_Brand product_Brand);
        bool SaveChanges();
    }
}
