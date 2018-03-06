using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.ContactList;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Error;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Wp.CIS.LynkSystems.Services
{
    public class ContactListApi : IContactListApi
    {
        #region Constructor 
        public IContactListRepository _contactRepository;
        private readonly ILoggingFacade _loggingFacade;
        public ContactListApi(IOptions<Settings> optionsAccessor, 
                              IContactListRepository contactRepository,
                              ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Contact List API Service",
                                    "ContactListApi.cs", "ContactListApi"), CancellationToken.None);
            _contactRepository = contactRepository;
            
        }
        #endregion
        public async Task<ApiResult<GenericPaginationResponse<Demographics>>> GetContactListAsync(LidTypeEnum LIDType, string LID, PaginationDemographics page)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Contact List GetContactListAsync for " + LIDType.ToString() + ", Value - " + LID ,
                                    "ContactListApi.cs", "GetContactListAsync"), CancellationToken.None);

            ApiResult<GenericPaginationResponse<Demographics>> response = new ApiResult<GenericPaginationResponse<Demographics>>();
           
            try
            {
                response.Result = await _contactRepository.GetContactListAsync(LIDType, LID, page);

                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Fetched the Contact List resultset from DB for  " + LIDType.ToString() + ", Value - " + LID,
                                    "ContactListApi.cs", "GetContactListAsync"), CancellationToken.None);
            }
            catch (Exception)
            {
               
                throw;
            }
            return response;
        }
    }
}
