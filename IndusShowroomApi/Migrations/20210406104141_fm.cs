using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IndusShowroomApi.Migrations
{
    public partial class fm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charts_Of_Accounts",
                columns: table => new
                {
                    ACC_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    P_ACC_ID = table.Column<int>(nullable: false),
                    Account_Title = table.Column<string>(nullable: true),
                    CreateBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charts_Of_Accounts", x => x.ACC_ID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CUSTOMER_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    C_Name = table.Column<string>(maxLength: 100, nullable: true),
                    C_Cnic = table.Column<string>(maxLength: 50, nullable: true),
                    C_Address = table.Column<string>(maxLength: 200, nullable: true),
                    C_PrimaryPhone = table.Column<string>(maxLength: 20, nullable: true),
                    C_SecondaryPhone = table.Column<string>(maxLength: 20, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CUSTOMER_ID);
                });

            migrationBuilder.CreateTable(
                name: "Instalment_Details",
                columns: table => new
                {
                    IND_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IN_ID = table.Column<string>(nullable: true),
                    TM_ID = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Narration = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    IsDue = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instalment_Details", x => x.IND_ID);
                });

            migrationBuilder.CreateTable(
                name: "Instalment_Master",
                columns: table => new
                {
                    IN_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ACC_ID = table.Column<int>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    Paid = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instalment_Master", x => x.IN_ID);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    INV_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ITEM_ID = table.Column<int>(nullable: false),
                    PRODUCT_ID = table.Column<int>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.INV_ID);
                });

            migrationBuilder.CreateTable(
                name: "Inventory_Details",
                columns: table => new
                {
                    INVD_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ITEM_ID = table.Column<int>(nullable: false),
                    InStock = table.Column<bool>(nullable: false),
                    ChassisNumber = table.Column<string>(maxLength: 100, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory_Details", x => x.INVD_ID);
                });

            migrationBuilder.CreateTable(
                name: "Item_Criteria",
                columns: table => new
                {
                    ITEM_CRIT_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ITEM_ID = table.Column<int>(nullable: false),
                    Color = table.Column<string>(maxLength: 50, nullable: true),
                    Model = table.Column<string>(maxLength: 50, nullable: true),
                    MfYear = table.Column<DateTime>(nullable: false),
                    Cc = table.Column<float>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Criteria", x => x.ITEM_CRIT_ID);
                });

            migrationBuilder.CreateTable(
                name: "Page_Routes",
                columns: table => new
                {
                    ROUTES_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    USER_TYPE_ID = table.Column<int>(nullable: true),
                    routeName = table.Column<string>(maxLength: 100, nullable: true),
                    routeValue = table.Column<string>(maxLength: 100, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page_Routes", x => x.ROUTES_ID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    PRODUCT_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PRODUCT_BRAND_ID = table.Column<int>(nullable: false),
                    PRODUCT_CAT_ID = table.Column<int>(nullable: false),
                    ProductTitle = table.Column<string>(maxLength: 100, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.PRODUCT_ID);
                });

            migrationBuilder.CreateTable(
                name: "Product_Brand",
                columns: table => new
                {
                    PRODUCT_BRAND_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductBrandTitle = table.Column<string>(maxLength: 100, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Brand", x => x.PRODUCT_BRAND_ID);
                });

            migrationBuilder.CreateTable(
                name: "Product_Category",
                columns: table => new
                {
                    PRODUCT_CAT_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductCategoryTitle = table.Column<string>(maxLength: 100, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Category", x => x.PRODUCT_CAT_ID);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    PURCHASE_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VENDOR_ID = table.Column<int>(nullable: false),
                    P_Amount = table.Column<double>(nullable: false),
                    P_Discount = table.Column<double>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.PURCHASE_ID);
                });

            migrationBuilder.CreateTable(
                name: "Purchase_Line",
                columns: table => new
                {
                    PURCHASE_LINE_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PURCHASE_ID = table.Column<int>(nullable: false),
                    ChassisNumber = table.Column<string>(maxLength: 50, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_Line", x => x.PURCHASE_LINE_ID);
                });

            migrationBuilder.CreateTable(
                name: "Purchase_Transaction_Log",
                columns: table => new
                {
                    PTL_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PURCHASE_LINE_ID = table.Column<int>(nullable: false),
                    ITEM_ID = table.Column<int>(nullable: false),
                    ChassisNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Tax = table.Column<DateTime>(nullable: false),
                    Cplc = table.Column<bool>(nullable: true),
                    RegistrationNum = table.Column<string>(maxLength: 100, nullable: true),
                    BookNumber = table.Column<string>(maxLength: 100, nullable: true),
                    EngineNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Keys = table.Column<int>(nullable: false),
                    File = table.Column<string>(maxLength: 100, nullable: true),
                    RunningPage = table.Column<string>(maxLength: 100, nullable: true),
                    NumberPlate = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_Transaction_Log", x => x.PTL_ID);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SALE_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CUSTOMER_ID = table.Column<int>(nullable: false),
                    S_Amount = table.Column<double>(nullable: false),
                    S_Discount = table.Column<double>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SALE_ID);
                });

            migrationBuilder.CreateTable(
                name: "Sale_Line",
                columns: table => new
                {
                    SALE_LINE_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SALE_ID = table.Column<int>(nullable: false),
                    ChassisNumber = table.Column<string>(maxLength: 50, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale_Line", x => x.SALE_LINE_ID);
                });

            migrationBuilder.CreateTable(
                name: "Sale_Transaction_Log",
                columns: table => new
                {
                    STL_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SALE_LINE_ID = table.Column<int>(nullable: false),
                    ITEM_ID = table.Column<int>(nullable: false),
                    ChassisNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Tax = table.Column<DateTime>(nullable: false),
                    Cplc = table.Column<bool>(nullable: true),
                    RegistrationNum = table.Column<string>(maxLength: 100, nullable: true),
                    BookNumber = table.Column<string>(maxLength: 100, nullable: true),
                    EngineNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Keys = table.Column<int>(nullable: false),
                    File = table.Column<string>(maxLength: 100, nullable: true),
                    RunningPage = table.Column<string>(maxLength: 100, nullable: true),
                    NumberPlate = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale_Transaction_Log", x => x.STL_ID);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_Details",
                columns: table => new
                {
                    TD_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TM_ID = table.Column<int>(nullable: false),
                    ACC_ID = table.Column<int>(nullable: false),
                    Debit = table.Column<double>(nullable: false),
                    Credit = table.Column<double>(nullable: false),
                    ChequeNo = table.Column<string>(maxLength: 50, nullable: true),
                    Narration = table.Column<string>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction_Details", x => x.TD_ID);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_Master",
                columns: table => new
                {
                    TM_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OP_ID = table.Column<int>(nullable: false),
                    IN_ID = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Type = table.Column<string>(maxLength: 5, nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction_Master", x => x.TM_ID);
                });

            migrationBuilder.CreateTable(
                name: "User_Types",
                columns: table => new
                {
                    USER_TYPE_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userTypeTitle = table.Column<string>(maxLength: 100, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Types", x => x.USER_TYPE_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    USER_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    USER_TYPE_ID = table.Column<int>(nullable: true),
                    username = table.Column<string>(maxLength: 100, nullable: true),
                    password = table.Column<string>(maxLength: 100, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.USER_ID);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VENDOR_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    V_Name = table.Column<string>(maxLength: 100, nullable: true),
                    V_Cnic = table.Column<string>(maxLength: 50, nullable: true),
                    V_Address = table.Column<string>(maxLength: 200, nullable: true),
                    V_PrimaryPhone = table.Column<string>(maxLength: 20, nullable: true),
                    V_SecondaryPhone = table.Column<string>(maxLength: 20, nullable: true),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    DeleteBy = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.VENDOR_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charts_Of_Accounts");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Instalment_Details");

            migrationBuilder.DropTable(
                name: "Instalment_Master");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Inventory_Details");

            migrationBuilder.DropTable(
                name: "Item_Criteria");

            migrationBuilder.DropTable(
                name: "Page_Routes");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Product_Brand");

            migrationBuilder.DropTable(
                name: "Product_Category");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "Purchase_Line");

            migrationBuilder.DropTable(
                name: "Purchase_Transaction_Log");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Sale_Line");

            migrationBuilder.DropTable(
                name: "Sale_Transaction_Log");

            migrationBuilder.DropTable(
                name: "Transaction_Details");

            migrationBuilder.DropTable(
                name: "Transaction_Master");

            migrationBuilder.DropTable(
                name: "User_Types");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vendor");
        }
    }
}
