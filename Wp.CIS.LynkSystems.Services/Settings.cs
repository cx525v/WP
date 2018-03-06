using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Services
{
    public class Settings
    {
        public Settings()
        {
            // Set default values.
            CISConnectionString = "";
            StarV3ConnectionString = "";
            CISNewConnectionString = "";
            CacheDatabase = "";

            HistoryConnectionString = "";

            HistoryTierConnectionString = "";

            CISStageConnectionString = "";

            StaticReportsConnectionString = "";

            TranHistSumConnectionString ="";

        }
        public string CISConnectionString { get; set; }
        public string StarV3ConnectionString { get; set; }
        public string CISNewConnectionString { get; set; }
        public string CacheDatabase { get; set; }
        public string HistoryConnectionString { get; set; }
        public string HistoryTierConnectionString { get; set; }
        public string CISStageConnectionString { get; set; }
        public string StaticReportsConnectionString { get; set; }

        public int MaxNumberOfRecordsToReturn { get; set; }

        public string EnvironmentName { get; set; }
        public string TranHistSumConnectionString { get; set; }

    }
}
