using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IProduct_CategoryRepo
    {
        List<Product_Category> GetProduct_Categories();
        Product_Category GetProduct_Category(int product_Category_Id);
        void InsertProduct_Category(Product_Category product_Category);
        void UpdateProduct_Category(Product_Category product_Category);
        bool SaveChanges();
    }
}
