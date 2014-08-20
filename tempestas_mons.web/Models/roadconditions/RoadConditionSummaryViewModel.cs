using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tempestas_mons.web.Models.roadconditions
{
    public class RoadConditionSummaryViewModel
    {
        public double PercentTractionTiresAdvised { get; set; }

        public double PercentTractionTiresRequired { get; set; }

        public double PercentChainsRequiredAllVehicles { get; set; }

        public double PercentChainsRequiredExceptAllWheelDrive { get; set; }

        public double PercentPassClosed { get; set; }

        public double PercentTrafficDelayed { get; set; }

        public double PercentTrafficDelayedForAvalancheControl { get; set; }

        public double PercentTrafficStoppedForAvalancheControl { get; set; }
    }
}