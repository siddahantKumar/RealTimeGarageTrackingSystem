using AutoMapper;
using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Dtos;
using IndusShowroomApi.Models;
using IndusShowroomApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;


namespace IndusShowroomApi.Data.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly DatabaseContext dbContext;
        private readonly IMapper mapper;
        private readonly IVendorRepo vendorRepo;
        private readonly IPurchaseRepo purchaseRepo;
        private readonly IPurchase_LineRepo purchase_LineRepo;
        private readonly IPurchase_Transaction_LogRepo purchase_Transaction_LogRepo;
        private readonly IInventoryRepo inventoryRepo;
        private readonly IInventory_DetailsRepo inventory_DetailsRepo;
        private readonly ITransaction_MasterRepo transaction_MasterRepo;
        private readonly ITransaction_DetailsRepo transaction_DetailsRepo;
        private readonly IProductRepo productRepo;
        private readonly IProduct_BrandRepo productBrandRepo;
        private readonly IItem_CriteriaRepo item_CriteriaRepo;

        public PurchaseService(DatabaseContext dbContext, IMapper mapper, IVendorRepo vendorRepo, IPurchaseRepo purchaseRepo, IPurchase_LineRepo purchase_LineRepo,
            IPurchase_Transaction_LogRepo purchase_Transaction_LogRepo, IInventoryRepo inventoryRepo, IInventory_DetailsRepo inventory_DetailsRepo, ITransaction_MasterRepo transaction_MasterRepo,
            ITransaction_DetailsRepo transaction_DetailsRepo, IProductRepo productRepo, IProduct_BrandRepo productBrandRepo, IItem_CriteriaRepo item_CriteriaRepo)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.vendorRepo = vendorRepo;
            this.purchaseRepo = purchaseRepo;
            this.purchase_LineRepo = purchase_LineRepo;
            this.purchase_Transaction_LogRepo = purchase_Transaction_LogRepo;
            this.inventoryRepo = inventoryRepo;
            this.inventory_DetailsRepo = inventory_DetailsRepo;
            this.transaction_MasterRepo = transaction_MasterRepo;
            this.transaction_DetailsRepo = transaction_DetailsRepo;
            this.productRepo = productRepo;
            this.productBrandRepo = productBrandRepo;
            this.item_CriteriaRepo = item_CriteriaRepo;
        }

        public List<Invoice> GetPurchaseInvoices()
        {
            List<Invoice> purchaseInvoices = new List<Invoice>();
            try
            {
                List<Vendor> Vendor = vendorRepo.GetVendors();
                List<Purchase> Purchases = purchaseRepo.GetPurchases();
                List<Purchase_Line> Purchase_Lines = purchase_LineRepo.GetPurchase_Lines();
                List<Inventory> Inventory = inventoryRepo.GetInventories();
                List<Inventory_Details> Inventory_Details = inventory_DetailsRepo.GetInventory_Details();
                List<Item_Criteria> Item_Criterias = item_CriteriaRepo.GetItem_Criterias();
                List<Transaction_Master> Transaction_Masters = transaction_MasterRepo.GetPurchaseTransaction_Master();
                List<Product> Products = productRepo.GetProducts();
                List<Product_Brand> Product_Brands = productBrandRepo.GetProduct_Brands();

                List<Purchase_Transaction_Log> PTL = purchase_Transaction_LogRepo.GetNonDuplicate_Purchase_Transaction_Logs();


                foreach (var item in PTL)
                {
                    var vendor = Vendor
                        .FirstOrDefault(x => x.VENDOR_ID == Purchases
                        .FirstOrDefault(x => x.PURCHASE_ID == Purchase_Lines
                        .FirstOrDefault(x => x.PURCHASE_LINE_ID == item.PURCHASE_LINE_ID).PURCHASE_ID).VENDOR_ID);

                    var item_Criteria = Item_Criterias
                        .FirstOrDefault(x => x.ITEM_ID == item.ITEM_ID);

                    var purchase = Purchases
                        .FirstOrDefault(x => x.PURCHASE_ID == Purchase_Lines
                        .FirstOrDefault(x => x.PURCHASE_LINE_ID == item.PURCHASE_LINE_ID).PURCHASE_ID);

                    var product = Products
                        .FirstOrDefault(x => x.PRODUCT_ID == Inventory
                        .Select(m => new { m.ITEM_ID, m.PRODUCT_ID }).Distinct()
                        .FirstOrDefault(x => x.ITEM_ID == item.ITEM_ID).PRODUCT_ID);

                    var product_Brand = Product_Brands
                        .FirstOrDefault(x => x.PRODUCT_BRAND_ID == product.PRODUCT_BRAND_ID);

                    purchaseInvoices.Add(new Invoice
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
                            Amount = purchase.P_Amount,
                            TransactionDate = purchase.TransactionDate,
                            Description = item.Description,
                            File = item.File,
                            RunningPage = item.RunningPage,
                            NumberPlate = item.NumberPlate,
                            VendName = vendor.V_Name,
                            Cnic = vendor.V_Cnic,
                            Address = vendor.V_Address,
                            PrimaryNumber = vendor.V_PrimaryPhone,
                            SecondaryNumber = vendor.V_SecondaryPhone
                        }
                    });
                }

                return purchaseInvoices;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return purchaseInvoices = null;
            }
        }


        public IDictionary<string, string> PurchaseInsert(PurchaseInsert purchaseInsert)
        {
            Dictionary<string, string>
                   purchaseSuccess = new Dictionary<string, string> { { "purchaseSuccess", "Purchase Successfull!" } },
                   purchaseFailure = new Dictionary<string, string> { { "purchaseFailure", "Purchase Failure!" } },
                   purchaseExists = new Dictionary<string, string> { { "purchaseExists", "The Car is already in the Inventory" } },
                   serverError = new Dictionary<string, string> { { "serverError", "Error!" } };

            var transaction = dbContext.Database.BeginTransaction();
            try
            {
                Vendor Vendor = purchaseInsert.Vendor;
                Purchase Purchase = purchaseInsert.Purchase;
                Inventory Inventory = purchaseInsert.Inventory;
                Item_Criteria Item_Criteria = purchaseInsert.Item_Criteria;
                Inventory_Details Inventory_Details = purchaseInsert.Inventory_Details;
                Purchase_Line Purchase_Line = new Purchase_Line();
                Purchase_Transaction_Log Purchase_Transaction_Log = purchaseInsert.Purchase_Transaction_Log;
                Transaction_Master Transaction_Master = purchaseInsert.Transaction_Master;
                List<Transaction_Details> Transaction_Details = purchaseInsert.Transaction_Details;

                int PURCHASE_ID, ITEM_ID = 0, TM_ID;
                string ChassisNumber;

                //Inserting Vendor
                vendorRepo.InsertVendor(Vendor);
                if (!vendorRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return purchaseFailure;
                }

                //Inserting Purchase
                Purchase.VENDOR_ID = vendorRepo.GetVendors().LastOrDefault().VENDOR_ID;
                purchaseRepo.InsertPurchase(Purchase);

                if (!purchaseRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return purchaseFailure;
                }

                PURCHASE_ID = purchaseRepo.GetPurchases().LastOrDefault().PURCHASE_ID;

                //Inserting Inventory
                var inventoryModel = inventoryRepo.GetInventories().LastOrDefault();
                if(inventoryModel == null)
                    Inventory.ITEM_ID = 1;
                else
                    Inventory.ITEM_ID = inventoryModel.INV_ID + 1;
                
                Inventory.CreateBy = Vendor.CreateBy;
                Inventory.Qty = 1;
                inventoryRepo.InsertInventory(Inventory);

                if (!inventoryRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return purchaseFailure;
                }

                ITEM_ID = inventoryRepo.GetInventories().LastOrDefault().INV_ID;


                //Inserting Item_Criteria
                Item_Criteria.ITEM_ID = ITEM_ID;
                item_CriteriaRepo.InsertItem_Criteria(Item_Criteria);

                if (!item_CriteriaRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return purchaseFailure;
                }

                //Checking if purchase is already in stock it will not be bought then
                foreach (var item in inventory_DetailsRepo.GetInventory_Details())
                {
                    if (item.ChassisNumber == Inventory_Details.ChassisNumber && item.InStock == true)
                    {
                        transaction.Rollback();
                        return purchaseExists;
                    }
                    else if (item.ChassisNumber == Inventory_Details.ChassisNumber && item.InStock == false)
                    {
                        //Inserting Inventory Details
                        item.ITEM_ID = ITEM_ID;
                        item.InStock = true;
                        inventory_DetailsRepo.UpdateInventory_Detail(item);

                        if (!inventory_DetailsRepo.SaveChanges())
                        {
                            transaction.Rollback();
                            return purchaseFailure;
                        }

                        goto RePurchase;
                    }
                }

                //Inserting Inventory Details
                Inventory_Details.ITEM_ID = ITEM_ID;
                Inventory_Details.InStock = true;
                inventory_DetailsRepo.InsertInventory_Detail(Inventory_Details);

                if (!inventory_DetailsRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return purchaseFailure;
                }

            RePurchase:

                ChassisNumber = Inventory_Details.ChassisNumber;

                //Inserting Purchase Line
                Purchase_Line.ChassisNumber = ChassisNumber;
                Purchase_Line.PURCHASE_ID = PURCHASE_ID;
                purchase_LineRepo.InsertPurchaseLine(Purchase_Line);

                if (!purchase_LineRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return purchaseFailure;
                }

                int PURCHASE_LINE_ID = purchase_LineRepo.GetPurchase_Lines().LastOrDefault().PURCHASE_LINE_ID;

                //Insert Purchase Transaction Log
                Purchase_Transaction_Log.PURCHASE_LINE_ID = PURCHASE_LINE_ID;
                Purchase_Transaction_Log.ChassisNumber = ChassisNumber;
                Purchase_Transaction_Log.ITEM_ID = ITEM_ID;
                purchase_Transaction_LogRepo.InsertPurchase_Transaction_Log(Purchase_Transaction_Log);

                if (!purchase_Transaction_LogRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return purchaseFailure;
                }

                ////Inserting Transaction_Master        
                Transaction_Master.OP_ID = PURCHASE_ID;
                transaction_MasterRepo.InsertTransaction_Master(Transaction_Master);

                if (!transaction_MasterRepo.SaveChanges())
                {
                    transaction.Rollback();
                    return purchaseFailure;
                }

                TM_ID = transaction_MasterRepo.GetTransaction_Masters().LastOrDefault().TM_ID;

                ////Inserting Transaction_Details
                foreach (var item in Transaction_Details)
                {
                    item.TM_ID = TM_ID;
                    transaction_DetailsRepo.InsertTransaction_Details(item);
                }

                List<Transaction_Details> transaction_Details = new List<Transaction_Details>
                {
                    new Transaction_Details { TM_ID = TM_ID, ACC_ID = 14, Debit = Purchase.P_Amount, CreateBy = Vendor.CreateBy } //   <---- 14 is inventory
                };

                foreach (var item in transaction_Details)
                {
                    transaction_DetailsRepo.InsertTransaction_Details(item);
                }

                if (transaction_DetailsRepo.SaveChanges())
                {
                    transaction.Commit();

                    return purchaseSuccess;
                }
                else
                {
                    transaction.Rollback();
                    return purchaseFailure;
                }
            }
            catch (Exception ex)
            {
                serverError["serverError"] = ex.ToString();
                transaction.Rollback();
                return serverError;
            }
        }
    }
}
