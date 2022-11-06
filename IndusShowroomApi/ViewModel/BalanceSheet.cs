using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndusShowroomApi.ViewModel
{
    public class BalanceSheet
    {
        public dynamic Assets { get; set; }
        public double Current_Asset { get; set; }
        public double Total_Assets { get; set; }
        public dynamic Liabilities { get; set; }
        public double Current_Liabilites { get; set; }
        public double Total_Liabilities { get; set; }
    }
}
