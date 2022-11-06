using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Services
{
    public interface ISetupService
    {
        List<Product> GetProducts();
        Product GetProduct(int product_id);
        bool InsertProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);

        List<Product_Category> GetProduct_Categories();
        Product_Category GetProduct_Category(int product_cat_id);
        bool InsertProduct_Category(Product_Category product_Category);
        bool UpdateProduct_Category(Product_Category product_Category);
        bool DeleteProduct_Category(Product_Category product_Category);

        List<Product_Brand> GetProduct_Brands();
        Product_Brand GetProduct_Brand(int product_cat_id);
        bool InsertProduct_Brand(Product_Brand product_Brand);
        bool UpdateProduct_Brand(Product_Brand product_Brand);
        bool DeleteProduct_Brand(Product_Brand product_Brand);
    }
}
