using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndusShowroomApi.ViewModel
{
    public class PaymentVouchers
    {
        public string ToAccount { get; set; }
        public string FromAccount { get; set; }
        public double Amount { get; set; }
        public string Narration { get; set; }
    }
}
