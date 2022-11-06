using IndusShowroomApi.Data.Services;
using IndusShowroomApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IndusShowroomApi.Controllers
{
    //[Authorize]
    [Route("api/paymentreport")]
    [ApiController]
    public class PaymentReportController : ControllerBase
    {
        private readonly IPaymentReportService paymentReportService;

        public PaymentReportController(IPaymentReportService paymentReportService)
        {
            this.paymentReportService = paymentReportService;
        }

        [HttpGet("allinstalmentaccounts/{condition?}")]
        public ActionResult<List<InstalmentAccounts>> GetInstalmentAccounts(string condition = null)
        {
            var installementAccounts = paymentReportService.GetInstalmentAccounts(condition);
            if (installementAccounts.Count > 0)
            {
                return Ok(installementAccounts);
            }
            return NoContent();
        }

        [HttpGet("instalmentpaymentreciept/{IN_ID}")]
        public ActionResult<InstalmentPaymentReciept> InstalmentPaymentReciept(int IN_ID)
        {
            var installementPaymentReciepts = paymentReportService.InstalmentPaymentReciept(IN_ID);
            if (installementPaymentReciepts != null)
            {
                return Ok(installementPaymentReciepts);
            }
            return NoContent();
        }

        //[AllowAnonymous]
        //[HttpGet("incomestatement/{dates?}")] // ? to make dates optional
        //public ActionResult<IncomeStatement> IncomeStatement(string dates = null)
        //{
        //    if (dates != null)
        //    {
        //        return Ok(paymentReportService.IncomeStatement(dates));
        //    }
        //    return Ok(paymentReportService.IncomeStatement());
        //}

        //[HttpGet("balancesheet/{dates?}")] // ? to make dates optional
        //public ActionResult<IncomeStatement> BalanceSheet(string dates = null)
        //{
        //    if (dates != null)
        //    {
        //        return Ok(paymentReportService.BalanceSheet(dates));
        //    }
        //    return Ok(paymentReportService.BalanceSheet());
        //}


        //[HttpGet("paymentvouchers/{dates?}")] // ? to make dates optional
        //public ActionResult<PaymentVouchers> PaymentVouchers(string dates = null)
        //{
        //    if (dates != null)
        //    {
        //        return Ok(paymentReportService.PaymentVouchers(dates));
        //    }
        //    return Ok(paymentReportService.PaymentVouchers());
        //}

        //[AllowAnonymous]
        //[HttpGet("trialbalance")]
        //public ActionResult<IDictionary<string, dynamic>> trialBalance()
        //{
        //    return Ok(paymentReportService.TrialBalance());
        //}

        //[HttpPost("voucher")]
        //public ActionResult InsertVoucher(List<PaymentVoucher> paymentVoucher)
        //{
        //    if (paymentReportService.InsertVoucher(paymentVoucher))
        //    {
        //        return Ok();
        //    }
        //    return StatusCode(409, paymentVoucher);
        //}

        [HttpPost("payinstalment")]
        public ActionResult PayInstalment(InstalmentPayment installementPayment)
        {
            if (paymentReportService.PayInstalment(installementPayment))
            {
                return Ok(true);
            }
            return StatusCode(409);
        }
    }
}
