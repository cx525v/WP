using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model.Administrative;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Worldpay.CIS.DataAccess.ActiveServicesInfo
{
    public class ActiveServicesRepository : IActiveServicesRepository
    {
        #region Private Fields

        private readonly IDatabaseConnectionFactory _connectionFactory;

        #endregion

        #region Public Constructors

        public ActiveServicesRepository(IOptions<DataContext> optionsAccessor, 
            IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        #endregion

        #region IActiveServicesRepository Implementation

        public async Task<ICollection<Wp.CIS.LynkSystems.Model.ActiveServices>> GetActiveServices(int LIDType, int LID)
        {
            try
            {
                var response = await this._connectionFactory.GetConnection(async c =>
                {
                    return (await c.QueryAsync<Wp.CIS.LynkSystems.Model.ActiveServices>("EXEC CISPlus.uspSLKeyedSearchTableStructure @LidType,@Lid ",
                            new { LIDType, LID })).ToList();
                });

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
