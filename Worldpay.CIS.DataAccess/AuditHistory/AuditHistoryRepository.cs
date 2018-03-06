using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model.Administrative;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Worldpay.CIS.DataAccess.AuditHistory
{
    public class AuditHistoryRepository : IAuditHistoryRepository
    {
        #region Private Fields

        private readonly IDatabaseConnectionFactory _connectionFactory;

        #endregion

        #region Public Constructors

        public AuditHistoryRepository(IOptions<DataContext> optionsAccessor, 
            IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        #endregion

        #region IAuditHistoryRepository Implementation

        public async Task<IEnumerable<AuditHistoryModel>> GetAuditHistoryAsync(LidTypeEnum lidType, int lid, ActionTypeEnum actionType)
        {
            var response = await this._connectionFactory.GetConnection(async c =>
            {
                IEnumerable<AuditHistoryModel> auditRecords = null;

                var p = new DynamicParameters();

                p.Add("LidType", (int)lidType, DbType.Int32);
                p.Add("Lid", (int)lid, DbType.Int32);
                p.Add("ActionType", (int)actionType, DbType.Int32);
                auditRecords = await c.QueryAsync<AuditHistoryModel>(sql: "uspGetAuditHistory", param: p, commandType: CommandType.StoredProcedure);
                return auditRecords;
            });

            return response;
        }

        #endregion
    }
}
