using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tempestas_mons.web.Models.roadconditions;

namespace tempestas_mons.web.Controllers
{
    public class RoadConditionController : Controller
    {
        // GET: RoadCondition
        public ViewResult Index(string startDate, string endDate)
        {
            var viewModel = new RoadConditionIndexViewModel
            {
                Summary = new RoadConditionSummaryViewModel
                {
                    PercentChainsRequiredAllVehicles = 0,
                    PercentChainsRequiredExceptAllWheelDrive = 0,
                    PercentPassClosed = 0,
                    PercentTractionTiresAdvised = 0,
                    PercentTractionTiresRequired = 0,
                    PercentTrafficDelayed = 0,
                    PercentTrafficDelayedForAvalancheControl = 0,
                    PercentTrafficStoppedForAvalancheControl = 0,
                }
            };

            //if (startDate != null && endDate != null)
            {
                viewModel.Summary = new RoadConditionSummaryViewModel
                {
                    PercentChainsRequiredAllVehicles = 50,
                    PercentChainsRequiredExceptAllWheelDrive = 60,
                    PercentPassClosed = 10,
                    PercentTractionTiresAdvised = 12,
                    PercentTractionTiresRequired = 90,
                    PercentTrafficDelayed = 5,
                    PercentTrafficDelayedForAvalancheControl = 1,
                    PercentTrafficStoppedForAvalancheControl = 2,
                };
            }
         
            return View(viewModel);
        }
    }
}