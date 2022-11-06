using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.ViewModel
{
    public class PaymentVoucher
    {
        public Transaction_Master transaction_Master { get; set; }
        public List<Transaction_Details> transaction_Details { get; set; }
    }
}
