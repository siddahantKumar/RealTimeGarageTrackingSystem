using IndusShowroomApi.Data.DatabaseContexts;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Data.Repos;
using IndusShowroomApi.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace IndusShowroomApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void DependencyInjection(IServiceCollection services)
        {
            // Dependency injecting my interfaces to my repositories

            //Car and its related details
            services.AddScoped<IProduct_BrandRepo, SqlProduct_BrandRepo>();
            services.AddScoped<IProduct_CategoryRepo, SqlProduct_CategoryRepo>();
            services.AddScoped<IProductRepo, SqlProductRepo>();

            //instalement
            services.AddScoped<IInstalment_MasterRepo, SqlInstalment_MasterRepo>();
            services.AddScoped<IInstalment_DetailsRepo, SqlInstalment_DetailsRepo>();

            //for dyanmic user interfacing
            services.AddScoped<IUserRepo, SqlUserRepo>();
            services.AddScoped<IPageRoutesRepo, SqlPageRoutesRepo>();
            services.AddScoped<IUserTypeRepo, SqlUserTypeRepo>();

            //Sale Purchase and Inventory
            services.AddScoped<ISaleRepo, SqlSaleRepo>();
            services.AddScoped<ISale_LineRepo, SqlSale_LineRepo>();
            services.AddScoped<ISale_Transaction_LogRepo, SqlSale_Transaction_LogRepo>();

            //Purchase
            services.AddScoped<IPurchaseRepo, SqlPurchaseRepo>();
            services.AddScoped<IPurchase_LineRepo, SqlPurchase_LineRepo>();
            services.AddScoped<IPurchase_Transaction_LogRepo, SqlPurchase_Transaction_LogRepo>();

            //Inventory
            services.AddScoped<IInventoryRepo, SqlInventoryRepo>();
            services.AddScoped<IInventory_DetailsRepo, SqlInvetory_DetailsRepo>();
            services.AddScoped<IItem_CriteriaRepo, SqlItem_CriteriaRepo>();

            //Customers and vendors
            services.AddScoped<ICustomerRepo, SqlCustomerRepo>();
            services.AddScoped<IVendorRepo, SqlVendorRepo>();
            
            //Transactionals
            services.AddScoped<ITransaction_MasterRepo, SqlTransaction_MasterRepo>();
            services.AddScoped<ITransaction_DetailsRepo, SqlTransaction_DetailsRepo>();
            services.AddScoped<ICharts_Of_AccountsRepo, SqlCharts_Of_AccountsRepo>();
            
            //Services BLL
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IPaymentReportService, PaymentReportService>();
            services.AddScoped<ISetupService, SetupService>();

           

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddDbContextPool<DatabaseContext>(options => options.UseMySql(Configuration.GetConnectionString("Connection"))); //   add db contextclaass so we can migrate our tables 
             //here we take connection string from connection variable from application.json class and assign variable to getconnectionstring           


            services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials()));

            services.AddControllers().AddNewtonsoftJson(); // <------ Activate patch operations on JsonPatchDocument

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // <--------- To use automapper in my application

            services.AddMemoryCache();


            DependencyInjection(services);

            //JWT Pipeline
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("This is my test key")),
                    ClockSkew = TimeSpan.Zero

                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseCors();

            app.UseRouting();

            app.UseAuthentication(); //<------ TO use authorization in headers
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
