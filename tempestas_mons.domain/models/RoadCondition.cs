using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tempestas_mons.domain.models
{
    public class RoadCondition
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public double Temperature { get; set; }

        public string Weather { get; set; }

        public string RoadCondition { get; set; }

        public List<string> RestrictionMessages { get; set; }
    }
}
