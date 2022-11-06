using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndusShowroomApi.ViewModel
{
    public class IncomeStatement
    {
        public double Revenue { get; set; }
        public double Expense { get; set; }
        public double Gross_Profit { get; set; }
        public double Net_Income { get; set; }
    }
}
