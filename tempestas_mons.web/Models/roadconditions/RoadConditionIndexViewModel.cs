using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tempestas_mons.web.Models.roadconditions
{
    public class RoadConditionIndexViewModel
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Direction { get; set; }

        public RoadConditionSummaryViewModel Summary { get; set; }
    }
}