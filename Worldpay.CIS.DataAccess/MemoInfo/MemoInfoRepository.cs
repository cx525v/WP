using Dapper;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.MemoInfo
{
    public class MemoInfoRepository : IMemoInfoRepository
    {
        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly IDatabaseConnectionFactory _connectionFactory;

        #endregion

        #region Public Constructors

        public MemoInfoRepository(IOptions<DataContext> optionsAccessor, 
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

        public async Task<MemoList> GetMemoResults(Helper.LIDTypes LIDtype, int LID)
        {
            try
            {
                var memoList = new MemoList();
                return await this._connectionFactory.GetConnection(async c =>
                {
                    using (var multi = c.QueryMultiple("[CISPlus].[uspGetMemoInfoForSearchLynk]  @LidType, @Lid",
                         new { LidType = LIDtype, Lid = LID }))
                    {
                        memoList.customerMemo = multi.Read<Wp.CIS.LynkSystems.Model.MemoInfo>().ToList();
                        memoList.merchMemo = multi.Read<Wp.CIS.LynkSystems.Model.MemoInfo>().ToList();
                        memoList.termMemo = multi.Read<Wp.CIS.LynkSystems.Model.MemoInfo>().ToList();
                        memoList.groupMemo = multi.Read<Wp.CIS.LynkSystems.Model.MemoInfo>().ToList();
                        return memoList;
                    }
                });
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
