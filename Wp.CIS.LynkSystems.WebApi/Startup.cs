
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Worldpay.CIS.DataAccess.Connection;
using Worldpay.CIS.DataAccess.MerchantProfile;
using Worldpay.CIS.Utilities;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services.Administrative;
using Wp.CIS.LynkSystems.Services.Lookup;
using Wp.CIS.LynkSystems.Interfaces.Lookup;
using Worldpay.CIS.DataAccess.EpsMapping;
using Worldpay.CIS.DataAccess.CommanderVersion;
using Worldpay.CIS.DataAccess.EpsLog;
using Worldpay.CIS.DataAccess.EpsTable;
using Worldpay.CIS.DataAccess.Product;
using Worldpay.CIS.DataAccess.ProductType;
using Worldpay.CIS.DataAccess.Manufacturer;
using Worldpay.CIS.DataAccess.InstallType;
using Worldpay.CIS.DataAccess.DownloadTime;
using Worldpay.CIS.DataAccess.Brand;
using Worldpay.CIS.DataAccess.MobileLookup;
using Wp.CIS.LynkSystems.Interfaces.Administrative;
using Worldpay.CIS.DataAccess.AuditHistory;
using Wp.CIS.LynkSystems.Model.Authentication;
using Wp.CIS.LynkSystems.Interfaces.Secuirity;
using Wp.CIS.LynkSystems.Services.Security;
using Worldpay.CIS.DataAccess.TransactionsInqDetailsInfo;
using Worldpay.CIS.DataAccess.TransactionsInqTerminalInfo;
using Worldpay.CIS.DataAccess.MerchantList;
using Worldpay.CIS.DataAccess.TerminalList;
using Worldpay.CIS.DataAccess.EpsPetroAudit;
using Wp.CIS.LynkSystems.WebApi.Common;
using Worldpay.CIS.DataAccess.DashboardInfo;
using Worldpay.CIS.DataAccess.ContactList;
using System.IO;
using System.Reflection;
using System.Threading;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Options;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Providers.Log4Net.Models;

using Worldpay.CIS.DataAccess.RecentStatement;
using Worldpay.CIS.DataAccess.TerminalDetailsInfo;
using Worldpay.CIS.DataAccess.TerminalDetailsSettlementInfo;
using Worldpay.CIS.DataAccess.TransactionHistory;
using Worldpay.CIS.DataAccess.MemoInfo;
using Worldpay.CIS.DataAccess.CaseHistory;
using Worldpay.CIS.DataAccess.BankingInfo;
using Worldpay.CIS.DataAccess.Parameters;

namespace Wp.CIS.LynkSystems.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfigurationRoot Configuration { get; }
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

	        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

	        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

	        var logger = LogManager.GetLogger(typeof(Startup));

	        logger.Error(DateTime.Now + "Started Application CISPlus");
	        logger.Debug(DateTime.Now + "From Debug: Started Application CISPlus");

			/*****/
	        services.Configure<Log4NetConfig>(Configuration.GetSection("Log4NetConfig"));
	        var provider2 = services.BuildServiceProvider();
	        var settings = provider2.GetService<IOptions<Log4NetConfig>>().Value;
	        var loggingFacade = new LoggingFacade(settings);
	        var assembly = Assembly.GetEntryAssembly();
	        var logEntry1 = new LogEntry(LogLevels.Info, "Starting CISPlus Application", "Startup.cs", "ConfigureServices");
			loggingFacade.LogAsync(logEntry1, CancellationToken.None);
			services.AddSingleton<Worldpay.Logging.Providers.Log4Net.Facade.ILoggingFacade>(loggingFacade);

			/* **********/

			// Add framework services.
			// Add service and create Policy with options
			services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()));
            services.AddOptions();
            services.AddSingleton(provider => Configuration);

            // Register the IConfiguration instance which MyOptions binds against.
            services.Configure<Settings>(Configuration);
            services.Configure<Settings>(myOptions =>
            {
                myOptions.CISConnectionString = Configuration.GetConnectionString("CISConnectionString");
                myOptions.StarV3ConnectionString = Configuration.GetConnectionString("StarV3ConnectionString");
                myOptions.CacheDatabase = Configuration.GetConnectionString("CacheDatabase");
                myOptions.HistoryConnectionString = Configuration.GetConnectionString("HistoryConnectionString");
                myOptions.HistoryTierConnectionString = Configuration.GetConnectionString("HistoryTierConnectionString");
                myOptions.CISStageConnectionString = Configuration.GetConnectionString("CISStageConnectionString");
                myOptions.StaticReportsConnectionString = Configuration.GetConnectionString("StaticReportsConnectionString");
                myOptions.MaxNumberOfRecordsToReturn = Configuration.GetSection("DatabaseDefaults").GetValue<int>("MaxNumberOfRecordsToReturn", 500);
                myOptions.EnvironmentName = Configuration.GetValue<string>("EnvironmentName");
                myOptions.TranHistSumConnectionString = Configuration.GetConnectionString("TranHistSumConnectionString");
            });
            services.Configure<DataContext>(Configuration);
            services.Configure<DataContext>(myOptions =>
            {
                myOptions.CisConnectionString = Configuration.GetConnectionString("CISConnectionString");
                myOptions.StarV3ConnectionString = Configuration.GetConnectionString("StarV3ConnectionString");
                myOptions.CacheDatabase = Configuration.GetConnectionString("CacheDatabase");
                myOptions.HistoryConnectionString = Configuration.GetConnectionString("HistoryConnectionString");
                myOptions.HistoryTierConnectionString = Configuration.GetConnectionString("HistoryTierConnectionString");
                myOptions.CisStageConnectionString = Configuration.GetConnectionString("CISStageConnectionString");
                myOptions.StaticReportsConnectionString = Configuration.GetConnectionString("StaticReportsConnectionString");
                myOptions.TranHistSumConnectionString = Configuration.GetConnectionString("TranHistSumConnectionString");
                myOptions.TranHistoryConnectionString = Configuration.GetConnectionString("TranHistoryConnectionString");
                myOptions.MaxNumberOfRecordsToReturn = Convert.ToInt32(Configuration["DatabaseDefaults:MaxNumberOfRecordsToReturn"]);
                myOptions.CommandTimeout = Convert.ToInt32(Configuration["DatabaseDefaults:CommandTimeout"]);
            });
            // Add framework services.
            services.AddMvc();
            services.AddMvc(
                config =>
                {
                    config.Filters.Add(typeof(ApiExceptionFilter));
                    config.Filters.Add(typeof(ValidationFormFilter));
                });
            
            services.AddAuthentication
                (options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
              .AddJwtBearer(cfg =>
              {

                  cfg.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuerSigningKey = true,
                      ValidateIssuer = true,
                      ValidIssuer = Configuration["Token:Issuer"],
                      ValidAudience = Configuration["Token:Audience"],
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ClockSkew = TimeSpan.Zero,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"]))
                  };

              });

            services.AddIdentity<Users, UserRole>()                                
                                .AddDefaultTokenProviders();
            //Add framework services.
           
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("CacheDatabase");
                options.SchemaName = "dbo";
                options.TableName = "CIS_CacheTable";
            });


            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Wp.CIS.LynkSystems.WebApi", Version = "v1" });
            });
			//services.AddSingleton<ILog>(logger);
			services.AddScoped<IOperation, Common.Operation>();
            services.AddScoped<IDatabaseConnectionFactory, BaseRepository>();
            services.AddScoped<IMerchantProfileRepository, MerchantProfileRepository>();
            services.AddScoped<IMerchantProfileApi, MerchantProfileApi>();
            services.AddScoped<IActiveServicesApi, ActiveServicesApi>();
            services.AddScoped<IEPSLogRepository, EPSLogRepository>();
            services.AddScoped<IEPSLogApi, EPSLogApi>();
            services.AddScoped<IEPSMappingRepository, EPSMappingRepository>();
            services.AddScoped<ICardTypes, CardTypesApi>();
            services.AddScoped<IEPSMappingApi, EPSMappingApi>();
            services.AddScoped<IUserApi, UserApi>();
            services.AddScoped<IDashboardInfoApi, DashboardInfoApi>();
            services.AddScoped<IBankingApi, BankingApi>();
            services.AddScoped<IBankingInfoRepository, BankingInfoRepository>();
            services.AddScoped<ITransactionsInquiryDetailsInfoApi, TransactionInquiryDetailsInfoApi>();
            services.AddScoped<ITransactionsInquiryDetailsInfoTierApi, TransactionsInquiryDetailsTierApi>();
            services.AddScoped<ITransactionsInquiryTerminalInfoApi, TransactionsInquiryTerminalInfoApi>();
            services.AddScoped<ITransactionsInqTerminalInfoRepository, TransactionsInqTerminalInfoRepository>();

            services.AddScoped<ITransactionHistoryApi, TransactionHistoryApi>();
            services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();
            //services.AddScoped<ITransactionsInquiryTypesRepository, TransactionsInquiryTypesRepository>();

            services.AddScoped<ITransactionInquiryTypes,TransactionInquiryTypesApi>();
            services.AddScoped<ICommanderVersionRepository, CommanderVersionRepository>();
            services.AddScoped<ITransactionsInqDetailsInfoRepository, TransactionsInqDetailsInfoRepository>();
            services.AddScoped<ICommanderVersionApi, CommanderVersionApi>();
            services.AddScoped<IEPSTableRepository, EPSTableRepository>();
            services.AddScoped<IEPSTableApi, EPSTableApi>();
            services.AddScoped<ICaseHistoryApi, CaseHistoryApi>();
            services.AddScoped<ICaseHistoryRepository, CaseHistoryRepository>();
            services.AddScoped<IParametersApi, ParametersApi>();
            services.AddScoped<IProductApi, ProductApi>();
            services.AddScoped<IDownloadTimesApi, DownloadTimesApi>();
            services.AddScoped<IProductTypesApi, ProductTypesApi>();
            services.AddScoped<IManufacturersApi, ManufacturersApi>();
            services.AddScoped<IInstallTypesApi, InstallTypesApi>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IInstallTypeRepository, InstallTypeRepository>();
            services.AddScoped<IDownloadTimeRepository, DownloadTimeRepository>();
            services.AddScoped<IBrandApi, BrandApi>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IMobileLookupApi, MobileLookupApi>();
            services.AddScoped<IMobileLookupRepository, MobileLookupRepository>();
            services.AddScoped<IAuditHistoryApi, AuditHistoryApi>();
            services.AddScoped<IAuditHistoryRepository, AuditHistoryRepository>();
            services.AddScoped<IMerchantListApi, MerchantListApi>();
            services.AddScoped<IMerchantListRepository, MerchantListRepository>();
            services.AddScoped<ITerminalListApi, TerminalListApi>();
            services.AddScoped<ITerminalListRepository, TerminalListRepository>();
            services.AddScoped<IContactListApi, ContactListApi>();
            services.AddScoped<IContactListRepository, ContactListRepository>();

            services.AddScoped<IEPSPetroAuditApi, EPSPetroAuditApi>();
            services.AddScoped<IEPSPetroAuditRepository, EPSPetroAuditRepository>();
            services.AddScoped<IAuthorisedClaimApi, AuthorisedClaimApi>();

            services.AddScoped<IDashboardInfoApi, DashboardInfoApi>();
            services.AddScoped<IDashboardInfoRepository, DashboardInfoRepository>();
            services.AddScoped<IRecentStatementApi, RecentStatementApi>();
            services.AddScoped<IRecentStatementRepository, RecentStatementRepository>();
            services.AddScoped<ITerminalDetailsApi, TerminalDetailsApi>();
            services.AddScoped<ITerminalDetailsRepository, TerminalDetailsRepository>();
            services.AddScoped<ITerminalDetailsSettlementInfoRepository, TerminalDetailsSettlementInfoRepository>();
            services.AddScoped<IMemoInfoApi, MemoInfoApi>();
            services.AddScoped<IMemoInfoRepository, MemoInfoRepository>();
            
            services.AddScoped<IParametersRepository, ParametersRepository>();
            services.AddScoped<IParametersApi, ParametersApi>();
            services.AddScoped<IXmlApi, XmlApi>();
            services.AddLocalization(opts => opts.ResourcesPath = "Resources");            
            
            //Add Authorization should be called in last. So put at end of the Method.
            services.AddMvcCore()
                    .AddAuthorization();
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseResponseCompression();

            app.UseMiddleware<RequestLoggingHandler>();Use
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("CorsPolicy");
            app.UseCors(builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });
        }
    }
}
