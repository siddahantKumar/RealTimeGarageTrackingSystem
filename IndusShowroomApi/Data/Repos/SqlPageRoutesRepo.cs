using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlPageRoutesRepo : IPageRoutesRepo
    {
        private readonly DatabaseContext context;

        public SqlPageRoutesRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public void InsertPageRoutes(Page_Routes page_Routes)
        {
            context.Page_Routes.Add(page_Routes);
        }

        public List<Page_Routes> GetPage_Routes()
        {
            return context.Page_Routes.ToList();
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
        public List<Page_Routes> GetPage_RoutesId(int id)
        {
            List<Page_Routes> page_Routes = new List<Page_Routes>();
            foreach (var item in context.Page_Routes.ToList())
            {
                if (item.USER_TYPE_ID == id)
                {
                    page_Routes.Add(item);
                }
            }
            return page_Routes;
        }
    }
}
