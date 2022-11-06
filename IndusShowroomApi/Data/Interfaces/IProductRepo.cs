using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IProductRepo
    {
        List<Product> GetProducts();
        Product GetProduct(int product_id);
        void InsertProduct(Product product);
        void UpdateProduct(Product product);
        bool SaveChanges();
    }
}
