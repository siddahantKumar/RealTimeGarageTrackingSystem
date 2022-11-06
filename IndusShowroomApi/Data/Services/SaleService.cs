using AutoMapper;
using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using IndusShowroomApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IndusShowroomApi.Data.Services
{
    public class SaleService : ISaleService
    {
        private readonly DatabaseContext dbContext;
        private readonly IMapper mapper;
        private readonly ICustomerRepo customerRepo;
        private readonly ISaleRepo saleRepo;
        private readonly ISale_LineRepo sale_LineRepo;
        private readonly ISale_Transaction_LogRepo sale_Transaction_LogRepo;
        private readonly IPurchaseRepo purchaseRepo;
        private readonly IPurchase_LineRepo purchase_LineRepo;
        private readonly IPurchase_Transaction_LogRepo purchase_Transaction_LogRepo;
        private readonly IInventoryRepo inventoryRepo;
        private readonly IInventory_DetailsRepo inventory_DetailsRepo;
        private readonly IItem_CriteriaRepo item_CriteriaRepo;
        private readonly ITransaction_MasterRepo transaction_MasterRepo;
        private readonly ITransaction_DetailsRepo transaction_DetailsRepo;
        private readonly IProductRepo productRepo;
        private readonly IProduct_BrandRepo product_BrandRepo;
        private readonly IInstalment_MasterRepo instalment_MasterRepo;
        private readonly IInstalment_DetailsRepo instalment_DetailsRepo;


        public SaleService(DatabaseContext dbContext, IMapper mapper, ICustomerRepo customerRepo, ISaleRepo saleRepo, ISale_LineRepo sale_LineRepo,
            ISale_Transaction_LogRepo sale_Transaction_LogRepo, IPurchaseRepo purchaseRepo, IPurchase_LineRepo purchase_LineRepo, IPurchase_Transaction_LogRepo purchase_Transaction_LogRepo,
            IInventoryRepo inventoryRepo, IInventory_DetailsRepo inventory_DetailsRepo, IItem_CriteriaRepo item_CriteriaRepo, ITransaction_MasterRepo transaction_MasterRepo,
            ITransaction_DetailsRepo transaction_DetailsRepo, IProductRepo productRepo, IProduct_BrandRepo product_BrandRepo, IInstalment_MasterRepo instalment_MasterRepo, IInstalment_DetailsRepo instalment_DetailsRepo)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.customerRepo = customerRepo;
            this.saleRepo = saleRepo;
            this.sale_LineRepo = sale_LineRepo;
            this.sale_Transaction_LogRepo = sale_Transaction_LogRepo;
            this.purchaseRepo = purchaseRepo;
            this.purchase_LineRepo = purchase_LineRepo;
            this.purchase_Transaction_LogRepo = purchase_Transaction_LogRepo;
            this.inventoryRepo = inventoryRepo;
            this.inventory_DetailsRepo = inventory_DetailsRepo;
            this.item_CriteriaRepo = item_CriteriaRepo;
            this.instalment_DetailsRepo = instalment_DetailsRepo;
            this.transaction_MasterRepo = transaction_MasterRepo;
            this.transaction_DetailsRepo = transaction_DetailsRepo;
            this.productRepo = productRepo;
            this.product_BrandRepo = product_BrandRepo;
            this.instalment_MasterRepo = instalment_MasterRepo;
            this.instalment_DetailsRepo = instalment_DetailsRepo;
        }

        public IDictionary<string, string> InsertSale(SaleInsert saleInsert)
        {
            var transaction = dbContext.Database.BeginTransaction();

            Dictionary<string, string>
                  saleSuccess = new Dictionary<string, string> { { "saleSuccess", "Sale Successfull!" } },
                  saleFailure = new Dictionary<string, string> { { "saleFailure", "Sale Failure!" } },
                  serverError = new Dictionary<string, string> { { "serverError", "Error!" } };
            try
            {
                Customer Customer = saleInsert.Customer;
                Sale Sale = saleInsert.Sale;
                Inventory_Details Inventory_Details = saleInsert.Inventory_Details;
                Inventory Inventory = saleInsert.Inventory;
                Sale_Line Sale_Line = new Sale_Line();
                Sale_Transaction_Log Sale_Transaction_Log = saleInsert.Sale_Transaction_Log;
                Transaction_Master Transaction_Master = saleInsert.Transaction_Master;
                List<Transaction_Details> Transaction_Details = saleInsert.Transaction_Details;

                //Inserting Customer
                customerRepo.InsertCustomer(Customer);

                if (!customerRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                //Inserting Sale
                Sale.CUSTOMER_ID = customerRepo.GetCustomers().LastOrDefault().CUSTOMER_ID;
                saleRepo.InsertSale(Sale);

                if (!saleRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                int SALE_ID = saleRepo.GetSales().LastOrDefault().SALE_ID;

                //Updating Inventory_Details
                Inventory_Details inventory_Details = inventory_DetailsRepo.GetInventory_Details().FirstOrDefault(x => x.ChassisNumber == Inventory_Details.ChassisNumber);
                int ITEM_ID = inventory_Details.ITEM_ID;

                inventory_Details.InStock = false;
                inventory_DetailsRepo.UpdateInventory_Detail(inventory_Details);

                if (!inventory_DetailsRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                //Inserting Inventory
                Inventory.ITEM_ID = ITEM_ID;
                Inventory.Qty = -1;
                inventoryRepo.InsertInventory(Inventory);

                if (!inventoryRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                //Inserting Sale Line
                Sale_Line.ChassisNumber = Inventory_Details.ChassisNumber;
                Sale_Line.SALE_ID = SALE_ID;
                sale_LineRepo.InsertSale_Line(Sale_Line);

                if (!saleRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                int SALE_LINE_ID = sale_LineRepo.GetSale_Lines().LastOrDefault().SALE_LINE_ID;

                //Inserting Sale Transaction Log
                Sale_Transaction_Log.SALE_LINE_ID = SALE_LINE_ID;
                Sale_Transaction_Log.ITEM_ID = ITEM_ID;
                Sale_Transaction_Log.ChassisNumber = Inventory_Details.ChassisNumber;
                sale_Transaction_LogRepo.InsertSale_Transaction_Log(Sale_Transaction_Log);


                if (!sale_Transaction_LogRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                //Inserting Transaction_Master
                Transaction_Master.OP_ID = SALE_ID;
                Transaction_Master.Type = "S";
                transaction_MasterRepo.InsertTransaction_Master(Transaction_Master);

                if (!transaction_MasterRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                int TM_ID = transaction_MasterRepo.GetTransaction_Masters().LastOrDefault().TM_ID;

                //Inserting Transaction_Details
                double P_Amount = purchaseRepo.GetPurchases().FirstOrDefault(x => x.PURCHASE_ID == purchase_LineRepo.GetPurchase_Lines().FirstOrDefault(x => x.PURCHASE_LINE_ID == purchase_Transaction_LogRepo.GetPurchase_Transaction_Logs().FirstOrDefault(x => x.ITEM_ID == ITEM_ID).PURCHASE_LINE_ID).PURCHASE_ID).P_Amount;

                List<Transaction_Details> transaction_Details = new List<Transaction_Details>
                {
                    new Transaction_Details { TM_ID = TM_ID, ACC_ID = 16, Credit = Sale.S_Amount, CreateBy = Customer.CreateBy }, // <--- Sales
                    new Transaction_Details { TM_ID = TM_ID, ACC_ID = 18, Debit = P_Amount, CreateBy = Customer.CreateBy }, // <--- COGS
                    new Transaction_Details { TM_ID = TM_ID, ACC_ID = 14, Credit = P_Amount, CreateBy = Customer.CreateBy } , // <--- Inventory
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

                if (transaction_DetailsRepo.SaveChanges())
                {
                    transaction.Commit();
                    return saleSuccess;
                }
                else
                {
                    transaction.Rollback();
                    return saleFailure;
                }
            }
            catch (Exception ex)
            {
                serverError["serverError"] = ex.ToString();
                transaction.Rollback();
                return serverError;
            }
        }

        public IDictionary<string, string> InsertSaleInstalment(SaleInsertInstalment saleInsertInstalment)
        {
            var transaction = dbContext.Database.BeginTransaction();

            Dictionary<string, string>
                  saleSuccess = new Dictionary<string, string> { { "saleSuccess", "Sale Instalment Successfull!" } },
                  saleFailure = new Dictionary<string, string> { { "saleFailure", "Sale Instalment Failure!" } },
                  serverError = new Dictionary<string, string> { { "serverError", "Error!" } };
            try
            {
                Customer Customer = saleInsertInstalment.Customer;
                Sale Sale = saleInsertInstalment.Sale;
                Inventory_Details Inventory_Details = saleInsertInstalment.Inventory_Details;
                Inventory Inventory = saleInsertInstalment.Inventory;
                Sale_Line Sale_Line = new Sale_Line();
                Sale_Transaction_Log Sale_Transaction_Log = saleInsertInstalment.Sale_Transaction_Log;
                Transaction_Master Transaction_Master = saleInsertInstalment.Transaction_Master;
                List<Transaction_Details> Transaction_Details = saleInsertInstalment.Transaction_Details;
                Instalment_Master Instalment_Master = saleInsertInstalment.Instalment_Master;
                Instalment_Details Instalment_Details = saleInsertInstalment.Instalment_Details;

                //Inserting Customer
                customerRepo.InsertCustomer(Customer);

                if (!customerRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                //Inserting Sale
                Sale.CUSTOMER_ID = customerRepo.GetCustomers().LastOrDefault().CUSTOMER_ID;
                saleRepo.InsertSale(Sale);

                if (!saleRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                int SALE_ID = saleRepo.GetSales().LastOrDefault().SALE_ID;

                //Updating Inventory_Details
                Inventory_Details inventory_Details = inventory_DetailsRepo.GetInventory_Details().FirstOrDefault(x => x.ChassisNumber == Inventory_Details.ChassisNumber);
                int ITEM_ID = inventory_Details.ITEM_ID;

                inventory_Details.InStock = false;
                inventory_DetailsRepo.UpdateInventory_Detail(inventory_Details);

                if (!inventory_DetailsRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                //Inserting Inventory
                Inventory.ITEM_ID = ITEM_ID;
                Inventory.Qty = -1;
                inventoryRepo.InsertInventory(Inventory);

                if (!inventoryRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                //Inserting Sale Line
                Sale_Line.ChassisNumber = Inventory_Details.ChassisNumber;
                Sale_Line.SALE_ID = SALE_ID;
                sale_LineRepo.InsertSale_Line(Sale_Line);

                if (!saleRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                int SALE_LINE_ID = sale_LineRepo.GetSale_Lines().LastOrDefault().SALE_LINE_ID;

                //Inserting Sale Transaction Log
                Sale_Transaction_Log.SALE_LINE_ID = SALE_LINE_ID;
                Sale_Transaction_Log.ITEM_ID = ITEM_ID;
                Sale_Transaction_Log.ChassisNumber = Inventory_Details.ChassisNumber;
                sale_Transaction_LogRepo.InsertSale_Transaction_Log(Sale_Transaction_Log);


                if (!sale_Transaction_LogRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }


                //Inserting Instalment_Master
                Instalment_Master.CreateBy = Customer.CreateBy;
                instalment_MasterRepo.InsertInstalmentMaster(Instalment_Master);

                if (!instalment_MasterRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                int IN_ID = instalment_MasterRepo.GetInstalment_Masters().LastOrDefault().IN_ID;
                
                //Inserting Transaction_Master
                Transaction_Master.IN_ID = IN_ID;
                Transaction_Master.OP_ID = SALE_ID;
                Transaction_Master.Type = "S";
                transaction_MasterRepo.InsertTransaction_Master(Transaction_Master);

                if (!transaction_MasterRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                int TM_ID = transaction_MasterRepo.GetTransaction_Masters().LastOrDefault().TM_ID;


                //Inserting Instalment_Details
                Instalment_Details.IN_ID = IN_ID;
                Instalment_Details.TM_ID = TM_ID;
                instalment_DetailsRepo.InsertInstalmentDetail(Instalment_Details);

                if (!instalment_DetailsRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return saleFailure;
                }

                //Inserting Transaction_Details
                //First Debit cash then A/R accounts with the remaining money after that credit the sales total amount of sale and update INV & COGS;

                double P_Amount = purchaseRepo.GetPurchases().FirstOrDefault(x => x.PURCHASE_ID == purchase_LineRepo.GetPurchase_Lines().FirstOrDefault(x => x.PURCHASE_LINE_ID == purchase_Transaction_LogRepo.GetPurchase_Transaction_Logs().FirstOrDefault(x => x.ITEM_ID == ITEM_ID).PURCHASE_LINE_ID).PURCHASE_ID).P_Amount;

                List<Transaction_Details> transaction_Details = new List<Transaction_Details>
                {
                    new Transaction_Details { TM_ID = TM_ID, ACC_ID = 13, Debit = Instalment_Master.Balance, CreateBy = Customer.CreateBy }, //<----- Accounts Receivables 
                    new Transaction_Details { TM_ID = TM_ID, ACC_ID = 16, Credit = Sale.S_Amount, CreateBy = Customer.CreateBy }, // <--- Sales
                    new Transaction_Details { TM_ID = TM_ID, ACC_ID = 18, Debit = P_Amount, CreateBy = Customer.CreateBy }, // <--- COGS
                    new Transaction_Details { TM_ID = TM_ID, ACC_ID = 14, Credit = P_Amount, CreateBy = Customer.CreateBy } , // <--- Inventory
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

                if (transaction_DetailsRepo.SaveChanges())
                {
                    transaction.Commit();
                    return saleSuccess;
                }
                else
                {
                    transaction.Rollback();
                    return saleFailure;
                }
            }
            catch (Exception ex)
            {
                serverError["serverError"] = ex.ToString();
                transaction.Rollback();
                return serverError;
            }

        }

        public List<Invoice> GetSaleInvoices()
        {
            List<Invoice> saleInvoices = new List<Invoice>();
            try
            {
                List<Customer> Customers = customerRepo.GetCustomers();
                List<Sale> Sales = saleRepo.GetSales();
                List<Sale_Line> Sale_Lines = sale_LineRepo.GetSale_Lines();
                List<Inventory> Inventory = inventoryRepo.GetInventories();
                List<Inventory_Details> Inventory_Details = inventory_DetailsRepo.GetInventory_Details();
                List<Item_Criteria> Item_Criterias = item_CriteriaRepo.GetItem_Criterias();
                List<Transaction_Master> Transaction_Masters = transaction_MasterRepo.GetPurchaseTransaction_Master();
                List<Product> Products = productRepo.GetProducts();
                List<Product_Brand> Product_Brands = product_BrandRepo.GetProduct_Brands();

                List<Sale_Transaction_Log> STL = sale_Transaction_LogRepo.GetNonDuplicate_Sale_Transaction_Logs();


                foreach (var item in STL)
                {
                    var customer = Customers
                        .FirstOrDefault(x => x.CUSTOMER_ID == Sales
                        .FirstOrDefault(x => x.SALE_ID == Sale_Lines
                        .FirstOrDefault(x => x.SALE_LINE_ID == item.SALE_LINE_ID).SALE_ID).CUSTOMER_ID);

                    var item_Criteria = Item_Criterias
                        .FirstOrDefault(x => x.ITEM_ID == item.ITEM_ID);

                    var sale = Sales
                        .FirstOrDefault(x => x.SALE_ID == Sale_Lines
                        .FirstOrDefault(x => x.SALE_LINE_ID == item.SALE_LINE_ID).SALE_ID);

                    var product = Products
                        .FirstOrDefault(x => x.PRODUCT_ID == Inventory
                        .Select(m => new { m.ITEM_ID, m.PRODUCT_ID }).Distinct()
                        .FirstOrDefault(x => x.ITEM_ID == item.ITEM_ID).PRODUCT_ID);

                    var product_Brand = Product_Brands
                        .FirstOrDefault(x => x.PRODUCT_BRAND_ID == product.PRODUCT_BRAND_ID);

                    saleInvoices.Add(new Invoice
                    {
                        InvoiceReceipt = new
                        {
                            Car = product.ProductTitle,
                            Company = product_Brand.ProductBrandTitle,
                            Model = item_Criteria.Model,
                            Color = item_Criteria.Color,
                            Cc = item_Criteria.Cc,
                            RegistrationNum = item.RegistrationNum,
                            ChassisNumber = item.ChassisNumber,
                            Amount = sale.S_Amount,
                            TransactionDate = sale.TransactionDate,
                            Description = item.Description,
                            File = item.File,
                            RunningPage = item.RunningPage,
                            NumberPlate = item.NumberPlate,
                            CustName = customer.C_Name,
                            Cnic = customer.C_Cnic,
                            Address = customer.C_Address,
                            PrimaryNumber = customer.C_PrimaryPhone,
                            SecondaryNumber = customer.C_SecondaryPhone
                        }
                    });
                }

                return saleInvoices;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return saleInvoices = null;
            }
        }

        public List<StockModel> SearchedStocks(string search)
        {
            List<StockModel> stockModels = new List<StockModel>();

            List<Purchase_Transaction_Log> PTL = purchase_Transaction_LogRepo.GetNonDuplicate_Purchase_Transaction_Logs();
            List<Item_Criteria> Item_Criteria = item_CriteriaRepo.GetItem_Criterias();
            List<Product> Products = productRepo.GetProducts();
            List<Inventory> Inventories = inventoryRepo.GetInventories();
            foreach (var item in inventory_DetailsRepo.GetSearchedInventory_Details(search))
            {
                if (item.InStock)
                {
                    Purchase_Transaction_Log PTL_Stock = PTL.FirstOrDefault(x => x.ITEM_ID == item.ITEM_ID);
                    Item_Criteria item_Criteria_Stock = Item_Criteria.FirstOrDefault(x => x.ITEM_ID == item.ITEM_ID);
                    Product product_Stock = Products
                        .FirstOrDefault(x => x.PRODUCT_ID == Inventories.Select(m => new { m.ITEM_ID, m.PRODUCT_ID }).Distinct()
                        .FirstOrDefault(x => x.ITEM_ID == item.ITEM_ID).PRODUCT_ID);

                    stockModels.Add(new StockModel
                    {
                        CAR_ID = product_Stock.PRODUCT_ID,
                        ITEM_ID = item.ITEM_ID,
                        Color = item_Criteria_Stock.Color,
                        File = PTL_Stock.File,
                        RunningPage = PTL_Stock.RunningPage,
                        Tax = PTL_Stock.Tax,
                        Cplc = PTL_Stock.Cplc,
                        RegistrationNum = PTL_Stock.RegistrationNum,
                        ChassisNumber = PTL_Stock.ChassisNumber,
                        Cc = item_Criteria_Stock.Cc,
                        NumberPlate = PTL_Stock.NumberPlate,
                        Model = item_Criteria_Stock.Model,
                        MfYear = item_Criteria_Stock.MfYear,
                        BookNumber = PTL_Stock.BookNumber,
                        EngineNumber = PTL_Stock.EngineNumber,
                        Description = PTL_Stock.Description
                    });
                }
            }
            return stockModels;
        }

        public List<StockModel> GetStock()
        {
            List<StockModel> stockModels = new List<StockModel>();

            List<Purchase_Transaction_Log> PTL = purchase_Transaction_LogRepo.GetNonDuplicate_Purchase_Transaction_Logs();
            List<Item_Criteria> Item_Criteria = item_CriteriaRepo.GetItem_Criterias();
            List<Product> Products = productRepo.GetProducts();
            List<Inventory> Inventories = inventoryRepo.GetInventories();

            foreach (var item in inventory_DetailsRepo.GetInventory_Details())
            {
                if (item.InStock)
                {
                    Purchase_Transaction_Log PTL_Stock = PTL.FirstOrDefault(x => x.ITEM_ID == item.ITEM_ID);
                    Item_Criteria item_Criteria_Stock = Item_Criteria.FirstOrDefault(x => x.ITEM_ID == item.ITEM_ID);
                    Product product_Stock = Products
                        .FirstOrDefault(x => x.PRODUCT_ID == Inventories.Select(m => new { m.ITEM_ID, m.PRODUCT_ID }).Distinct()
                        .FirstOrDefault(x => x.ITEM_ID == item.ITEM_ID).PRODUCT_ID);

                    stockModels.Add(new StockModel
                    {
                        CAR_ID = product_Stock.PRODUCT_ID,
                        ITEM_ID = item.ITEM_ID,
                        Color = item_Criteria_Stock.Color,
                        File = PTL_Stock.File,
                        RunningPage = PTL_Stock.RunningPage,
                        Tax = PTL_Stock.Tax,
                        Cplc = PTL_Stock.Cplc,
                        RegistrationNum = PTL_Stock.RegistrationNum,
                        ChassisNumber = PTL_Stock.ChassisNumber,
                        Cc = item_Criteria_Stock.Cc,
                        NumberPlate = PTL_Stock.NumberPlate,
                        Model = item_Criteria_Stock.Model,
                        MfYear = item_Criteria_Stock.MfYear,
                        BookNumber = PTL_Stock.BookNumber,
                        EngineNumber = PTL_Stock.EngineNumber,
                        Description = PTL_Stock.Description
                    });
                }
            }
            return stockModels;
        }

        //public List<StockDto> GetDetailsSale()
        //{
        //    List<SP_Detail> sP_Details = new List<SP_Detail>();
        //    foreach (var item in sP_DetailRepo.GetSP_Details())
        //    {
        //        if (item.QtyOut > 0)
        //        {
        //            sP_Details.Add(item);
        //        }
        //    }
        //    var sales = mapper.Map<List<StockDto>>(sP_Details);
        //    return sales;
        //}
    }
}
