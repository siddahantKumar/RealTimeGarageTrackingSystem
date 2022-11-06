using IndusShowroomApi.Models;
using Microsoft.EntityFrameworkCore;

namespace IndusShowroomApi.Data.DatabaseContexts
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt) { }

        public DbSet<Product_Brand> Product_Brand { get; set; }

        public DbSet<Product_Category> Product_Category { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Vendor> Vendor { get; set; }

        public DbSet<Sale> Sale { get; set; }

        public DbSet<Sale_Line> Sale_Line { get; set; }

        public DbSet<Sale_Transaction_Log> Sale_Transaction_Log { get; set; }

        public DbSet<Purchase> Purchase { get; set; }

        public DbSet<Purchase_Line> Purchase_Line { get; set; }

        public DbSet<Purchase_Transaction_Log> Purchase_Transaction_Log { get; set; }

        public DbSet<Inventory> Inventory { get; set; }

        public DbSet<Item_Criteria> Item_Criteria { get; set; }

        public DbSet<Inventory_Details> Inventory_Details { get; set; }

        public DbSet<Instalment_Master> Instalment_Master { get; set; }

        public DbSet<Instalment_Details> Instalment_Details { get; set; }

        public DbSet<Transaction_Details> Transaction_Details { get; set; }

        public DbSet<Transaction_Master> Transaction_Master { get; set; }

        public DbSet<Charts_Of_Accounts> Charts_Of_Accounts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<User_Type> User_Types { get; set; }

        public DbSet<Page_Routes> Page_Routes { get; set; }

    }
}