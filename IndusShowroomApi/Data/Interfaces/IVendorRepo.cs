using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface IVendorRepo
    {
        List<Vendor> GetVendors();
        void InsertVendor(Vendor vendor);
        bool SaveChanges();
    }
}
