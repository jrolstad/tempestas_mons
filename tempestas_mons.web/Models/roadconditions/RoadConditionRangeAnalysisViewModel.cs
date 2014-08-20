namespace tempestas_mons.web.Models.roadconditions
{
    public class RoadConditionRangeAnalysisViewModel
    {
        public string Month { get; set; }

        public int Day { get; set; }

        public int Range { get; set; }

        public string Direction { get; set; }

        public RoadConditionSummaryViewModel Summary { get; set; }
    }
}