using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.ViewModel
{
    public class SaleInsertInstalment
    {
        public Customer Customer { get; set; }

        public Sale Sale { get; set; }

        public Sale_Transaction_Log Sale_Transaction_Log { get; set; }

        public Inventory Inventory { get; set; }

        public Inventory_Details Inventory_Details { get; set; }

        public Item_Criteria Item_Criteria { get; set; }

        public Transaction_Master Transaction_Master { get; set; }

        public List<Transaction_Details> Transaction_Details { get; set; }

        public Instalment_Master Instalment_Master { get; set; }

        public Instalment_Details Instalment_Details { get; set; }
    }
}
