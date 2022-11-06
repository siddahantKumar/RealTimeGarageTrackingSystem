using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface ISaleRepo
    {
        List<Sale> GetSales();
        Sale GetSale(int sale_Id);
        void InsertSale(Sale sale);
        void UpdateSale(Sale sale);
        bool SaveChanges();
    }
}
