using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Dashboard;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Worldpay.CIS.DataAccess.DashboardInfo
{
    public class DashboardInfoRepository : IDashboardInfoRepository
    {
        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly IDatabaseConnectionFactory _connectionFactory;
        
        #endregion

        #region Public Constructors

        public DashboardInfoRepository(IOptions<DataContext> optionsAccessor, 
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

        #region IDashboardRepository Implementation

        /// <summary>
        /// This returns the primary key information.
        /// </summary>
        /// <param name="lidType"></param>
        /// <param name="lid"></param>
        /// <param name="lidPk"></param>
        /// <returns></returns>
        public async Task<DashboardPrimaryKeysModel> GetDashboardSearchPrimaryKeys(LidTypeEnum lidType, string lid, int? lidPk)
        {
            const string OutTerminalNbrParamName = "OutTerminalNbr";
            const string OutMerchantIDParamName = "OutMerchantID";
            const string OutCustIDParamName = "OutCustID";
            const string OutputLidPkTypeParamName = "OutputLidPkType";

            var response = await this._connectionFactory.GetConnection(async c =>
            {
                var primaryKeyInfo = new DashboardPrimaryKeysModel();

                var p = new DynamicParameters();

                p.Add("InputLid", lid, DbType.String);
                p.Add("InputPk", lidPk, DbType.Int32);
                p.Add("InputType", (int)lidType, DbType.Int32);
                p.Add(OutTerminalNbrParamName, DbType.Int32, direction: ParameterDirection.Output);
                p.Add(OutMerchantIDParamName, DbType.Int32, direction: ParameterDirection.Output);
                p.Add(OutCustIDParamName, DbType.Int32, direction: ParameterDirection.Output);
                p.Add(OutputLidPkTypeParamName, DbType.Int32, direction: ParameterDirection.Output);

                //await c.QueryAsync<Wp.CIS.LynkSystems.Model.TransactionsInquiry>(sql: "[CISPlus].[uspCISPlusGetPrimaryKeys]", param: p, commandType: CommandType.StoredProcedure);
                await c.ExecuteAsync(sql: "[CISPlus].[uspCISPlusGetPrimaryKeys]", param: p, commandType: CommandType.StoredProcedure);

                primaryKeyInfo.TerminalNbr = p.Get<int?>(OutTerminalNbrParamName);
                primaryKeyInfo.MerchantID = p.Get<int?>(OutMerchantIDParamName);
                primaryKeyInfo.CustomerID = p.Get<int?>(OutCustIDParamName);
                primaryKeyInfo.LidType = (LidTypeEnum)p.Get<int?>(OutputLidPkTypeParamName);
                return primaryKeyInfo;
            });

            return response;
        }

        #endregion
        public async Task<Wp.CIS.LynkSystems.Model.DashboardInfo> GetDashboardSearchResults(Helper.LIDTypes LIDtype, int LID, int maxRecordCount)
        {
            try
            {
                string inputType = LIDtype.ToString();
                string outputType = inputType.Replace("ID", "").Replace("Nbr", "");
                Wp.CIS.LynkSystems.Model.DashboardInfo dbInfo = new Wp.CIS.LynkSystems.Model.DashboardInfo();

                return await this._connectionFactory.GetConnection(async c =>
                {
                    var p = new DynamicParameters();
                    p.Add("OutputType", outputType, DbType.String, ParameterDirection.Input);
                    p.Add("LidType", LIDtype, DbType.Int32, ParameterDirection.Input);
                    p.Add("Lid", LID, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    //  p.Add("SelectTopMaxNumber", 500, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    p.Add("TotalCaseHistoryRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("TotalDemographicsRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("TotalMerchantRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    using (var multi = c.QueryMultiple("CISPlus.uspCISPlusGetDashboardInfo", p, commandType: CommandType.StoredProcedure))
                    {
                        switch (LIDtype)
                        {
                            case Helper.LIDTypes.TerminalNbr:
                            case Helper.LIDTypes.TerminalID:
                                dbInfo.TermInfo = multi.Read<TerminalInfo>().FirstOrDefault();
                                dbInfo.MerchInfo = multi.Read<MerchantInfo>().FirstOrDefault();
                                dbInfo.CustProfile = multi.Read<CustomerProfile>().FirstOrDefault();
                                break;
                            case Helper.LIDTypes.MerchantID:
                            case Helper.LIDTypes.MerchantNbr:
                                dbInfo.MerchInfo = multi.Read<MerchantInfo>().FirstOrDefault();
                                dbInfo.CustProfile = multi.Read<CustomerProfile>().FirstOrDefault();
                                break;
                            case Helper.LIDTypes.CustomerID:
                            case Helper.LIDTypes.CustomerNbr:
                                dbInfo.CustProfile = multi.Read<CustomerProfile>().FirstOrDefault();
                                break;
                            default:
                                break;
                        }

                        dbInfo.GroupInfo = multi.Read<Group>().FirstOrDefault();
                        dbInfo.ActvServices = multi.Read<ActiveServices>().FirstOrDefault();
                        dbInfo.DemographicsInfo = multi.Read<Demographics>().ToList();
                        dbInfo.MerchantsList = multi.Read<Merchant>().ToList();
                        dbInfo.CaseHistorysList = multi.Read<Wp.CIS.LynkSystems.Model.CaseHistory>().ToList();
                        dbInfo.TotalNumberOfCaseHistoryRecords = p.Get<int>("TotalCaseHistoryRecords");
                    }

                    if (dbInfo.DemographicsInfo != null && dbInfo.DemographicsInfo.Count > 0)
                    {
                        dbInfo.DemographicsInfoCust = dbInfo.DemographicsInfo.Where(d => d.Level == "Customer").ToList();
                        dbInfo.DemographicsInfoMerch = dbInfo.DemographicsInfo.Where(d => d.Level == "Merchant").ToList();
                        dbInfo.DemographicsInfoTerm = dbInfo.DemographicsInfo.Where(d => d.Level == "Terminal").ToList();
                    }

                    return dbInfo;
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Wp.CIS.LynkSystems.Model.DashboardInfo> GetDashboardSearchResultsPagination(Helper.LIDTypes LIDtype, int LID, int maxRecordCount)
        {
            try
            {
                string inputType = LIDtype.ToString();
                string outputType = inputType.Replace("ID", "").Replace("Nbr", "");
                Wp.CIS.LynkSystems.Model.DashboardInfo dbInfo = new Wp.CIS.LynkSystems.Model.DashboardInfo();



                int totalCaseHistoryRecords = -1;
                int totalMerchantRecords = -1;

                return await this._connectionFactory.GetConnection(async c =>
                {
                    var p = new DynamicParameters();
                    p.Add("OutputType", outputType, DbType.String, ParameterDirection.Input);
                    p.Add("LidType", LIDtype, DbType.Int32, ParameterDirection.Input);
                    p.Add("Lid", LID, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    p.Add("MaximumRecsLimit", maxRecordCount, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    p.Add("TotalCaseHistoryRecords", totalCaseHistoryRecords, dbType: DbType.Int32, direction: ParameterDirection.Output);

                    p.Add("TotalMerchantRecords", totalMerchantRecords, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    using (var multi = c.QueryMultiple("CISPlus.uspCISPlusGetDashboardInfo", p, commandType: CommandType.StoredProcedure))
                    {
                        switch (LIDtype)
                        {
                            case Helper.LIDTypes.TerminalNbr:
                            case Helper.LIDTypes.TerminalID:
                                dbInfo.TermInfo = multi.Read<TerminalInfo>().FirstOrDefault();
                                dbInfo.MerchInfo = multi.Read<MerchantInfo>().FirstOrDefault();
                                dbInfo.CustProfile = multi.Read<CustomerProfile>().FirstOrDefault();
                                break;
                            case Helper.LIDTypes.MerchantID:
                            case Helper.LIDTypes.MerchantNbr:
                                dbInfo.MerchInfo = multi.Read<MerchantInfo>().FirstOrDefault();
                                dbInfo.CustProfile = multi.Read<CustomerProfile>().FirstOrDefault();
                                break;
                            case Helper.LIDTypes.CustomerID:
                            case Helper.LIDTypes.CustomerNbr:
                                dbInfo.CustProfile = multi.Read<CustomerProfile>().FirstOrDefault();
                                break;
                            default:
                                break;
                        }

                        dbInfo.GroupInfo = multi.Read<Group>().FirstOrDefault();
                        dbInfo.ActvServices = multi.Read<ActiveServices>().FirstOrDefault();
                        dbInfo.DemographicsInfo = multi.Read<Demographics>().ToList();
                        // Not retrieving the case history data until the performance issue is solved.
                        //dbInfo.CaseHistorysList = multi.Read<Wp.CIS.LynkSystems.Model.CaseHistory>().ToList();
                        dbInfo.MerchantsList = multi.Read<Merchant>().ToList();
                        // Not retrieving the case history data until the performance issue is solved.
                        //dbInfo.TotalNumberOfCaseHistoryRecords = p.Get<int>("TotalCaseHistoryRecords");
                        dbInfo.TotalMerchantRecords = p.Get<int>("TotalMerchantRecords");
                    
                    }

                    if (dbInfo.DemographicsInfo != null && dbInfo.DemographicsInfo.Count > 0)
                    {
                        dbInfo.DemographicsInfoCust = dbInfo.DemographicsInfo.Where(d => d.Level == "Customer").ToList();
                        dbInfo.DemographicsInfoMerch = dbInfo.DemographicsInfo.Where(d => d.Level == "Merchant").ToList();
                        dbInfo.DemographicsInfoTerm = dbInfo.DemographicsInfo.Where(d => d.Level == "Terminal").ToList();
                        dbInfo.TotalDemographicsInfoCustRecords = dbInfo.DemographicsInfoCust.Count;
                        dbInfo.TotalDemographicsInfoMerchRecords = dbInfo.DemographicsInfoMerch.Count;

                    }

                    //dbInfo.CaseHistoryList = caseHistObj.GetCaseHistory(LIDtype, LID);

                    return dbInfo;
                });
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public Task<TerminalDetails> GetTerminalDetails(int termNbr)
        {
            try
            {
                return this._connectionFactory.GetConnection(async c =>
                {
                    var p = new DynamicParameters();
                    p.Add("TermNbr", termNbr, DbType.Int32);
                    var terminalDetails = await c.QueryFirstOrDefaultAsync<TerminalDetails>(sql: "[dbo].[uspGatherEandPData]", param: p, commandType: CommandType.StoredProcedure);
                    return terminalDetails;
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
