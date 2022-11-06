using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.ViewModel
{
    public class InstalmentPaymentReciept
    {
        public dynamic Instalment_Details { get; set; }
        public Instalment_Master Instalment_Master { get; set; }
        public dynamic  Car_Details { get; set; }
        public string Customer { get; set; }
    }
}
