using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlSale_LineRepo : ISale_LineRepo
    {
        private readonly DatabaseContext context;

        public List<Sale_Line> GetSale_Lines()
        {
            return context.Sale_Line.ToList();
        }

        public SqlSale_LineRepo(DatabaseContext context)
        {
            this.context = context;
        }
        public void InsertSale_Line(Sale_Line sale_Line)
        {
            context.Sale_Line.Add(sale_Line);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
