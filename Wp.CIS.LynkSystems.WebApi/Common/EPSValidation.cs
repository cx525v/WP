using System;
using System.Linq;
using System.Text.RegularExpressions;
using Wp.CIS.LynkSystems.Model;

namespace Wp.CIS.LynkSystems.WebApi.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class EPSValidation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public static String PostValidation(EPSMapping mapping)
        {
            String errorkey = "";
            if (mapping.pdlFlag)
            {
                if (string.IsNullOrEmpty(mapping.paramName))
                {
                    errorkey = "EPSMappingParamNameErrorMsg";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(mapping.worldPayFieldName) || string.IsNullOrEmpty(mapping.worldPayTableName))
                {
                    errorkey = "EPSMappingTable_FieldNamerErrorMsg";
                }
            }
            if ((string.IsNullOrEmpty(mapping.viperFieldName) || string.IsNullOrEmpty(mapping.viperTableName)) && string.IsNullOrEmpty(errorkey))
            {
                errorkey = "EPSMappingViperFieldsErrorMsg";

            }

            if ((mapping.effectiveBeginDate.HasValue == false || mapping.effectiveEndDate.HasValue == false) && string.IsNullOrEmpty(errorkey))
            {
                errorkey = "EPSMappingDatesErrorMsg";
            }

            return errorkey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="petroTable"></param>
        /// <returns></returns>
        public static string UpsertPetroTable(PetroTable petroTable)
        {
            string errorKey = string.Empty;
            if (petroTable.DefinitionOnly && !string.IsNullOrEmpty(petroTable.DefaultXML))
            {
                errorKey = "EPSTableDefaultXMLErrorMsg";
            }
            else if (petroTable.EffectiveDate == System.DateTime.MinValue)
            {
                errorKey = "EPSTableEffectiveDateErrorMsg";
            }
            else if (string.IsNullOrEmpty(petroTable.LastUpdatedBy))
            {
                errorKey = "EPSTableLastUpdatedByErrorMsg";
            }
            else if (string.IsNullOrEmpty(petroTable.TableName))
            {
                errorKey = "EPSTableTableNameErrorMsg";
            }
            else if (petroTable.VersionID <= 0)
            {
                errorKey = "EPSTableVersionErrorMsg";
            }
            return errorKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static string ValidateLogRequest(string startDate, string endDate)
        {
            string errorkey = string.Empty;
            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                errorkey = "EPSLogDateRangeError";
            }
            else if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                DateTime start = Convert.ToDateTime(startDate);
                DateTime end = Convert.ToDateTime(endDate);
                if ((end - start).TotalDays > 62)
                {
                    errorkey = "EPSLogDateRangeError";
                }
            }
            return errorkey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool ValidateVersionFormat(string version)
        {
            string regularExpression = @"^[A-Z\s.0-9#$*()?!+_-]{1,20}$";
            Regex regex = new Regex(regularExpression, RegexOptions.Singleline);
            Match m = regex.Match(version);
            return m.Success && !version.Split(new char[0])[0].Any(char.IsLower);
        }

    }
}
