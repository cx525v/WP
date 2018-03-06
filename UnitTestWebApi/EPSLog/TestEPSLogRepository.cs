
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Linq;
using Xunit;
using Worldpay.CIS.DataAccess.MerchantList;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using Wp.CIS.LynkSystems.Model.Pagination;
using Worldpay.Logging.Providers.Log4Net.Facade;
using System.Threading.Tasks;
using Dapper;
using CIS.WebApi.UnitTests.EpsLog;
using Worldpay.CIS.DataAccess.EpsLog;

namespace CIS.WebApi.UnitTests.EPSLog
{
    public class TestEPSLogRepository
    {
        [Fact]
        public void EPSLogRepositoryTest_Success()
        {
            string startDate = "2017-12-12";
            string endDate = "2017-12-30";

            MockEPSLogRepository mockRepository = new MockEPSLogRepository();
            Task<ICollection<Wp.CIS.LynkSystems.Model.EPSLog>> expectedResult = mockRepository.GetEPSLogAsync(startDate, endDate, null, string.Empty);
            //DataContext dc = new DataContext()
            //{
            //    CisConnectionString = @"Fake String"
            //};
            //IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            //optionsAccessor = Options.Create(dc);

            //IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
           // EPSLogRepository mockRepo = Substitute.For<EPSLogRepository>(optionsAccessor, connectionFactory);
            IEPSLogRepository mockRepo = Substitute.For<IEPSLogRepository>();
            mockRepo.WhenForAnyArgs(x => x.GetEPSLogAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>(), Arg.Any<string>())).DoNotCallBase();
            mockRepo.GetEPSLogAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>(), Arg.Any<string>()).ReturnsForAnyArgs(expectedResult);

            //mockRepo.WhenForAnyArgs(async x => await x.GetEPSLogAsync(Arg.Any<DynamicParameters>())).DoNotCallBase();
            //  mockRepo.WhenForAnyArgs(async x => await x.GetValuesAsync(Arg.Any<DynamicParameters>() )).DoNotCallBase();
            // mockRepo.GetValuesAsync(Arg.Any<DynamicParameters>()).ReturnsForAnyArgs(expectedResult);

            var logs = mockRepo.GetEPSLogAsync(startDate,endDate,null,string.Empty).Result;
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.EPSLog>)logs;

            Assert.Equal(actualRecord.Count, expectedResult.Result.Count);
        }

        //[Fact]
        ////If Database connection string is null 
        //public void EPSLogRepositoryTest_UnSuccess_DbConnection()
        //{option
        //    // Arrange
        //    int CustomerID = 191807;
        //    //string mid = "191807";

        //    MockMerchantListRepository mockMerchantListRepository = new MockMerchantListRepository();
        //    GenericPaginationResponse<Merchant> expectedResult = mockMerchantListRepository.GetMockDataForRepository(CustomerID);
        //    PaginationMerchant page = mockMerchantListRepository.GetPagination();

        //    DataContext dc = new DataContext()
        //    {
        //        MaxNumberOfRecordsToReturn = 2,
        //        CisConnectionString = null
        //    };
        //    IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
        //    optionsAccessor = Options.Create(dc);

        //    ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
        //    loggingFacade.When(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

        //    IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();

        //    MerchantListRepository mockRepo = new MerchantListRepository(optionsAccessor, connectionFactory, loggingFacade);

        //    ////Act & Assert
        //    Assert.ThrowsAsync<Exception>(() => mockRepo.GetMerchantListAsync(CustomerID, page));

        //}

        //[Fact]
        ////If there is any exception 
        //public void EPSLogRepositoryTest_Exception()
        //{
        //    // Arrange
        //    int CustomerID = 191809;


        //    MockEPSLogRepository mockMerchantListRepository = new MockEPSLogRepository();
        //    GenericPaginationResponse<Merchant> expectedResult = mockMerchantListRepository.GetMockDataForRepository(CustomerID);
        //    PaginationMerchant page = mockMerchantListRepository.GetPagination();

        //    DataContext dc = new DataContext()
        //    {
        //        MaxNumberOfRecordsToReturn = 2,
        //        CisConnectionString = @"Fake String"
        //    };
        //    IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
        //    optionsAccessor = Options.Create(dc);

        //    ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
        //    loggingFacade.When(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();


        //    IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
        //    MerchantListRepository mockRepo = Substitute.For<MerchantListRepository>(optionsAccessor, connectionFactory, loggingFacade);

        //    mockRepo.When(x => x.GetMerchantList(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<DynamicParameters>(), Arg.Any<GenericPaginationResponse<Merchant>>())).DoNotCallBase();
        //    mockRepo.GetMerchantList(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<DynamicParameters>(), Arg.Any<GenericPaginationResponse<Merchant>>()).Throws(new Exception());


        //    // Act & Assert                        
        //    Assert.ThrowsAsync<Exception>(() => mockRepo.GetMerchantListAsync(CustomerID, page));

        //}
    }
}
