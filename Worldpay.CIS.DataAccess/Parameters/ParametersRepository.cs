using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;

namespace Worldpay.CIS.DataAccess.Parameters
{
    public class ParametersRepository : IParametersRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public ParametersRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        public async Task<ICollection<Wp.CIS.LynkSystems.Model.Parameters>> GetParametersAsync(int? parameterId = null)
        {
            try
            {             
                var p = new DynamicParameters();
                p.Add("ParameterId", parameterId, DbType.Int32);
                var result = await GetParameters(p);
                return await Task.FromResult(result);               
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public virtual async Task<ICollection<Wp.CIS.LynkSystems.Model.Parameters>> GetParameters(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                var result = await c.QueryAsync<Wp.CIS.LynkSystems.Model.Parameters>(sql: "USP_CBS_GetParameters", param: p, commandType: CommandType.StoredProcedure);
                return result.ToList();
            });
        }

    }
}
