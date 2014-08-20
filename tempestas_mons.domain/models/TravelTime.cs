using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tempestas_mons.domain.models
{
    public class TravelTime
    {
        public Direction Direction { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public double TravelTime { get; set; }
    }

    public enum Direction
    {
        Eastbound,
        Westbound
    }
}
