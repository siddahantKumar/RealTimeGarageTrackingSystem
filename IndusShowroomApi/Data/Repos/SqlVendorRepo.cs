using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlVendorRepo : IVendorRepo
    {
        private DatabaseContext context;

        public SqlVendorRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Vendor> GetVendors()
        {
            List<Vendor> vendors = new List<Vendor>();

            foreach (var item in context.Vendor.ToList())
            {
                if (item.IsDelete != true)
                    vendors.Add(item);
            }

            return vendors;
        }

        public void InsertVendor(Vendor vendor)
        {
            context.Vendor.Add(vendor);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
