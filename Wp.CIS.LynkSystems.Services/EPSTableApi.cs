using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.EpsTable;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class EPSTableApi : IEPSTableApi
    {
        public IEPSTableRepository _epsTableRepository;
        private readonly IXmlApi _xmlApi;
        public EPSTableApi(IOptions<Settings> optionsAccessor, IEPSTableRepository epsTableRepository, IXmlApi xmlApi)
        {
            _epsTableRepository = epsTableRepository;
            _xmlApi = xmlApi;
        }

        public async Task<ApiResult<ICollection<PetroTable>>> EPSGetAllPetroTablesByVersion(int versionID)
        {
            ApiResult<ICollection<PetroTable>> response = new ApiResult<ICollection<PetroTable>>();
            try
            {
                response.Result = await _epsTableRepository.EPSGetAllPetroTablesByVersionAsync(versionID);
            }
            catch (System.Exception)
            {

                throw;
            }
            
            return response;
        }
        
        public async Task<ApiResult<EPSTableErrorCodes>> EPSUpsertPetroTable(PetroTable petroTable)
        {
            ApiResult<EPSTableErrorCodes> apiResponse = new ApiResult<EPSTableErrorCodes>();

            try
            {
                var response = EPSTableErrorCodes.Succeeded;
                if (petroTable != null && !string.IsNullOrEmpty(petroTable.SchemaDef))
                {
                    petroTable.SchemaDef = _xmlApi.RemoveEncoding(petroTable.SchemaDef);
                }
                if (petroTable != null && !string.IsNullOrEmpty(petroTable.DefaultXML))
                {
                    petroTable.DefaultXML = _xmlApi.RemoveEncoding(petroTable.DefaultXML);
                }
                if (petroTable.DefinitionOnly && !string.IsNullOrEmpty(petroTable.DefaultXML))
                {
                    response = EPSTableErrorCodes.EPSTableDefaultXMLErrorMsg;
                }
                else if (petroTable.EffectiveDate == System.DateTime.MinValue)
                {
                    response = EPSTableErrorCodes.EPSTableEffectiveDateErrorMsg;
                }
                else if (string.IsNullOrEmpty(petroTable.LastUpdatedBy))
                {
                    response = EPSTableErrorCodes.EPSTableLastUpdatedByErrorMsg;
                }
                else if (string.IsNullOrEmpty(petroTable.TableName))
                {
                    response = EPSTableErrorCodes.EPSTableTableNameErrorMsg;
                }
                else if (petroTable.VersionID <= 0)
                {
                    response = EPSTableErrorCodes.EPSTableVersionErrorMsg;
                }
                else
                {
                    apiResponse.Result = await _epsTableRepository.EPSUpsertPetroTableAsync(petroTable);
                    return apiResponse;
                }
                apiResponse.Result = response;

            }
            catch (System.Exception)
            {

                throw;
            }

            return apiResponse;
        }
    }
}
