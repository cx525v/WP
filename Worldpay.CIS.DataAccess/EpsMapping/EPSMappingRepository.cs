using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.EpsMapping
{
    public class EPSMappingRepository : IEPSMappingRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public EPSMappingRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        public async Task<ICollection<EPSMapping>> RetrieveEPSMappingAsync(int versionID)
        {
            try
            {              
                var p = new DynamicParameters();
                p.Add("VersionID", versionID, DbType.Int32);
                var result = await RetrieveEPSMapping(p);
                return await Task.FromResult(result);               
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public virtual async Task<ICollection<EPSMapping>> RetrieveEPSMapping(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                var result = await c.QueryAsync<EPSMapping>(sql: "CISPlus.uspEPSPetroGetMappingsByVersion", param: p, commandType: CommandType.StoredProcedure);
                return result.ToList();
            });
        }


        public async Task<bool> UpdateEPSMappingAsync(EPSMapping mapping)
        {
            try
            {            
                var p = new DynamicParameters();
                p.Add("VersionID", mapping.versionID, DbType.Int32);
                p.Add("MappingID", mapping.mappingID, DbType.Int32);
                p.Add("PDLFlag", mapping.pdlFlag, DbType.Boolean);
                p.Add("ParamID", mapping.paramID, DbType.Int32);
                p.Add("WorldPayFieldName", mapping.worldPayFieldName, DbType.String);
                p.Add("WorldPayTableName", mapping.worldPayTableName, DbType.String);
                p.Add("WorldPayJoinFields", mapping.worldPayJoinFields, DbType.String);
                p.Add("WorldPayCondition", mapping.worldPayCondition, DbType.String);
                p.Add("WorldPayOrderBy", mapping.worldPayOrderBy, DbType.String);
                p.Add("WorldPayFieldDescription", mapping.worldPayFieldDescription, DbType.String);
                p.Add("EffectiveBeginDate", mapping.effectiveBeginDate, DbType.DateTime);
                p.Add("EffectiveEndDate", mapping.effectiveEndDate, DbType.DateTime);
                p.Add("VIPERTableName", mapping.viperTableName, DbType.String);
                p.Add("VIPERFieldName", mapping.viperFieldName, DbType.String);
                p.Add("ViperCondition", mapping.viperCondition, DbType.String);
                p.Add("CharStartIndex", mapping.charStartIndex, DbType.Int32);
                p.Add("CharLength", mapping.charLength, DbType.Int32);
                p.Add("LastUpdatedBy", mapping.lastUpdatedBy, DbType.String);

                var result = await UpdateEPSMapping(p);
                return await Task.FromResult(result);
               
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public virtual async Task<bool> UpdateEPSMapping(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                await c.ExecuteAsync(sql: "CISPlus.uspEPSPetroUpdateMapping", param: p, commandType: CommandType.StoredProcedure);
                return true;
            });
        }

        public async Task<bool> BulkInsertEPSMappingAsync(int versionId, string strFormatFileName, string strDataFileName)
        {
            try
            {             
                var p = new DynamicParameters();
                p.Add("VersionID", versionId, DbType.Int32);
                p.Add("FormatFileName", strFormatFileName, DbType.String);
                p.Add("DataFileName", strDataFileName, DbType.String);
                var result = await BulkInsertEPSMapping(p);
                return await Task.FromResult(result);              
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public virtual async Task<bool> BulkInsertEPSMapping(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                await c.ExecuteAsync(sql: "CISPlus.uspCISPlusEPSInsertMappings", param: p, commandType: CommandType.StoredProcedure);
                return true;
            });
        }

        public async Task<bool> CopyEpsMappingAsync(int fromVersionId, int toVersionId, string userName)
        {
            try
            {              
                var p = new DynamicParameters();
                p.Add("FromVersionId", fromVersionId, DbType.Int32);
                p.Add("ToVersionID", toVersionId, DbType.Int32);
                p.Add("UserName", userName, DbType.String);
                var result = await CopyEpsMapping(p);
                return await Task.FromResult(result);               
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public virtual async Task<bool> CopyEpsMapping(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                await c.ExecuteAsync(sql: "CISPlus.uspCISPlusEPSCopyMappings", param: p, commandType: CommandType.StoredProcedure);
                return true;
            });
        }

        public async Task<bool> InsertEPSMappingAsync(EPSMapping mapping)
        {
            try
            {              
                var p = new DynamicParameters();
                p.Add("VersionID", mapping.versionID, DbType.Int32);
                p.Add("PDLFlag", mapping.pdlFlag, DbType.Boolean);
                p.Add("ParamID", mapping.paramID, DbType.Int32);
                p.Add("WorldPayFieldName", mapping.worldPayFieldName, DbType.String);
                p.Add("WorldPayTableName", mapping.worldPayTableName, DbType.String);
                p.Add("WorldPayJoinFields", mapping.worldPayJoinFields, DbType.String);
                p.Add("WorldPayCondition", mapping.worldPayCondition, DbType.String);
                p.Add("WorldPayOrderBy", mapping.worldPayOrderBy, DbType.String);
                p.Add("WorldPayFieldDescription", mapping.worldPayFieldDescription, DbType.String);
                p.Add("EffectiveBeginDate", mapping.effectiveBeginDate, DbType.DateTime);
                p.Add("EffectiveEndDate", mapping.effectiveEndDate, DbType.DateTime);
                p.Add("VIPERTableName", mapping.viperTableName, DbType.String);
                p.Add("VIPERFieldName", mapping.viperFieldName, DbType.String);
                p.Add("ViperCondition", mapping.viperCondition, DbType.String);
                p.Add("CharStartIndex", mapping.charStartIndex, DbType.Int32);
                p.Add("CharLength", mapping.charLength, DbType.Int32);
                p.Add("CreatedByUser", mapping.createdByUser, DbType.String);
                var result = await InsertEPSMapping(p);
                return await Task.FromResult(result);              
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public virtual async Task<bool> InsertEPSMapping(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                await c.ExecuteAsync(sql: "CISPlus.uspEPSPetroInsertMapping", param: p, commandType: CommandType.StoredProcedure);
                return true;
            });
        }
    }
}
