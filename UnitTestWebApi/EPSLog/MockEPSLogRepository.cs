using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.EpsLog;

namespace CIS.WebApi.UnitTests.EpsLog
{
    public class MockEPSLogRepository: IEPSLogRepository
    {
        public ICollection<Wp.CIS.LynkSystems.Model.EPSLog> epslogs;
        public bool FailGet { get; set; }

        public MockEPSLogRepository()
        {
            epslogs = new List<Wp.CIS.LynkSystems.Model.EPSLog> {
            new Wp.CIS.LynkSystems.Model.EPSLog{
              merchantNbr ="542929803226091",
              terminalID = "LK102248",
              downloadDate = "2017-07-05 12:29:39",
              actionType="GetUpdateAvailable",
              download="No",
              success ="Yes",
              responseMessage =null
            },

              new Wp.CIS.LynkSystems.Model.EPSLog{
              merchantNbr ="542929803226091",
              terminalID = "LK102248",
              downloadDate = "2017-07-05 12:29:39",
              actionType="GetTableUpdate",
              download="No",
              success ="Yes",
              responseMessage =null
            },

               new Wp.CIS.LynkSystems.Model.EPSLog{
              merchantNbr ="542929803226091",
              terminalID = "LK102248",
              downloadDate = "2017-07-05 12:30:28",
              actionType="SendAck",
              download="Yes",
              success ="Yes",
              responseMessage =null
            },

            };

        }
        

        public Task<ICollection<Wp.CIS.LynkSystems.Model.EPSLog>> GetEPSLogAsync(string startDate, string endDate, int? LidType, string Lid)
        {
            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                throw new Exception("Start or End date not provided");
            }
            return Task.Run(()=> epslogs);
        }

        
    }
}
