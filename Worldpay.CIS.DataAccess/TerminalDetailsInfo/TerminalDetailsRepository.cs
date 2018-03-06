using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Worldpay.CIS.DataAccess.Connection;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Dapper;
using System.Data;
using System.Linq;

namespace Worldpay.CIS.DataAccess.TerminalDetailsInfo
{
    public class TerminalDetailsRepository : ITerminalDetailsRepository
    {
        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly IDatabaseConnectionFactory _connectionFactory;

        #endregion

        #region Public Constructors

        public TerminalDetailsRepository(IOptions<DataContext> optionsAccessor,
            IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
            {
                this._connectionFactory = new BaseRepository(optionsAccessor);
            }
            else
            {
                this._connectionFactory = connectionFactory;
            }
        }


        #endregion

        public Task<EAndPData> GetTerminalDetails(int termNbr)
        {
            try
            {
                return this._connectionFactory.GetConnection(async c =>
                {
                    var eandPData = new EAndPData();
                    using (var multi = c.QueryMultiple("[CISPlus].[uspGatherTerminalDetails]  @TermNbr",
                         new { TermNbr = termNbr }))
                    {
                        eandPData.terminalDetails = multi.Read<TerminalDetails>().FirstOrDefault();
                        eandPData.activeServices = multi.Read<ActiveServices>().FirstOrDefault();
                        eandPData.terminalInfo = multi.Read<TerminalInfo>().FirstOrDefault();
                        eandPData.sensitivityInfo = multi.Read<SensitivityInfo>().FirstOrDefault();
                        return eandPData;
                    }
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}