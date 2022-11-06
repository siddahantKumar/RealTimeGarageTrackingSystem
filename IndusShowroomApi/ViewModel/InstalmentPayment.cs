using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.ViewModel
{
    public class InstalmentPayment
    {
        public Transaction_Master Transaction_Master { get; set; }
        public List<Transaction_Details> Transaction_Details { get; set; }
        public Instalment_Details Instalment_Details { get; set; }
    }
}
