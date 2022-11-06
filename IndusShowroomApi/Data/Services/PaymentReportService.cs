using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using IndusShowroomApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace IndusShowroomApi.Data.Services
{
    public class PaymentReportService : IPaymentReportService
    {
        private readonly DatabaseContext dbContext;
        private readonly ITransaction_MasterRepo transaction_MasterRepo;
        private readonly ITransaction_DetailsRepo transaction_DetailsRepo;
        private readonly IInstalment_MasterRepo instalment_MasterRepo;
        private readonly IInstalment_DetailsRepo instalment_DetailsRepo;
        private readonly IInventoryRepo inventoryRepo;
        private readonly IInventory_DetailsRepo inventory_DetailsRepo;
        private readonly IProductRepo productRepo;
        private readonly IProduct_BrandRepo product_BrandRepo;
        private readonly IPurchase_LineRepo purchase_LineRepo;
        private readonly IPurchaseRepo purchaseRepo;
        private readonly ICustomerRepo customerRepo;
        private readonly ISaleRepo saleRepo;
        private readonly ISale_LineRepo sale_LineRepo;
        private readonly ISale_Transaction_LogRepo sale_Transaction_LogRepo;
        private readonly IItem_CriteriaRepo item_CriteriaRepo;

        public PaymentReportService(DatabaseContext dbContext, ITransaction_MasterRepo transaction_MasterRepo, ITransaction_DetailsRepo transaction_DetailsRepo, IInstalment_MasterRepo instalment_MasterRepo,
            IInstalment_DetailsRepo instalment_DetailsRepo, IInventoryRepo inventoryRepo, IInventory_DetailsRepo inventory_DetailsRepo, IProductRepo productRepo, IProduct_BrandRepo product_BrandRepo,
            IPurchaseRepo purchaseRepo, IPurchase_LineRepo purchase_LineRepo, ICustomerRepo customerRepo, ISaleRepo saleRepo, ISale_LineRepo sale_LineRepo, ISale_Transaction_LogRepo sale_Transaction_LogRepo,
            IItem_CriteriaRepo item_CriteriaRepo)
        {
            this.dbContext = dbContext;
            this.transaction_MasterRepo = transaction_MasterRepo;
            this.transaction_DetailsRepo = transaction_DetailsRepo;
            this.instalment_MasterRepo = instalment_MasterRepo;
            this.instalment_DetailsRepo = instalment_DetailsRepo;
            this.inventoryRepo = inventoryRepo;
            this.inventory_DetailsRepo = inventory_DetailsRepo;
            this.productRepo = productRepo;
            this.product_BrandRepo = product_BrandRepo;
            this.purchase_LineRepo = purchase_LineRepo;
            this.purchaseRepo = purchaseRepo;
            this.customerRepo = customerRepo;
            this.saleRepo = saleRepo;
            this.sale_LineRepo = sale_LineRepo;
            this.sale_Transaction_LogRepo = sale_Transaction_LogRepo;
            this.item_CriteriaRepo = item_CriteriaRepo;
        }

        public bool InsertVoucher(List<PaymentVoucher> paymentVoucher)
        {
            Transaction_Master transaction_Master = new Transaction_Master();
            List<Transaction_Details> transaction_Details = new List<Transaction_Details>();

            foreach (var paymentvoucher in paymentVoucher)
            {
                transaction_Master = paymentvoucher.transaction_Master;
                transaction_Details = paymentvoucher.transaction_Details;

                //Inserting Transaction_Master        
                string TM_ID = "TM_" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ffffff tt");
                //transaction_Master.TK_ID = "TK_" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                //transaction_Master.TM_ID = TM_ID;
                transaction_MasterRepo.InsertTransaction_Master(transaction_Master);

                //Inserting Transaction_Details
                foreach (var transaction_detail in transaction_Details)
                {
                    //transaction_detail.TM_ID = TM_ID;
                    transaction_DetailsRepo.InsertTransaction_Details(transaction_detail);
                }
            }

            if (transaction_MasterRepo.SaveChanges() && transaction_DetailsRepo.SaveChanges())
            {
                return true;
            }

            return false;

        }

        public List<InstalmentAccounts> GetInstalmentAccounts(string condition)
        {
            List<InstalmentAccounts> InstalmentAccounts = new List<InstalmentAccounts>();

            List<Instalment_Master> Instalment_Masters = instalment_MasterRepo.GetInstalment_Masters();
            List<Instalment_Details> Instalment_Details = condition == null ? instalment_DetailsRepo.GetInstalment_Details() : instalment_DetailsRepo.GetNonDuplicateInstalment_Details().ToList();

            List<Transaction_Master> Transaction_Masters = transaction_MasterRepo.GetTransaction_Masters().OrderBy(x => x.IN_ID).ToList(); List<Customer> Customers = customerRepo.GetCustomers();
            List<Sale> Sales = saleRepo.GetSales();
            List<Sale_Line> Sale_Lines = sale_LineRepo.GetSale_Lines().OrderBy(x => x.SALE_ID).ToList();
            List<Inventory_Details> Inventory_Details = inventory_DetailsRepo.GetInventory_Details().OrderBy(x => x.ChassisNumber).ToList();
            List<Inventory> Invetories = inventoryRepo.GetInventories().OrderBy(x => x.ITEM_ID).ToList();
            List<Product> Products = productRepo.GetProducts();



            foreach (var item in Instalment_Details)
            {
                if (item.IsActive == (condition == null ? true : false))
                {
                    string car = Products
                        .FirstOrDefault(x => x.PRODUCT_ID == Invetories
                        .FirstOrDefault(x => x.ITEM_ID == Inventory_Details
                        .FirstOrDefault(x => x.ChassisNumber == Sale_Lines
                        .FirstOrDefault(x => x.SALE_ID == Sales
                        .FirstOrDefault(x => x.SALE_ID == Transaction_Masters
                        .FirstOrDefault(x => x.IN_ID == item.IN_ID).OP_ID).SALE_ID).ChassisNumber).ITEM_ID).PRODUCT_ID).ProductTitle;

                    string customer = Customers.FirstOrDefault(x => x.CUSTOMER_ID == Sales
                        .FirstOrDefault(x => x.SALE_ID == Transaction_Masters
                        .FirstOrDefault(x => x.IN_ID == item.IN_ID).OP_ID).CUSTOMER_ID).C_Name;

              
                        InstalmentAccounts.Add(new InstalmentAccounts
                        {
                            Instalment_Master = Instalment_Masters.FirstOrDefault(x => x.IN_ID == item.IN_ID),
                            Instalment_Details = item,
                            Car = car,
                            Customer = customer
                        });
                
                   
                }
            }

            return InstalmentAccounts;
        }

        public bool PayInstalment(InstalmentPayment instalmentPayment)
        {
            var transaction = dbContext.Database.BeginTransaction();
            try
            {
                Transaction_Master Transaction_Master = instalmentPayment.Transaction_Master;
                List<Transaction_Details> Transaction_Details = instalmentPayment.Transaction_Details;
                Instalment_Details Instalment_Detail = instalmentPayment.Instalment_Details;

                //Updating instalment details active fields
                foreach (var item in instalment_DetailsRepo.GetInstalment_Details())
                {
                    if (item.IN_ID == Instalment_Detail.IN_ID && item.IsActive)
                    {
                        item.IsActive = false;
                        instalment_DetailsRepo.UpdateInstalment_Detail(item);

                        if (!instalment_DetailsRepo.SaveChanges())
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }

                //Updating instalment master recieved and remaining fields
                int IN_ID = Instalment_Detail.IN_ID;

                Instalment_Master instalment_Master = instalment_MasterRepo.GetInstalment_Master(IN_ID);
                instalment_Master.Paid += Transaction_Master.Amount;
                instalment_Master.Balance -= Transaction_Master.Amount;
                instalment_Master.TransactionDate = Transaction_Master.TransactionDate;

                instalment_MasterRepo.UpdateInstalmentMaster(instalment_Master);
                if (!instalment_MasterRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return false;
                }

                //Inserting new transaction master
                Transaction_Master.OP_ID = transaction_MasterRepo.GetTransaction_MasterByIN_ID(IN_ID).OP_ID;
                Transaction_Master.IN_ID = IN_ID;
                Transaction_Master.Type = "INS";
                transaction_MasterRepo.InsertTransaction_Master(Transaction_Master);

                if (!transaction_MasterRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return false;
                }

                int TM_ID = transaction_MasterRepo.GetTransaction_Masters().LastOrDefault().TM_ID;

                //Inserting new Transaction details
                List<Transaction_Details> transaction_Details = new List<Transaction_Details>
                {
                    new Transaction_Details { TM_ID = TM_ID, ACC_ID = 13, Credit = Transaction_Master.Amount, CreateBy = Transaction_Master.CreateBy }, // <--- Sales
                };

                foreach (var item in Transaction_Details)
                {
                    item.TM_ID = TM_ID;
                    transaction_DetailsRepo.InsertTransaction_Details(item);
                }

                foreach (var item in transaction_Details)
                {
                    transaction_DetailsRepo.InsertTransaction_Details(item);
                }

                if (!transaction_DetailsRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return false;
                }

                //Inserting new instalment detail
                if (instalment_Master.Balance == 0)
                {
                    Instalment_Detail.IsActive = false;
                }
                Instalment_Detail.IN_ID = IN_ID;
                Instalment_Detail.TM_ID = TM_ID;
                instalment_DetailsRepo.InsertInstalmentDetail(Instalment_Detail);

                if (!instalment_DetailsRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return false;
                }

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public InstalmentPaymentReciept InstalmentPaymentReciept(int IN_ID)
        {
            Instalment_Master Instalment_Master = instalment_MasterRepo.GetInstalment_Masters().FirstOrDefault(x => x.IN_ID == IN_ID);
            List<Instalment_Master> Instalment_Masters = instalment_MasterRepo.GetInstalment_Masters();
            List<Instalment_Details> Instalment_Details = instalment_DetailsRepo.GetInstalment_Details();

            List<Transaction_Master> Transaction_Masters = transaction_MasterRepo.GetTransaction_Masters().OrderBy(x => x.IN_ID).ToList();
            List<Customer> Customers = customerRepo.GetCustomers();
            List<Sale> Sales = saleRepo.GetSales();
            List<Sale_Line> Sale_Lines = sale_LineRepo.GetSale_Lines().OrderBy(x => x.SALE_ID).ToList();
            List<Inventory_Details> Inventory_Details = inventory_DetailsRepo.GetInventory_Details().OrderBy(x => x.ChassisNumber).ToList();
            List<Inventory> Invetories = inventoryRepo.GetInventories().OrderBy(x => x.ITEM_ID).ToList();
            List<Product> Products = productRepo.GetProducts();
            List<Item_Criteria> Item_Criterias = item_CriteriaRepo.GetItem_Criterias();
            List<Sale_Transaction_Log> Sale_Transaction_Logs = sale_Transaction_LogRepo.GetNonDuplicate_Sale_Transaction_Logs();

            List<Instalment_Details> instalment_Details = new List<Instalment_Details>();


            Product product = Products
                .FirstOrDefault(x => x.PRODUCT_ID == Invetories
                .FirstOrDefault(x => x.ITEM_ID == Inventory_Details
                .FirstOrDefault(x => x.ChassisNumber == Sale_Lines
                .FirstOrDefault(x => x.SALE_ID == Sales
                .FirstOrDefault(x => x.SALE_ID == Transaction_Masters
                .FirstOrDefault(x => x.IN_ID == IN_ID).OP_ID).SALE_ID).ChassisNumber).ITEM_ID).PRODUCT_ID);

            Inventory_Details inventory_Details = Inventory_Details
                .FirstOrDefault(x => x.ChassisNumber == Sale_Lines
                .FirstOrDefault(x => x.SALE_ID == Sales
                .FirstOrDefault(x => x.SALE_ID == Transaction_Masters
                .FirstOrDefault(x => x.IN_ID == IN_ID).OP_ID).SALE_ID).ChassisNumber);

            Sale_Transaction_Log sale_Transaction_Log = Sale_Transaction_Logs
                .FirstOrDefault(x => x.SALE_LINE_ID == Sale_Lines
                .FirstOrDefault(x => x.SALE_ID == Sales
                .FirstOrDefault(x => x.SALE_ID == Transaction_Masters
                .FirstOrDefault(x => x.IN_ID == IN_ID).OP_ID).SALE_ID).SALE_LINE_ID);


            Item_Criteria item_Criteria = Item_Criterias
                .FirstOrDefault(x => x.ITEM_ID == Inventory_Details
                .FirstOrDefault(x => x.ChassisNumber == Sale_Lines
                .FirstOrDefault(x => x.SALE_ID == Sales
                .FirstOrDefault(x => x.SALE_ID == Transaction_Masters
                .FirstOrDefault(x => x.IN_ID == IN_ID).OP_ID).SALE_ID).ChassisNumber).ITEM_ID);

            Customer customer = Customers
                .FirstOrDefault(x => x.CUSTOMER_ID == Sales
                .FirstOrDefault(x => x.SALE_ID == Transaction_Masters
                .FirstOrDefault(x => x.IN_ID == IN_ID).OP_ID).CUSTOMER_ID);

            var Car_Details = new
            {
                CarTitle = product.ProductTitle,
                Maker = product_BrandRepo.GetProduct_Brands().FirstOrDefault(x => x.PRODUCT_BRAND_ID == product.PRODUCT_BRAND_ID).ProductBrandTitle,
                Model = item_Criteria.Model,
                cc = item_Criteria.Cc,
                RegNum = sale_Transaction_Log.RegistrationNum,
                ChassisNum = inventory_Details.ChassisNumber
            };

            foreach (var item_detail in instalment_DetailsRepo.GetInstalment_Details())
            {
                if (item_detail.IN_ID == IN_ID)
                {
                    instalment_Details.Add(item_detail);
                }
            }

            List<dynamic> list = new List<dynamic>();

            foreach (var itemDetail in instalment_Details)
            {
                List<Transaction_Details> transactional_Details = new List<Transaction_Details>();
                foreach (var item in transaction_DetailsRepo.GetTransactionDetailByTM_ID(itemDetail.TM_ID))
                {
                    if (item.ACC_ID == 11 || item.ACC_ID == 12)
                    {
                        transactional_Details.Add(item);
                    }
                }
                list.Add(new { Instalment_Detail = itemDetail, transactional_Details = transactional_Details });
            }

            return (new InstalmentPaymentReciept
            {
                Instalment_Master = Instalment_Master,
                Instalment_Details = list,
                Car_Details = Car_Details,
                Customer = customer.C_Name
            });
        }

        public IDictionary<string, dynamic> TrialBalance()
        {
            //List<AccountsThree> accountsThrees = accountsThreeRepo.GetAccountsThrees();
            //List<AccountsFour> accountsFours = accountsFourRepo.GetAccountsFours();

            List<Transaction_Details> transaction_Details = transaction_DetailsRepo.GetTransaction_Details();
            Dictionary<string, dynamic> trialBalance = new Dictionary<string, dynamic>();

            double Sum = 0;

            //foreach (var accThreeItem in accountsThrees)
            //{
            //    if (accThreeItem.IsDefault)
            //    {
            //        if (accThreeItem.OpeningBalance > 0)
            //        {
            //            Sum += accThreeItem.OpeningBalance;
            //        }

            //        foreach (var tranItem in transaction_Details)
            //        {
            //            if (accThreeItem.ACC_THREE_ID == tranItem.ACC_THREE_ID)
            //            {
            //                Sum += (tranItem.Debit - tranItem.Credit);
            //            }

            //        }

            //        for (int i = 0; i < accountsFours.Count; i++)
            //        {
            //            if (accountsFours[i].ACC_THREE_ID == accThreeItem.ACC_THREE_ID)
            //            {
            //                foreach (var tranItem in transaction_Details)
            //                {
            //                    if (tranItem.ACC_THREE_ID == accountsFours[i].ACC_THREE_ID)
            //                    {
            //                        Sum += (tranItem.Debit - tranItem.Credit);
            //                    }

            //                }

            //                if (trialBalance.ContainsKey(accThreeItem.AccountThreeTitle))
            //                {
            //                    trialBalance[accThreeItem.AccountThreeTitle].Add(accountsFours[i].AccountFourTitle, Sum);
            //                    Sum = 0;
            //                }
            //                else
            //                {
            //                    trialBalance.Add(accThreeItem.AccountThreeTitle, new Dictionary<string, double>() { { accountsFours[i].AccountFourTitle, Sum } });
            //                    Sum = 0;
            //                }


            //            }
            //        }

            //        if (!trialBalance.ContainsKey(accThreeItem.AccountThreeTitle))
            //        {
            //            trialBalance.Add(accThreeItem.AccountThreeTitle, Sum);
            //            Sum = 0;
            //        }
            //    }
            //}

            return trialBalance;
        }



        //public IncomeStatement IncomeStatement(string dates = null)
        //{
        //    double Product_Sales = 0, Discount_To_Customer = 0, Operating_Revenue = 0, Revenue = 0;
        //    double Product_Cost = 0, Salaries = 0, Daily_Expense = 0, Cost_Of_Goods_Sold = 0, Administrative_Expenses = 0, Operating_Expense = 0, Expense = 0;
        //    double Gross_Profit = 0, NetIncome = 0;

        //    IDictionary<string, dynamic> trialBalance = TrialBalance();


        //    //foreach (var accOneItem in accountsOnes)
        //    //{
        //    //    foreach (var accTwoItem in accountsTwos)
        //    //    {
        //    //        if (accOneItem.ACC_ONE_ID == accTwoItem.ACC_ONE_ID)
        //    //        {
        //    //            foreach (var accThreeItem in accountsThrees)
        //    //            {
        //    //                if (accTwoItem.ACC_TWO_ID == accThreeItem.ACC_TWO_ID && accThreeItem.IsDefault)
        //    //                {
        //    //                    if (accOneItem.ACC_ONE_ID == 4)
        //    //                    {
        //    //                        Revenue += trialBalance[accThreeItem.AccountThreeTitle];
        //    //                    }
        //    //                    if (accOneItem.ACC_ONE_ID == 5 && accTwoItem.ACC_ONE_ID == 5 && accThreeItem.ACC_TWO_ID == 11)
        //    //                    {
        //    //                        Cost_Of_Goods_Sold += trialBalance[accThreeItem.AccountThreeTitle];
        //    //                    }
        //    //                    if (accOneItem.ACC_ONE_ID == 5 && accTwoItem.ACC_ONE_ID == 5 && accThreeItem.ACC_TWO_ID == 12)
        //    //                    {
        //    //                        Administrative_Expenses += trialBalance[accThreeItem.AccountThreeTitle];
        //    //                    }
        //    //                }
        //    //            }
        //    //        }
        //    //    }

        //    //}



        //    //if (dates != null)
        //    //{
        //    //    DateTime fromDate = Convert.ToDateTime(dates.Split("_")[0]).Date, toDate = Convert.ToDateTime(dates.Split("_")[1]).Date;
        //    //    foreach (var item in transaction_DetailsRepo.GetTransaction_Details())
        //    //    {
        //    //        DateTime dateCheck = Convert.ToDateTime(item.CreateDate.ToString().Split(" ")[0]).Date;
        //    //        if (dateCheck >= fromDate && dateCheck <= toDate)
        //    //        {
        //    //            if (item.ACC_THREE_ID == 7)
        //    //            {
        //    //                Product_Sales += item.Credit;
        //    //            }
        //    //            else if (item.ACC_THREE_ID == 8)
        //    //            {
        //    //                Discount_To_Customer += item.Credit;
        //    //            }
        //    //            if (item.ACC_FOUR_ID == 1)
        //    //            {
        //    //                Product_Cost += (item.Debit - item.Credit);
        //    //            }
        //    //            else if (item.ACC_FOUR_ID == 2)
        //    //            {
        //    //                Salaries += (item.Debit - item.Credit);
        //    //            }
        //    //            else if (item.ACC_FOUR_ID == 3)
        //    //            {
        //    //                Daily_Expense += (item.Debit - item.Credit);
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    DateTime today = DateTime.Now.Date;
        //    //    foreach (var item in transaction_DetailsRepo.GetTransaction_Details())
        //    //    {
        //    //        DateTime dateCheck = Convert.ToDateTime(item.CreateDate.ToString().Split(" ")[0]).Date;
        //    //        if (dateCheck == today)
        //    //        {
        //    //            var split = item.CreateDate.ToString().Split(" ")[0];
        //    //            if (item.ACC_THREE_ID == 7)
        //    //            {
        //    //                Product_Sales += item.Credit;
        //    //            }
        //    //            else if (item.ACC_THREE_ID == 8)
        //    //            {
        //    //                Discount_To_Customer += item.Credit;
        //    //            }
        //    //            if (item.ACC_FOUR_ID == 1)
        //    //            {
        //    //                Product_Cost += (item.Debit - item.Credit);
        //    //            }
        //    //            else if (item.ACC_FOUR_ID == 2)
        //    //            {
        //    //                Salaries += (item.Debit - item.Credit);
        //    //            }
        //    //            else if (item.ACC_FOUR_ID == 3)
        //    //            {
        //    //                Daily_Expense += (item.Debit - item.Credit);
        //    //            }
        //    //        }
        //    //    }
        //    //}



        //    //Calculating GrossProfit
        //    Gross_Profit += Revenue - Cost_Of_Goods_Sold;

        //    //Calculating Net Income
        //    NetIncome += Gross_Profit - Administrative_Expenses;

        //    return new IncomeStatement
        //    {
        //        Revenue = Revenue,
        //        Expense = Expense,
        //        Gross_Profit = Gross_Profit,
        //        Net_Income = Revenue - Administrative_Expenses,
        //    };
        //}

        //public BalanceSheet BalanceSheet(string dates = null)
        //{
        //    double Cash = 0, Bank = 0, Accounts_Receivables = 0, Inventory = 0, Current_Asset = 0, Assets = 0;
        //    double Accounts_Payables = 0, Current_Liabilites = 0, Liabilites = 0;


        //    //if (dates != null)
        //    //{
        //    //    DateTime fromDate = Convert.ToDateTime(dates.Split("_")[0]).Date, toDate = Convert.ToDateTime(dates.Split("_")[1]).Date;
        //    //    foreach (var item in transaction_DetailsRepo.GetTransaction_Details())
        //    //    {
        //    //        DateTime dateCheck = Convert.ToDateTime(item.CreateDate.ToString().Split(" ")[0]).Date;
        //    //        if (dateCheck >= fromDate && dateCheck <= toDate)
        //    //        {
        //    //            if (item.ACC_THREE_ID == 1)
        //    //            {
        //    //                Cash += (item.Debit - item.Credit);
        //    //            }
        //    //            else if (item.ACC_THREE_ID == 2)
        //    //            {
        //    //                Bank += (item.Debit - item.Credit);
        //    //            }
        //    //            else if (item.ACC_THREE_ID == 4)
        //    //            {
        //    //                Inventory += (item.Debit - item.Credit);
        //    //            }
        //    //            else if (item.ACC_THREE_ID == 3)
        //    //            {
        //    //                Accounts_Receivables += (item.Debit - item.Credit);
        //    //            }

        //    //        }
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    //DateTime today = DateTime.Now.Date;
        //    //    foreach (var item in transaction_DetailsRepo.GetTransaction_Details())
        //    //    {
        //    //        //DateTime dateCheck = Convert.ToDateTime(item.CreateDate.ToString().Split(" ")[0]).Date;
        //    //        //if (dateCheck == today)
        //    //        //{
        //    //        if (item.ACC_THREE_ID == 1)
        //    //        {
        //    //            Cash += (item.Debit - item.Credit);
        //    //        }
        //    //        else if (item.ACC_THREE_ID == 2)
        //    //        {
        //    //            Bank += (item.Debit - item.Credit);
        //    //        }
        //    //        else if (item.ACC_THREE_ID == 4)
        //    //        {
        //    //            Inventory += (item.Debit - item.Credit);
        //    //        }
        //    //        else if (item.ACC_THREE_ID == 3)
        //    //        {
        //    //            Accounts_Receivables += (item.Debit - item.Credit);
        //    //        }

        //    //        //}
        //    //    }
        //    //}


        //    //Calculating Current Assets and Assets
        //    Assets = Current_Asset = Cash + Bank + Inventory + Accounts_Receivables;

        //    //Calculating Liabilites and Current Liabilities
        //    Liabilites = Current_Liabilites = Accounts_Payables;

        //    //Retained Earnings is my net income

        //    return new BalanceSheet
        //    {
        //        Assets = new
        //        {
        //            Cash,
        //            Bank,
        //            Inventory,
        //            Accounts_Receivables
        //        },
        //        Current_Asset = Current_Asset,
        //        Total_Assets = Assets,
        //        Liabilities = new
        //        {
        //            Accounts_Payables
        //        },
        //        Current_Liabilites = Current_Liabilites,
        //        Total_Liabilities = Liabilites

        //    };
        //}

        //public List<PaymentVouchers> PaymentVouchers(string dates = null)
        //{
        //    List<Transaction_Master> transaction_Masters = transaction_MasterRepo.GetTransaction_Masters();
        //    List<Transaction_Details> transaction_Details = transaction_DetailsRepo.GetTransaction_Details();


        //    List<PaymentVouchers> selectedTransaction_Details = new List<PaymentVouchers>();
        //    int count = 0;


        //    //if (dates != null)
        //    //{

        //    //    DateTime fromDate = Convert.ToDateTime(dates.Split("_")[0]).Date, toDate = Convert.ToDateTime(dates.Split("_")[1]).Date;
        //    //    for (int i = 0; i < transaction_Masters.Count; i++)
        //    //    {
        //    //        DateTime dateCheck = Convert.ToDateTime(transaction_Masters[i].CreateDate.ToString().Split(" ")[0]).Date;
        //    //        if (transaction_Masters[i].Type == "PV" && (dateCheck >= fromDate && dateCheck <= toDate))
        //    //        {
        //    //            for (int j = 0; j < transaction_Details.Count; j++)
        //    //            {
        //    //                if (transaction_Masters[i].TM_ID == transaction_Details[j].TM_ID && transaction_Details[j].Debit > 0)
        //    //                {
        //    //                    selectedTransaction_Details.Add(new PaymentVouchers
        //    //                    {
        //    //                        ToAccount = transaction_Details[j].ACC_THREE_ID > 0 ? accountsThrees.Find(x => x.ACC_THREE_ID == transaction_Details[j].ACC_THREE_ID).AccountThreeTitle : accountsFours.Find(x => x.ACC_FOUR_ID == transaction_Details[j].ACC_FOUR_ID).AccountFourTitle,
        //    //                        Amount = transaction_Details[j].Debit,
        //    //                        Narration = transaction_Details[j].Narration

        //    //                    });

        //    //                }
        //    //                if (transaction_Masters[i].TM_ID == transaction_Details[j].TM_ID && transaction_Details[j].Credit > 0)
        //    //                {
        //    //                    selectedTransaction_Details[count].FromAccount = transaction_Details[j].ACC_THREE_ID > 0 ? accountsThrees.Find(x => x.ACC_THREE_ID == transaction_Details[j].ACC_THREE_ID).AccountThreeTitle : accountsFours.Find(x => x.ACC_FOUR_ID == transaction_Details[j].ACC_FOUR_ID).AccountFourTitle;
        //    //                    count++;
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //else
        //    //{

        //    //    DateTime today = DateTime.Now.Date;
        //    //    for (int i = 0; i < transaction_Masters.Count; i++)
        //    //    {
        //    //        DateTime dateCheck = Convert.ToDateTime(transaction_Masters[i].CreateDate.ToString().Split(" ")[0]).Date;
        //    //        if (transaction_Masters[i].Type == "PV" && today == dateCheck)
        //    //        {
        //    //            for (int j = 0; j < transaction_Details.Count; j++)
        //    //            {
        //    //                if (transaction_Masters[i].TM_ID == transaction_Details[j].TM_ID && transaction_Details[j].Debit > 0)
        //    //                {
        //    //                    selectedTransaction_Details.Add(new PaymentVouchers
        //    //                    {
        //    //                        ToAccount = transaction_Details[j].ACC_THREE_ID > 0 ? accountsThrees.Find(x => x.ACC_THREE_ID == transaction_Details[j].ACC_THREE_ID).AccountThreeTitle : accountsFours.Find(x => x.ACC_FOUR_ID == transaction_Details[j].ACC_FOUR_ID).AccountFourTitle,
        //    //                        Amount = transaction_Details[j].Debit,
        //    //                        Narration = transaction_Details[j].Narration

        //    //                    });

        //    //                }
        //    //                if (transaction_Masters[i].TM_ID == transaction_Details[j].TM_ID && transaction_Details[j].Credit > 0)
        //    //                {
        //    //                    selectedTransaction_Details[count].FromAccount = transaction_Details[j].ACC_THREE_ID > 0 ? accountsThrees.Find(x => x.ACC_THREE_ID == transaction_Details[j].ACC_THREE_ID).AccountThreeTitle : accountsFours.Find(x => x.ACC_FOUR_ID == transaction_Details[j].ACC_FOUR_ID).AccountFourTitle;
        //    //                    count++;
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}


        //    return selectedTransaction_Details;
        //}
    }

}
