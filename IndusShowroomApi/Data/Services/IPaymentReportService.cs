using IndusShowroomApi.ViewModel;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Services
{
    public interface IPaymentReportService
    {
        bool InsertVoucher(List<PaymentVoucher> paymentVoucher);
        List<InstalmentAccounts> GetInstalmentAccounts(string condition);
        bool PayInstalment(InstalmentPayment instalmentPayment);
        InstalmentPaymentReciept InstalmentPaymentReciept(int IN_ID);
        //IncomeStatement IncomeStatement(string dates = null);
        //BalanceSheet BalanceSheet(string dates = null);

        //List<PaymentVouchers> PaymentVouchers(string dates = null);
        //IDictionary<string, dynamic> TrialBalance();
    }
}
