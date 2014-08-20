using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using tempestas_mons.web.Models.api;

namespace tempestas_mons.web.Controllers.api
{
    public class RoadConditionController : ApiController
    {
        public List<RoadConditionApiModel> Get()
        {
            return new List<RoadConditionApiModel>();
        }

        public List<RoadConditionApiModel> Get(string startDate, string endDate)
        {
            return new List<RoadConditionApiModel>();
        }

        public List<RoadConditionApiModel> Get(string startDate, string endDate, string direction)
        {
            return new List<RoadConditionApiModel>();
        }
    }
}
