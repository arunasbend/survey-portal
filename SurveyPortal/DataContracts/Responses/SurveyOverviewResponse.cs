using System;
using System.Collections.Generic;

namespace SurveyPortal.DataContracts.Responses
{
    public class SurveyOverviewResponse
    {
        public SurveyOverviewResponseHeader Header { get; set; }
        public List<SurveyManager> SurveyManager { get; set; }
    }

    public class SurveyOverviewResponseHeader
    {
        public List<StatisticsData> StatisticsData { get; set; }
        public List<SurveyData> SurveyData { get; set; }
    }

    public class SurveyManager
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class StatisticsData
    {
        public string Title { get; set; }
        public int Count { get; set; }
    }

    public class SurveyData
    {
        public string Title { get; set; }
        public int Count { get; set; }
    }
}
