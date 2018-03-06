namespace Worldpay.CIS.DataAccess.Connection
{
        public class DataContext
        {
            public DataContext()
            {
                // Set default values.
                CisConnectionString = "";
                StarV3ConnectionString = "";
                CisNewConnectionString = "";
                CacheDatabase = "";

                HistoryConnectionString = "";

                HistoryTierConnectionString = "";

                CisStageConnectionString = "";

                StaticReportsConnectionString = "";

                TranHistSumConnectionString = "";


                CommandTimeout = 120;

            }
            public string CisConnectionString { get; set; }
            public string StarV3ConnectionString { get; set; }
            public string CisNewConnectionString { get; set; }
            public string CacheDatabase { get; set; }
            public string HistoryConnectionString { get; set; }
            public string HistoryTierConnectionString { get; set; }
            public string CisStageConnectionString { get; set; }
            public string StaticReportsConnectionString { get; set; }
            public string TranHistoryConnectionString { get; set; }
            public int MaxNumberOfRecordsToReturn { get; set; }
            public string TranHistSumConnectionString { get; set; }

            public int CommandTimeout { get; set; }
    }
    }

