using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.ViewModel
{
    public class SaleInsert
    {
        public Customer Customer { get; set; }
        public Sale Sale { get; set; }
        public Inventory_Details Inventory_Details { get; set; }
        public Inventory Inventory { get; set; }
        public Sale_Transaction_Log Sale_Transaction_Log { get; set; }
        public Transaction_Master Transaction_Master { get; set; }
        public List<Transaction_Details> Transaction_Details { get; set; }
    }
}
