using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.AuditHistory;
using Wp.CIS.LynkSystems.Interfaces.Administrative;
using Wp.CIS.LynkSystems.Model.Administrative;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Wp.CIS.LynkSystems.Services.Administrative
{
    public class AuditHistoryApi : IAuditHistoryApi
    {
        #region Private Fields

        private IOptions<Settings> _optionsAccessor;

        private IAuditHistoryRepository _auditHistoryRepository;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes the class
        /// </summary>
        /// <param name="optionsAccessor"></param>
        /// <param name="auditHistoryRepository"></param>
        public AuditHistoryApi(IOptions<Settings> optionsAccessor,
            IAuditHistoryRepository auditHistoryRepository)
        {
            this._optionsAccessor = optionsAccessor;

            this._auditHistoryRepository = auditHistoryRepository;
        }

        //public AuditHistoryApi()
        //{
        //    var i = 0;
        //    i++;
        //}

        #endregion

        #region IAuditHistoryApi Implementation

        /// <summary>
        /// This retrieves the most recent audit history record.
        /// </summary>
        /// <param name="lidType"></param>
        /// <param name="lid"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public async Task<AuditHistoryModel> GetLatestAuditHistoryRecordAsync(LidTypeEnum lidType, int lid, ActionTypeEnum actionType)
        {
            AuditHistoryModel response = null;

            var auditRecords = await this._auditHistoryRepository
                                    .GetAuditHistoryAsync(lidType, lid, actionType);

            if(null != auditRecords)
            {
                var auditList = new List<AuditHistoryModel>(auditRecords);

                if(auditList.Count > 0)
                {
                    auditList = auditList
                        .OrderByDescending(currentItem => currentItem.ActionDate)
                        .ToList();
                    response = auditList[0];
                }
            }

            return response;
        }

        #endregion
    }
}
