using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;

namespace Worldpay.CIS.DataAccess.EpsTable
{
    public class EPSTableRepository : IEPSTableRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public EPSTableRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }
        public async Task<ICollection<PetroTable>> EPSGetAllPetroTablesByVersionAsync(int versionID)
        {
            try
            {              
                var p = new DynamicParameters();
                p.Add("VersionID", versionID, DbType.Int32);
                var result = await EPSGetAllPetroTablesByVersion(p);
                return await Task.FromResult(result);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public virtual async Task<ICollection<PetroTable>> EPSGetAllPetroTablesByVersion(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                var result = await c.QueryAsync<PetroTable>(sql: "CISPlus.uspEPSGetAllPetroTablesByVersion", param: p, commandType: CommandType.StoredProcedure);
                return result.ToList();
            });
        }



        public async Task<EPSTableErrorCodes> EPSUpsertPetroTableAsync(PetroTable petroTable)
        {
            try
            {              
                var p = new DynamicParameters();
                p.Add("VersionID", petroTable.VersionID, DbType.Int32);
                p.Add("TableName", petroTable.TableName, DbType.String);
                p.Add("Active", petroTable.Active, DbType.Boolean);
                p.Add("DefinitionOnly", petroTable.DefinitionOnly, DbType.Boolean);
                p.Add("SchemaDef", petroTable.SchemaDef, DbType.Xml);
                p.Add("DefaultXML", string.IsNullOrEmpty(petroTable.DefaultXML) ? "" : petroTable.DefaultXML, DbType.Xml);//
                p.Add("EffectiveDate", petroTable.EffectiveDate, DbType.DateTime);
                p.Add("LastUpdatedBy", petroTable.LastUpdatedBy, DbType.String);
                var result = await EPSUpsertPetroTable(p);
                return await Task.FromResult(result);
            }
            catch (System.Exception)
            {            
                throw;
            }
        }
        public virtual async Task<EPSTableErrorCodes> EPSUpsertPetroTable(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                var result = await c.ExecuteAsync(sql: "CISPlus.uspEPSUpsertPetroTable", param: p, commandType: CommandType.StoredProcedure);
                return EPSTableErrorCodes.Succeeded;
            });
        }

    }
}
