using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Interfaces
{
    public interface ICustomerRepo
    {
        List<Customer> GetCustomers();
        void InsertCustomer(Customer customer);
        bool SaveChanges();
    }
}
