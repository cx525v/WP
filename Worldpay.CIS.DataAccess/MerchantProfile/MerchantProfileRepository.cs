using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Worldpay.CIS.DataAccess.Connection;

namespace Worldpay.CIS.DataAccess.MerchantProfile
{
    public class MerchantProfileRepository: IMerchantProfileRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public MerchantProfileRepository(IOptions<DataContext> optionsAccessor,IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
           
        }
       
        public async Task<Wp.CIS.LynkSystems.Model.MerchantProfile> GetMerchantProfileGeneralInfoAsync(int mid)
        {
            try
            {
                return await this._connectionFactory.GetConnection(async c =>
                {
                    var p = new DynamicParameters();
                    p.Add("MerchantID", mid, DbType.Int64);
                    var merchantprofile = await c.QueryAsync<Wp.CIS.LynkSystems.Model.MerchantProfile>(sql: "USP_MerchProfGetRow", param: p, commandType: CommandType.StoredProcedure);
                    return merchantprofile.FirstOrDefault();
                });
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
       
}

