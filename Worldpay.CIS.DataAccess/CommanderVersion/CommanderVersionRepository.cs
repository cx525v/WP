using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.CommanderVersion
{
    public class CommanderVersionRepository : ICommanderVersionRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public CommanderVersionRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;

        }

        public async Task<bool> CreateVersionAsync(Wp.CIS.LynkSystems.Model.CommanderVersion commanderVersion)
        {
            try
            {
              
                var p = new DynamicParameters();
                p.Add("VersionDescription", commanderVersion.VersionDescription, DbType.String);
                p.Add("BaseVersionID", commanderVersion.BaseVersionID, DbType.Int32);
                p.Add("CreatedByUser", commanderVersion.CreatedByUser, DbType.String);
                var result = await CreateVersion(p);
                return await Task.FromResult(result);

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public virtual async Task<bool> CreateVersion(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                await c.ExecuteAsync(sql: "CISPlus.uspEPSCreateNewVersions", param: p, commandType: CommandType.StoredProcedure);
                return true;
            });
        }

        public async Task<bool> DeleteVersionAsync(int versionID, string userName)
        {
            try
            {             
                var p = new DynamicParameters();
                p.Add("VersionID", versionID, DbType.Int32);
                p.Add("UserName", userName, DbType.String);
                var result = await DeleteVersion(p);
                return await Task.FromResult(result);              
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public virtual async Task<bool> DeleteVersion(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                await c.ExecuteAsync(sql: "CISPlus.uspEPSDeleteVersion", param: p, commandType: CommandType.StoredProcedure);
                return true;
            });
        }

        public async Task<ICollection<BaseVersion>> GetBaseVersionsAsync()
        {
            try
            {
                return await this._connectionFactory.GetConnection(async c =>
                {
                    var p = new DynamicParameters();
                    var result = await GetBaseVersions(p);
                    return await Task.FromResult(result);
                });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public virtual async Task<ICollection<BaseVersion>> GetBaseVersions(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                var result = await c.QueryAsync<BaseVersion>(sql: "CISPlus.uspEPSGetBaseVersions", param: p, commandType: CommandType.StoredProcedure);
                return result.ToList();
            });
        }



        public async Task<ICollection<Wp.CIS.LynkSystems.Model.CommanderVersion>> GetVersionsAsync()
        {
            try
            {
              var p = new DynamicParameters();
              var result = await GetVersions(p);
              return await Task.FromResult(result);              
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        public virtual async Task<ICollection<Wp.CIS.LynkSystems.Model.CommanderVersion>> GetVersions(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                var result = await c.QueryAsync<Wp.CIS.LynkSystems.Model.CommanderVersion>(sql: "CISPlus.uspEPSGetVersions", param: p, commandType: CommandType.StoredProcedure);
                return result.ToList();
            });
        }

    }
}
