using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface ISale_LineRepo
    {
        List<Sale_Line> GetSale_Lines();
        void InsertSale_Line(Sale_Line sale_Line);
        bool SaveChanges();
    }
}
