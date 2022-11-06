using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IPageRoutesRepo
    {
        void InsertPageRoutes(Page_Routes page_Routes);
        List<Page_Routes> GetPage_Routes();
        List<Page_Routes> GetPage_RoutesId(int id);
        bool SaveChanges();
    }
}
