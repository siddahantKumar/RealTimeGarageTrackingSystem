using IndusShowroomApi.Models;

namespace IndusShowroomApi.ViewModel
{
    public class InstalmentAccounts
    {
        public Instalment_Master Instalment_Master { get; set; }
        public Instalment_Details Instalment_Details { get; set; }
        public string Customer { get; set; }
        public string Car { get; set; }
    }
}
