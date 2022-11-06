using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.ViewModel
{
    public class PurchaseInsert
    {
        public Vendor Vendor { get; set; }
        public Purchase Purchase { get; set; }
        public Inventory Inventory { get; set; }
        public Item_Criteria Item_Criteria { get; set; }
        public Inventory_Details Inventory_Details { get; set; }
        public Purchase_Transaction_Log Purchase_Transaction_Log { get; set; }
        public Transaction_Master Transaction_Master { get; set; }
        public List<Transaction_Details> Transaction_Details { get; set; }

    }
}
