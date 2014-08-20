using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tempestas_mons.domain.models.rawdata
{
    public class TravelTime
    {
        public string Date_Time {get;set;}
        public string DirectionTravelTimeMinutes {get;set;}
        public string Free_Flow { get; set; }
    }
}
