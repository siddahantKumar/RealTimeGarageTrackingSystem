using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Repos
{
    public class SqlCustomerRepo : ICustomerRepo
    {
        private DatabaseContext context;

        public SqlCustomerRepo(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            foreach (var item in context.Customer.ToList())
            {
                if (item.IsDelete != true)
                    customers.Add(item);
            }

            return customers;
        }

        public void InsertCustomer(Customer customer)
        {
            context.Customer.Add(customer);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}
