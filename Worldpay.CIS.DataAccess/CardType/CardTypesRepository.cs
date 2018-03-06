using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Wp.CIS.LynkSystems.Model;
using Worldpay.CIS.DataAccess.TransactionsInqDetailsInfo;
using Worldpay.CIS.DataAccess.Connection;

namespace Worldpay.CIS.DataAccess.CardType
{
    public class CardTypesRepository: ICardTypesRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public CardTypesRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        public async Task<System.Collections.Generic.ICollection<CardTypes>> GetTransInquiryCardTypes()
       
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                var cardtypes = await c.QueryAsync<Wp.CIS.LynkSystems.Model.CardTypes>(sql: "[CISPlus].uspCardTypes", commandType: CommandType.StoredProcedure);
                return cardtypes.ToList();

            });
        }





    }
}
