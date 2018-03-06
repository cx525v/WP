using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Microsoft.Extensions.Options;
using Wp.CIS.LynkSystems.Services.DapperConnection;
namespace Wp.CIS.LynkSystems.Services
{
    public class CardTypesApi : ConnectionManager, ICardTypes
    {
        //ToDO: Need to load connection string from app settings
        public CardTypesApi(IOptions<Settings> optionsAccessor) : base(optionsAccessor.Value.CISStageConnectionString)
        {

        }

        public async Task<ICollection<CardTypes>> GetCISCardTypes()
        {
            return await WithConnection(async c =>
            {
                var cardtypes = await c.QueryAsync<Model.CardTypes>(sql: "[CISNOW].CardTypes", commandType: CommandType.StoredProcedure);
                return cardtypes.ToList();
            });
        }

    }
}
