using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.CommanderVersion;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class CommanderVersionApi : ICommanderVersionApi
    {
        private ICommanderVersionRepository _commanderVersionRepository;
        public CommanderVersionApi(IOptions<Settings> optionsAccessor, ICommanderVersionRepository repository)
        {
            _commanderVersionRepository = repository;
        }
        public Task<ICollection<CommanderVersion>> GetVersions()
        {

            return _commanderVersionRepository.GetVersionsAsync();
        }

        public async Task<ApiResult<ICollection<BaseVersion>>> GetBaseVersions()
        {
            ApiResult<ICollection<BaseVersion>> response = new ApiResult<ICollection<BaseVersion>>();

            try
            {
                response.Result = await _commanderVersionRepository.GetBaseVersionsAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResult<bool>> CreateVersion(CommanderVersion commanderVersion)
        {
            ApiResult<bool> response = new ApiResult<bool>();

            try
            {
                bool isValid = false;
                string regularExpression = @"^[A-Z\s.0-9#$*()?!+_-]{1,20}$";
                Regex regex = new Regex(regularExpression, RegexOptions.Singleline);
                Match m = regex.Match(commanderVersion.VersionDescription);
                isValid = m.Success;
                if (isValid)
                    response.Result = await _commanderVersionRepository.CreateVersionAsync(commanderVersion);
                else
                {
                    response.AddErrorMessage(CommanderVersionErrorCodes.CommanderCreateversionsVersionFormatErrorMsg.ToString());
                }
            }
            catch(Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<ApiResult<bool>> DeleteVersion(int versionID, string userName)
        {
            ApiResult<bool> response = new ApiResult<bool>();

            try
            {
                response.Result = await _commanderVersionRepository.DeleteVersionAsync(versionID, userName);
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }
    }
}
