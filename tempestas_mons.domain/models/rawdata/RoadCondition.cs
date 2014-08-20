using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tempestas_mons.domain.models.rawdata
{
    public class RoadCondition
    {
        public string MountainPassConditionArchiveID {get;set;}
        public string DateUpdated {get;set;}
        public string Temperature {get;set;}
        public string Weather {get;set;}
        public string RoadCondition {get;set;}
        public string RestrictionText1 {get;set;}
        public string RestrictionText2 { get; set; }
    }
}
